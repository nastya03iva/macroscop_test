using ImageDownloader.Services;
using System.Windows;

namespace ImageDownloader
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Экземпляр для управления первым набором элементов интерфейса
        /// </summary>
        private readonly ButtonManager _buttonManager1;

        /// <summary>
        /// Экземпляр для управления вторым набором элементов интерфейса
        /// </summary>
        private readonly ButtonManager _buttonManager2;

        /// <summary>
        /// Экземпляр для управления третьим набором элементов интерфейса
        /// </summary>
        private readonly ButtonManager _buttonManager3;

        public MainWindow()
        {
            InitializeComponent();
            
            _buttonManager1 = new ButtonManager(StartDownload1, StopDownload1, DownloadAll, UrlTextBox1, Image1, ProgressBar);
            _buttonManager2 = new ButtonManager(StartDownload2, StopDownload2, DownloadAll, UrlTextBox2, Image2, ProgressBar);
            _buttonManager3 = new ButtonManager(StartDownload3, StopDownload3, DownloadAll, UrlTextBox3, Image3, ProgressBar);
        }

        /// <summary>
        /// Обработчик события GotFocus для UrlTextBox1
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Объект, который содержит информацию о событии</param>
        /// <returns></returns>
        private void UrlTextBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            ButtonManager.UrlTextBox_GotFocus(UrlTextBox1, StartDownload1, StopDownload1, DownloadAll);
        }

        /// <summary>
        /// Обработчик события LostFocus для UrlTextBox1
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Объект, который содержит информацию о событии</param>
        /// <returns></returns>
        private void UrlTextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            ButtonManager.UrlTextBox_LostFocus(UrlTextBox1, StartDownload1, StopDownload1);
        }

        /// <summary>
        /// Обработчик события GotFocus для UrlTextBox2
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Объект, который содержит информацию о событии</param>
        /// <returns></returns>
        private void UrlTextBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            ButtonManager.UrlTextBox_GotFocus(UrlTextBox2, StartDownload2, StopDownload2, DownloadAll);
        }

        /// <summary>
        /// Обработчик события LostFocus для UrlTextBox2
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Объект, который содержит информацию о событии</param>
        /// <returns></returns>
        private void UrlTextBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            ButtonManager.UrlTextBox_LostFocus(UrlTextBox2, StartDownload2, StopDownload2);
        }

        /// <summary>
        /// Обработчик события GotFocus для UrlTextBox3
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Объект, который содержит информацию о событии</param>
        /// <returns></returns>
        private void UrlTextBox3_GotFocus(object sender, RoutedEventArgs e)
        {
            ButtonManager.UrlTextBox_GotFocus(UrlTextBox3, StartDownload3, StopDownload3, DownloadAll);
        }

        /// <summary>
        /// Обработчик события LostFocus для UrlTextBox3
        /// </summary>
        /// <param name="sender">Объект, который инициализировал событие</param>
        /// <param name="e">Объект, который содержит информацию о событии</param>
        /// <returns></returns>
        private void UrlTextBox3_LostFocus(object sender, RoutedEventArgs e)
        {
            ButtonManager.UrlTextBox_LostFocus(UrlTextBox3, StartDownload3, StopDownload3);
        }
    }
}
