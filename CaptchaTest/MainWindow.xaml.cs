using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CaptchaTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Capch capch = null;

        public List<Capch> capches = new List<Capch>
        {
            new Capch{ IsTru="F13" , path = "/Image/F13.png"} ,
            new Capch{ IsTru="G7H" , path = "/Image/G7H.png"} ,
            new Capch{ IsTru="NOZ" , path = "/Image/NOZ.png"} ,
        };



        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            Title = "Вход";
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RandomCapcha();

        }

        private void RandomCapcha()
        {
            Random random = new Random();
            var   newcapch = capches[random.Next(capches.Count)];

            if(capch == null)
            {
                capch = newcapch;
                imageCapch.Source = new BitmapImage(new Uri(capch.path, UriKind.Relative));
                return;
            }
           
            if(capch.IsTru != newcapch.IsTru)
            {
                capch = newcapch;
                imageCapch.Source = new BitmapImage(new Uri(capch.path, UriKind.Relative));
                return;
            }

            if (capch.IsTru == newcapch.IsTru)
            {
                RandomCapcha();
            }
        }

        private void GoButonn_Click(object sender, RoutedEventArgs e)
        {
            if (tbLog.Text == String.Empty)
            { MessageBox.Show("введите логин"); return; }

            if (tbpass.Text  == String.Empty)
            { MessageBox.Show("введите Пароль"); return; }



            if (tbCapcha.Text != capch.IsTru)
            {
                MessageBox.Show( " Вы введи не правельную  капчу - Блокировка на  10 секунд ");
                for (int i = 10; i > 0; i--)
                {
                    this.Title =  $"блокировка {i}  cек";
                    Thread.Sleep(1000);
                }

                RandomCapcha();
                tbCapcha.Clear();
                Title = "Вход";
                return;
            }


            if(tbpass.Text == "admin" && tbLog.Text == "admin" && tbCapcha.Text == capch.IsTru )
            {
                MessageBox.Show("Ура");
            }
            else 
            {
                MessageBox.Show("неверный логин  или пароль");
            }


        }

        private void refreshButonn_Click(object sender, RoutedEventArgs e)
        {
            RandomCapcha();
        }
    }


    public  class Capch
    {
      public   string path { get; set; }
      public string IsTru { get; set; }
    }
}
