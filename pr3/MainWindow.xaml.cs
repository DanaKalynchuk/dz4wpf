using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace pr3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        
    {
        DispatcherTimer timer = new DispatcherTimer();
        public string path = "";
        BitmapImage ImageForAudio = new BitmapImage(new Uri(@"pack://application:,,,/Resources/fon.jpg"));
        public bool isPlaying= false;
        
        public MainWindow()
        {
            InitializeComponent();
            t.Content = ">10";
            b.Content = "<10";
            Pausa.Content = ">>";
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Time.Text = Player.Position.ToString();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "mp4(*.mp4)|*.mp4|mp3(*.mp3)|*.mp3";

            try
            {
                if (openFile.ShowDialog() == true)
                {
                    path = openFile.FileName;
                    Uri uri = new Uri(openFile.FileName);
                    Player.Source = uri;


                    FileInfo info = new FileInfo(openFile.FileName);
                    ListBoxItem item = new ListBoxItem();
                    item.Content = info.Name;
                    item.Tag = openFile.FileName;
                    list.Items.Add(item);
                    Player.Play();
                    isPlaying = true;
                    Pausa.Content = "||";

                }
            }
            catch(Exception x)
            {
                Window1 window = new Window1(x.Message);
                window.ShowDialog();
            }
        }

        private void Player_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (Player.HasAudio)
            {

                ImageAudio.Source = ImageForAudio;
            }

        }

        private void Pausa_Click(object sender, RoutedEventArgs e)
        {
            if(isPlaying)
            {
                Player.Pause();
                Pausa.Content = ">>";
                isPlaying = false;
                timer.Stop();
            }
            else
            {
                Player.Play();
                Pausa.Content = "||";
                isPlaying = true;
                timer.Start();
            }

        }

        private void t_Click(object sender, RoutedEventArgs e)
        {
            Player.Position +=new TimeSpan(0,0,10);
        }

        private void b_Click(object sender, RoutedEventArgs e)
        {
            Player.Position -= new TimeSpan(0, 0, 10);
        }

        private void Player_MediaEnded_1(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void x05_Click(object sender, RoutedEventArgs e)
        {

            x1.IsChecked = false;
            x125.IsChecked= false;
            x15.IsChecked= false;
            x175.IsChecked= false;
            x2.IsChecked= false;
            Player.SpeedRatio = 0.5;
        }

        private void x1_Click(object sender, RoutedEventArgs e)
        {
            x05.IsChecked = false;
            
            x125.IsChecked = false;
            x15.IsChecked = false;
            x175.IsChecked = false;
            x2.IsChecked = false;
            Player.SpeedRatio = 1;
        }

        private void x125_Click(object sender, RoutedEventArgs e)
        {
            x05.IsChecked = false;
            x1.IsChecked = false;
            
            x15.IsChecked = false;
            x175.IsChecked = false;
            x2.IsChecked = false;
            Player.SpeedRatio = 1.25;
        }

        private void x15_Click(object sender, RoutedEventArgs e)
        {
            x05.IsChecked = false;
            x1.IsChecked = false;
            x125.IsChecked = false;
            
            x175.IsChecked = false;
            x2.IsChecked = false;
            Player.SpeedRatio = 1.5;
        }

        private void x175_Click(object sender, RoutedEventArgs e)
        {
            x05.IsChecked = false;
            x1.IsChecked = false;
            x125.IsChecked = false;
            x15.IsChecked = false;
           
            x2.IsChecked = false;
            Player.SpeedRatio = 1.75;
        }

        private void x2_Click(object sender, RoutedEventArgs e)
        {
            x05.IsChecked = false;
            x1.IsChecked = false;
            x125.IsChecked = false;
            x15.IsChecked = false;
            x175.IsChecked = false;
            Player.SpeedRatio = 2;

        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                
                Uri uri = new Uri(((ListBoxItem)list.SelectedItem).Tag.ToString());
                Player.Source = uri;
            }
        }
    }
}
