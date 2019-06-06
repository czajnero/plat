using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //const string APPID = "";
        //string city = "Wroclaw";

        private void Btn_WeatherCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WebClient web = new WebClient();

                StringBuilder stringBuilder = new StringBuilder
                ("https://api.openweathermap.org/data/2.5/weather?appid=968f21f3c0df218e3d72d731eedcda52&units=metric&cnt=6&lang=pl&");

                stringBuilder.Append("q=" + tbox_City.Text);
                var json = web.DownloadString(stringBuilder.ToString());

                stringBuilder.Clear();
                var weather = JsonConvert.DeserializeObject<WeatherApplication.WeatherParser.WeatherObject>(json);

                WeatherApplication.WeatherParser.WeatherObject output = weather;

                stringBuilder.AppendLine(string.Format("Temperatura: {0}", weather.main.temp));
                stringBuilder.AppendLine(string.Format("Cisnienie powietrza: {0} hPa", weather.main.pressure));
                stringBuilder.AppendLine(string.Format("Wiatr: {0} m/s", weather.wind.speed));
                stringBuilder.AppendLine(string.Format("Zachmurzenie: {0}", weather.weather[0].description));

                tbox_WeatherBox.Text = stringBuilder.ToString();

                stringBuilder.Clear();
                stringBuilder.Append("http://api.openweathermap.org/img/w/");

                stringBuilder.Append(weather.weather[0].icon);

                stringBuilder.Append(".png");

                var uri = new Uri(stringBuilder.ToString());
                var bitmap = new BitmapImage(uri);
                img_Weather.Source = bitmap;
            }
            catch (Exception)
            {
                MessageBox.Show("Connection error");
            }
        }



        public MainWindow()
        {
            InitializeComponent();
          
        }

        
        private void Btn_DatabaseSend_Click(object sender, RoutedEventArgs e)
        {
            
            var context = new WeatherTestEntities();
            var post = new Post()
            {
                ID = 55,
                City = tbox_City.Text,
                Date = DateTime.Now
            };
            context.Posts.Add(post);
            context.SaveChanges();

            MessageBox.Show("Dodano");
        }

    








        /*
        public class BlogDbContext : DbContext
        {
            public DbSet<Post> Posts { get; set; }
        }
        */


    }
}
