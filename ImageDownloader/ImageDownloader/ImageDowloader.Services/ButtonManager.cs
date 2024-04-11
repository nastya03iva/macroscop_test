using System.Windows;
using System.Windows.Controls;
using Image = System.Windows.Controls.Image;

namespace ImageDownloader.Services
{
    /// <summary>
    /// Класс для управления кнопками и соответствующими элементами интерфейса
    /// </summary>
    public class ButtonManager
    {
        /// <summary>
        /// Экземпляр для управления процессом загрузки изображений
        /// </summary>
        private ImageDownloader _imageDownloader;
        
        /// <summary>
        /// Список текстбоксов с URL 
        /// </summary>
        private List<TextBox> _urlTextBoxes = new List<TextBox>();

        public ButtonManager(Button startButton, Button stopButton, Button downloadAll, TextBox urlTextBox, 
            Image image, ProgressBar progressBar)
        {
            _urlTextBoxes.Add(urlTextBox);
      
            _imageDownloader = new ImageDownloader(image, progressBar);

            startButton.Click += StartButton_Click;
            downloadAll.Click += StartButton_Click;
            stopButton.Click += StopButton_Click;
        }

        /// <summary>
        /// Обработчик события Click для кнопки начала загрузки
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Объект, который содержит информацию о событии</param>
        /// <returns></returns>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _imageDownloader.StartDownload(_urlTextBoxes);
        }

        /// <summary>
        /// Обработчик события Click для кнопки остановки загрузки
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Объект, который содержит информацию о событии</param>
        /// <returns></returns>
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _imageDownloader.StopDownload();
        }

        /// <summary>
        /// Обработчик события GotFocus для текстбокса
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Объект, который содержит информацию о событии</param>
        /// <returns></returns>
        public static void UrlTextBox_GotFocus(TextBox urlTextBox, Button startDownload, Button stopDownload, Button downloadAll)
        {
            if (urlTextBox.Text == "Введите URL")
            {
                urlTextBox.Text = "";

                startDownload.IsEnabled = true;
                downloadAll.IsEnabled = true;
                stopDownload.IsEnabled = true;
            }
        }

        /// <summary>
        /// Обработчик события LostFocus для текстбокса
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Объект, который содержит информацию о событии</param>
        /// <returns></returns>
        public static void UrlTextBox_LostFocus(TextBox urlTextBox, Button startDownload, Button stopDownload)
        {
            if (string.IsNullOrWhiteSpace(urlTextBox.Text))
            {
                urlTextBox.Text = "Введите URL";

                startDownload.IsEnabled = false;
                stopDownload.IsEnabled = false;
            }
        }
    }
}
