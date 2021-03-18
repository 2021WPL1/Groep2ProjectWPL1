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
            dao = DAO.Instance(); //TODO bij oproepen van constructor id mee geven
            //er moet data in de database zijn om dit te doen werken
            
        }

        //laad de gegevens in van een jobrequest op basis van het id
        private void load(ref int IdJr)
        {
            //RqRequest req = dao.getRequest(selectedId);
            //RqRequestDetail reqdet = dao.getRequestDetail(selectedId);
            //Eut eut = dao.getEut(reqdet.IdRqDetail);
            //RqOptionel optionel = dao.getOptionel(selectedId);


            //txtRequisterInitials.Text = req.Requester;
            //txtJobNature.Text = req.JobNature;
            //txtDevision.Text = req.BarcoDivision;
            //txtProjectName.Text = req.EutProjectname;
            //lblRequestDate.Content = req.RequestDate;
            //lblExpectedEndDate.Content = req.ExpectedEnddate;
            //lblJobRequestNumber.Content = req.JrNumber;
            //if (req.Battery == true)
            //{
            //    RBBatteriesYes.IsChecked = true;
            //}
            //else
            //{
            //    RBBatteriesNo.IsChecked = true;
            //}

            //txtLinkToTestPlan.Text = optionel.Link;
            //txtSpecialRemarks.Text = optionel.Remarks;

            ////do-while extra aanmaken voor partweight(net- en gross-) met de nieuwe database
            //string s = req.EutPartnumbers;

            //do
            //{
            //    ListBoxPartNumber.Items.Add(s.Substring(0, s.IndexOf(";")));
            //    s = s.Substring(s.IndexOf(";") + 1);

            //} while (s.Contains(";"));
            //ListBoxPartNumber.Items.Add(s);

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
                txtDevision.Text = rqRequest.BarcoDivision;
                txtJobNature.Text = rqRequest.JobNature;
                lblRequestDate.Content = rqRequest.RequestDate;
                lblJobRequestNumber.Content = rqRequest.JrNumber;
                txtPvgRes.Text = requestDetail.Pvgresp;
                lblExpectedEndDate.Content = rqRequest.ExpectedEnddate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

       
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            OverviewJobRequest overview = new OverviewJobRequest();
            Close();
            overview.Show();
        }
    }
}
