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
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Window
    {
        public HomeScreen()
        {
            InitializeComponent();
            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            imgBarco.Source = photo;

        }
      
private void SeeAllRequests_Click(object sender, RoutedEventArgs e)
        {
            OverviewJobRequest overviewJobRequest = new OverviewJobRequest();

            overviewJobRequest.ShowDialog();
        }

        private void PersonalLeave_Click(object sender, RoutedEventArgs e)
        {
            PersonalLeave personalLeave = new PersonalLeave();
            personalLeave.ShowDialog();
        }

        private void CollectiveLeave_Click(object sender, RoutedEventArgs e)
        {
            /*CollectiveLeave collectiveLeave = new CollectiveLeave();
            collectiveLeave.ShowDialog();*/
        }

        private void CreateJobRequest_Click(object sender, RoutedEventArgs e)
        {
            JobRequest createJobRequest = new JobRequest();
            createJobRequest.ShowDialog();
        }
    }
}
