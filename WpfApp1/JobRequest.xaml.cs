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
        private DAO dao;
        public JobRequest()
        {
            InitializeComponent();
            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            imgJobRequest.Source = photo;
            dao = DAO.Instance();


            showDepartment();
             showJobNature();
         
        }


        //bianca
        private void showDepartment()
        {
            cmbDivision.Items.Clear();
            cmbDivision.ItemsSource = dao.getDepartment();
            cmbDivision.DisplayMemberPath = "Afkorting";
            cmbDivision.SelectedValuePath = "Afkorting";

        }

        //bianca
        private void showJobNature()
        {
            cmbJobNature.Items.Clear();
            cmbJobNature.ItemsSource = dao.getNature();
            cmbJobNature.DisplayMemberPath = "Nature";
            cmbJobNature.SelectedValuePath = "Nature";
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            


        }
        //bianca
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen homeScreen = new HomeScreen();
            Close();
            homeScreen.ShowDialog();
        }

        

    }
}
