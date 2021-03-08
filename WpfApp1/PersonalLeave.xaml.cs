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
    /// Interaction logic for PersonalLeave.xaml
    /// </summary>
    public partial class PersonalLeave : Window
    {
        public PersonalLeave()
        {
            InitializeComponent();
            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));

           PLBarco.Source = photo;
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
