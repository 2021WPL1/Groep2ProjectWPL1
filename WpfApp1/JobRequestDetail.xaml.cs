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
    /// Interaction logic for JobRequestDetail.xaml
    /// </summary>
    ///         private DAO dao;

    public partial class JobRequestDetail : Window
    {
        private DAO dao;

        public JobRequestDetail()
        {
            InitializeComponent();
            dao = DAO.Instance();

        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void ShowDialog(ref int IdJr)
        {
            int Idjr = IdJr;
            RqRequest rqRequest = dao.getRqRequestById(Idjr);
            RqRequestDetail requestDetail = dao.getRqRequestDetailById(Idjr);
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
            lblExpectedEndDate.Content = rqRequest.ExpectedEnddate;
        }

    }
}
