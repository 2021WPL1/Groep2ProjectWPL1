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
        private JobRequestAanpassenViewModel jobRequestAanpassenViewModel;

        private int SelectedId;
        private DAO dao;

        public JobRequestAanpassen(int selectedId) : base()
        {
            InitializeComponent();
            dao = DAO.Instance();
            SelectedId = selectedId;
            DataContext = jobRequestAanpassenViewModel;
            load(SelectedId); 



        }

        //laad de gegevens in van een jobrequest op basis van het id
        private void load(int selectedId)
        {
            RqRequest req = dao.getRequest(selectedId);
            RqRequestDetail reqdet = dao.getRequestDetail(selectedId);
            Eut eut = dao.getEut(reqdet.IdRqDetail);
            RqOptionel optionel = dao.getOptionel(selectedId);


            txtRequisterInitials.Text = req.Requester;
            comboBoxJobNature.Text = req.JobNature;
            comboBoxDivision.Text = req.BarcoDivision;
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

            txtLinkToTestPlan.Text = optionel.Link;
            txtSpecialRemarks.Text = optionel.Remarks;

            string s = req.EutPartnumbers;
            do
            {
                ListBoxPartNumber.Items.Add(s.Substring(0, s.IndexOf(";")));
                s = s.Substring(s.IndexOf(";") + 1);

            } while (s.Contains(";"));
            ListBoxPartNumber.Items.Add(s);


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


        public void ShowDialog(ref int IdJr)
        { 
                int Idjr = IdJr;
                RqRequest rqRequest = dao.getRqRequestById(Idjr);
                RqRequestDetail requestDetail = dao.getRqRequestDetailById(Idjr);
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
           
        }
    }
}
