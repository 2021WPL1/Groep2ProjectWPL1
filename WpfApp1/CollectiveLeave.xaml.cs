using Barco.Data;
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

namespace Barco
{
    /// <summary>
    /// Interaction logic for CollectiveLeave.xaml
    /// </summary>
    public partial class CollectiveLeave : Window
    {
        Barco2021Context context = new Barco2021Context();
        public CollectiveLeave()
        {
            InitializeComponent();
            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            imgCollectiveLeave.Source = photo;
            
        }
       
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
