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
    /// Interaction logic for AddTeam.xaml
    /// </summary>
    public partial class AddTeam : Window
    {
        public AddTeam()
        {
            InitializeComponent();
            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            imgCollectiveLeave.Source = photo;
        }

        private void AddEquip_Click(object sender, RoutedEventArgs e)
        {

        }

     

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NotificationOverbooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
