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
        private DAO dao;
        private RqRequest RqRequest;


        public JobRequestAanpassen(RqRequest rqRequest): base()
        {
            InitializeComponent();
            dao = DAO.Instance();
            RqRequest = rqRequest;
            load(rqRequest.IdRequest);

        }
        private void load(int selectedId)
        {
            RqRequest req = dao.getRequest(selectedId);
            RqRequestDetail reqdet = dao.getRequestDetail(selectedId);


            txtRequisterInitials.Text = req.Requester;
            txtProjectName.Text = req.EutProjectname;
            lblRequestDate.Content = req.RequestDate;
            DatePickerExpectedEndDate.SelectedDate = req.ExpectedEnddate;
            lblJobRequestNumber.Content = req.JrNumber;
            if (req.Battery == true)
            {
                RBBatteriesYes.IsChecked = true;
            }
            else
            {
                RBBatteriesNo.IsChecked = true;
            }
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

        //bianca
        private void btnCancelRequest_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen homeScreen = new HomeScreen();
            Close();
            homeScreen.ShowDialog();
        }

    }
}
