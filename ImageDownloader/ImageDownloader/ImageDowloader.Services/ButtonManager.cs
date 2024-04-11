using System.Windows;
using System.Windows.Controls;
using Image = System.Windows.Controls.Image;

namespace ImageDownloader.Services
{
    /// <summary>
    /// ����� ��� ���������� �������� � ���������������� ���������� ����������
    /// </summary>
    public class ButtonManager
    {
        /// <summary>
        /// ��������� ��� ���������� ��������� �������� �����������
        /// </summary>
        private ImageDownloader _imageDownloader;
        
        /// <summary>
        /// ������ ����������� � URL 
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
        /// ���������� ������� Click ��� ������ ������ ��������
        /// </summary>
        /// <param name="sender">������, ������� ��������������� �������</param>
        /// <param name="e">������, ������� �������� ���������� � �������</param>
        /// <returns></returns>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _imageDownloader.StartDownload(_urlTextBoxes);
        }

        /// <summary>
        /// ���������� ������� Click ��� ������ ��������� ��������
        /// </summary>
        /// <param name="sender">������, ������� ��������������� �������</param>
        /// <param name="e">������, ������� �������� ���������� � �������</param>
        /// <returns></returns>
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _imageDownloader.StopDownload();
        }

        /// <summary>
        /// ���������� ������� GotFocus ��� ����������
        /// </summary>
        /// <param name="sender">������, ������� ��������������� �������</param>
        /// <param name="e">������, ������� �������� ���������� � �������</param>
        /// <returns></returns>
        public static void UrlTextBox_GotFocus(TextBox urlTextBox, Button startDownload, Button stopDownload, Button downloadAll)
        {
            if (urlTextBox.Text == "������� URL")
            {
                urlTextBox.Text = "";

                startDownload.IsEnabled = true;
                downloadAll.IsEnabled = true;
                stopDownload.IsEnabled = true;
            }
        }

        /// <summary>
        /// ���������� ������� LostFocus ��� ����������
        /// </summary>
        /// <param name="sender">������, ������� ��������������� �������</param>
        /// <param name="e">������, ������� �������� ���������� � �������</param>
        /// <returns></returns>
        public static void UrlTextBox_LostFocus(TextBox urlTextBox, Button startDownload, Button stopDownload)
        {
            if (string.IsNullOrWhiteSpace(urlTextBox.Text))
            {
                urlTextBox.Text = "������� URL";

                startDownload.IsEnabled = false;
                stopDownload.IsEnabled = false;
            }
        }
    }
}
