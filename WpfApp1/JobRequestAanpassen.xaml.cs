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
        public JobRequestAanpassen()
        {
            InitializeComponent();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
             
        }

        private void btnRemovePart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddPart_Click(object sender, RoutedEventArgs e)
        {
             
        }

        
        private void btnCancelRequest_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void ShowDialog(ref int IdJr)
        {
            int Idjr = IdJr;
            RqRequest rqRequest = new dao.getRqRequestById(Idjr);
            RqRequestDetail requestDetail = new dao.getRqRequestDetailById(Idjr);
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
    }
}
