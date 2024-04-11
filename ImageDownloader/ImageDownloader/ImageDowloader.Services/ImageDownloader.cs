using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ImageDownloader.Services
{
    /// <summary>
    /// Класс для управления процессом загрузки изображений
    /// </summary>
    internal class ImageDownloader
    {
        /// <summary>
        /// WebClient для загрузки одного изображения
        /// </summary>
        private readonly WebClient _webClient;

        /// <summary>
        /// Список webClients для одновременной загрузки нескольких изображений
        /// </summary>
        private List<WebClient> _webClients = new List<WebClient>();

        /// <summary>
        /// Список изображений для загрузки и отображения
        /// </summary>
        private List<Image> _images = new List<Image>();

        /// <summary>
        /// Флаг, указывающий выполняется ли в данный момент загрузка изображения
        /// </summary>
        private bool _isDownloading = false;

        /// <summary>
        /// Количество завершенных загрузок
        /// </summary>
        private int _completedCount = 0;

        /// <summary>
        /// Источник токенов отмены для отмены операции загрузки изображений
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Прогресс-бар для отображения прогресса загрузки изображений
        /// </summary>
        private ProgressBar _progressBar;

        public ImageDownloader(Image image, ProgressBar progressBar)
        {
            _webClient = new WebClient();

            _webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            _webClient.DownloadDataCompleted += WebClient_DownloadDataCompleted;

            _webClients.Add(_webClient);

            _progressBar = progressBar;

            _images.Add(image);
        }

        /// <summary>
        /// Загрузить изображение по ссылке
        /// </summary>
        /// <param name="urlTextBoxes">Список текстбоксов с URL</param>
        /// <returns></returns>
        public void StartDownload(List<TextBox> urlTextBoxes)
        {
            if (_isDownloading)
                return;

            List<string> urls = new List<string>();

            for (int i = 0; i < urlTextBoxes.Count; i++)
            {
                if (urlTextBoxes[i].Text != "" && urlTextBoxes[i].Text != "Введите URL")
                    urls.Add(urlTextBoxes[i].Text);
            }

            if (urls.Count == 0)
                return;

            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = _cancellationTokenSource.Token;

                for (int i = 0; i < urls.Count; i++)
                {
                    Uri uri = new Uri(urls[i]);
                    if (cancellationToken.IsCancellationRequested)
                        break;
                    _webClients[i].DownloadDataAsync(uri);
                }

                _isDownloading = false;
            }
            catch (UriFormatException ex)
            {
                MessageBox.Show("Ошибка: Неверный формат URL.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при запуске загрузки: " + ex.Message);
            }
        }

        /// <summary>
        /// Остановить загрузку изображения
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void StopDownload()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;

            foreach (var client in _webClients)
            {
                client.CancelAsync();
            }
        }

        /// <summary>
        /// Обработчик изменения прогресса загрузки данных с помощью WebClient
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Аргументы события DownloadProgressChangedEventArgs</param>
        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                _progressBar.Visibility = Visibility.Visible;

                Dispatcher dispatcher = Application.Current.Dispatcher;

                dispatcher.Invoke(() =>
                {
                    long totalBytesReceived = 0;
                    long totalBytesToReceive = 0;

                    foreach (var client in _webClients)
                    {
                        totalBytesReceived += e.BytesReceived;
                        totalBytesToReceive += e.TotalBytesToReceive;
                    }

                    int totalProgress = (int)((double)totalBytesReceived / totalBytesToReceive * 100);
                    _progressBar.Value = totalProgress;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении прогресса загрузки: " + ex.Message);
            }
        }

        /// <summary>
        /// Обработчик завершения загрузки данных с помощью WebClient
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Аргументы события DownloadDataCompleted</param>
        private void WebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    MessageBox.Show("Ошибка при загрузке изображения: \n" + e.Error.Message);
                    return;
                }

                if (e.Cancelled)
                {
                    MessageBox.Show("Загрузка изображения отменена.");
                    return;
                }

                _completedCount++;

                if (_completedCount == _webClients.Count)
                {
                    Dispatcher dispatcher = Application.Current.Dispatcher;

                    dispatcher.Invoke(() =>
                    {
                        for (int i = 0; i < _webClients.Count; i++)
                        {
                            if (i < _images.Count)
                                _images[i].Source = LoadImageFromData(e.Result);
                        }
                    });

                    _completedCount = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке: \n" + ex.Message);
            }
        }

        /// <summary>
        /// Загрузить изображение из массива байтов
        /// </summary>
        /// <param name="data">Массив байтов, представляющий изображение</param>
        /// <returns>Объект BitmapImage, представляющий загруженное изображение</returns>
        private BitmapImage LoadImageFromData(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
        }
    }
}
