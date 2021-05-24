using LandmarkAI.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png; *.jpg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (dialog.ShowDialog() == true)
            {
                string imagePath = dialog.FileName;
                SelectedImage.Source = new BitmapImage(new Uri(imagePath));

                MakePredictionAsync(imagePath);
            }
        }

        private async void MakePredictionAsync(string imagePath)
        {
            string url = "https://eastus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/3b3d3fac-af6b-4a0e-a9cd-f0556201e743/classify/iterations/Iteration1/image";
            string predictionKey = "a3770a59f5b941d6a935052dfc2477e7";
            string contentType = "application/octet-stream";

            var file = File.ReadAllBytes(imagePath);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);
                
                using (var content = new ByteArrayContent(file))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(url, content);

                    // Use http://jsonutils.com/ to convert the JSON string to a C# class.
                    var responseString = await response.Content.ReadAsStringAsync();


                    List<Prediction> prediction = (JsonConvert.DeserializeObject<CustomVision>(responseString)).predictions;
                }
            }
        }
    }
}
