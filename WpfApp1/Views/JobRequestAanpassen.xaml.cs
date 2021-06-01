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
    /// Interaction logic for JobRequestAanpassen.xaml
    /// </summary>
    public partial class JobRequestAanpassen : Window
    {

        private static DAO dao;

        private JobRequestAanpassenViewModel jobRequestAanpassenViewModel;



        public JobRequestAanpassen(int selectedId)
        {
            InitializeComponent();
            dao = DAO.Instance();

            showDivision(selectedId);
            getJobNatures();
            jobRequestAanpassenViewModel = new JobRequestAanpassenViewModel(this, selectedId);
            DataContext = jobRequestAanpassenViewModel;
           


        }

        public void showDivision(int id)
        {

            comboBoxDivision.ItemsSource = dao.GetDepartment();
            comboBoxDivision.DisplayMemberPath = "Afkorting";
            comboBoxDivision.SelectedValuePath = "Afkorting";
            comboBoxDivision.SelectedValue = dao.GetRqRequestById(id).BarcoDivision;

        }
        public void getJobNatures()
        {
            comboBoxJobNature.ItemsSource = dao.GetJobNatures();
            comboBoxJobNature.DisplayMemberPath = "Nature";
            comboBoxJobNature.SelectedValuePath = "Nature";
        }

        private void txtRequisterInitials_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void btnRemovePart_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void btnAddPart_Click(object sender, RoutedEventArgs e)
        //{

        //}

        ////bianca
        //private void btnCancelRequest_Click(object sender, RoutedEventArgs e)
        //{
        //    HomeScreen homeScreen = new HomeScreen();
        //    Close();
        //    homeScreen.ShowDialog();
        //}

        /*
        public void ShowDialog(ref int IdJr)
        { 
                int Idjr = IdJr;
                RqRequest rqRequest = dao.GetRqRequestById(Idjr);
                RqRequestDetail requestDetail = dao.GetRqRequestDetailById(Idjr);
            try
            {
                if (rqRequest.Battery == true)
                {
                    RBBatteriesYes.IsChecked = true;
                }
                else
                {
                    RBBatteriesNo.IsChecked = true;
                }
                txtProjectName.Text = rqRequest.EutProjectname;
                txtRequisterInitials.Text = rqRequest.Requester;
                comboBoxDivision.SelectedItem = rqRequest.BarcoDivision;
                comboBoxJobNature.SelectedItem = rqRequest.JobNature;
                lblRequestDate.Content = rqRequest.RequestDate;
                lblJobRequestNumber.Content = rqRequest.JrNumber;
                txtPvgRes.Text = requestDetail.Pvgresp;
                DatePickerExpectedEndDate.SelectedDate = rqRequest.ExpectedEnddate;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           */
    }

}

