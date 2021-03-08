using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Barco.Data;



namespace Barco
{
    /// <summary>
    /// Interaction logic for JobRequest.xaml
    /// </summary>
    public partial class JobRequest : Window
    {

        private static Barco2021Context context = new Barco2021Context();
        public JobRequest()
        {
            InitializeComponent();

            
            
        }

        /*BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
        imgJobRequest.Source = photo;*/



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close(); 
        }
    }
}
