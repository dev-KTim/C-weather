using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;

namespace Wetter_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string apiKey = "8a0f94abfa9fb9bf57d158df7761534f";
        private string requestUrl = "https://api.openweathermap.org/data/2.5/weather";

        public MainWindow()
        {
            InitializeComponent();

            UpdateData("München");
        }
        public void UpdateData(string city)
        {
            WeatherMapResponse result = GetWeatherData(city);

            string finalImage = "sun.png";
            string currentWeather = result.weather[0].main.ToLower();

            if (currentWeather.Contains("cloud"))
            {
                finalImage = "cloud.png";
            }
            else if (currentWeather.Contains("rain"))
            {
                finalImage = "rain.png";
            }
            else if (currentWeather.Contains("snow"))
            {
                finalImage = "snow.png";
            }

            backgroundImage.ImageSource = new BitmapImage(new Uri($"Images/{finalImage}", UriKind.Relative));


            labelTemperature.Content = $"{Math.Round(result.main.temp, 1)}°C";
            labelInfo.Content = result.weather[0].main;
        }

        public WeatherMapResponse GetWeatherData(string city)
        {
            //get weather from api, initialize city eg.

            HttpClient httpclient = new HttpClient(); //create an object client for my api
            var finalUri = requestUrl + "?q=" + city + "&appid=" + apiKey + "&units=metric"; //build final url with city, api, unit
            HttpResponseMessage httpresponse = httpclient.GetAsync(finalUri).Result; // send an async get request
            string response = httpresponse.Content.ReadAsStringAsync().Result; // read json text as a string!
            WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(response); //convert json text as string (deserialize)
            return weatherMapResponse; //return the deserialized object
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = textBoxQuery.Text;
            UpdateData(query);
        }
    }
}