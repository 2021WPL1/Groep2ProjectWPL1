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
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Window
    {
        private HomeScreenViewModel homeScreenViewModel;
        //bianca
        public HomeScreen()
        {
            InitializeComponent();
            homeScreenViewModel = new HomeScreenViewModel(this);
            DataContext = homeScreenViewModel;
            //BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/logo.png"));
            //imgBarco.Source = photo;
            //  dao = DAO.Instance();
        }
        //private void CreateJobRequest_Click(object sender, RoutedEventArgs e)
        //{
        //    JobRequest createJobRequest = new JobRequest();
        //    Close();
        //    createJobRequest.ShowDialog();
        //this.Hide();
        // homeScreenViewModel.CreateRequest();
        //}
        //private void SeeAllRequests_Click(object sender, RoutedEventArgs e)
        //{
        //    //OverviewJobRequest overviewJobRequest = new OverviewJobRequest();
        //   //Close();
        //    //overviewJobRequest.ShowDialog();
        //   this.Hide();
        //    homeScreenViewModel.Overview();
        //}
        //private void PersonalLeave_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Hide();
        //    //    PersonalLeave personalLeave = new PersonalLeave();
        //    //    Close();
        //    //    personalLeave.ShowDialog();
        //    homeScreenViewModel.PersonalLeave();
        //}
        //private void CollectiveLeave_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Hide();
        //    //CollectiveLeave collectiveLeave = new CollectiveLeave();
        //    //Close();
        //    //collectiveLeave.ShowDialog();
        //    homeScreenViewModel.CollectiveLeave();
        //}
    }
}
