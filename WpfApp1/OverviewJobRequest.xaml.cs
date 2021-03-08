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
//using Barco.Data;

namespace Barco 
{
    /// <summary>
    /// Interaction logic for OverviewJobRequest.xaml
    /// </summary>
    public partial class OverviewJobRequest : Window
    {
        public OverviewJobRequest()
        {
            InitializeComponent();
            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            imgOverview.Source = photo;


        }

        //public Barco2021Context context = new Barco2021Context();

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            JobRequestAanpassen jobRequestAanpassen = new JobRequestAanpassen();

            jobRequestAanpassen.ShowDialog();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowDetails_Click(object sender, RoutedEventArgs e)
        {
            JobRequestDetail jobRequestDetail = new JobRequestDetail();
            jobRequestDetail.ShowDialog();
        }
    }
}
