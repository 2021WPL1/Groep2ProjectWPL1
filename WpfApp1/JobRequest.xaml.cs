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
using System.Linq;



namespace Barco
{
    /// <summary>
    /// Interaction logic for JobRequest.xaml
    /// </summary>
    public partial class JobRequest : Window
    {
        private JobRequestViewModel jobRequestViewModel;
        //remove this line if working with DAO static class
        //private static Barco2021Context DAO = new Barco2021Context();

        private DAO dao;

        //private static Barco2021Context context = new Barco2021Context();

       //private RqRequest request = new RqRequest();
        //private List<Part> parts = new List<Part>();

        public JobRequest()
        {
            InitializeComponent();
            dao = DAO.Instance();
            jobRequestViewModel = new JobRequestViewModel(this);
            DataContext = jobRequestViewModel;



            cmbDivision.ItemsSource = dao.getDivisions();
            cmbDivision.DisplayMemberPath = "Afkorting";
            cmbDivision.SelectedValuePath = "Afkorting";

            cmbJobNature.ItemsSource = dao.getJobNatures();
            cmbJobNature.DisplayMemberPath = "Nature";
            cmbJobNature.SelectedValuePath = "Nature";


        }

        //private void btnAdd_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string sPartNo = txtPartNr.Text;
        //        string sNetWeight = txtNetWeight.Text;
        //        string sGrossWeight = txtGrossWeight.Text;

        //        if (sPartNo == "" || sNetWeight == "" || sGrossWeight == "")
        //        {
        //            MessageBox.Show("please fill in all values");
        //        }
        //        else
        //        {
        //            parts.Add(new Part()
        //            {
        //                NetWeight = txtNetWeight.Text,
        //                GrossWeight = txtGrossWeight.Text,
        //                partNo = txtPartNr.Text
        //            });
        //            refreshGUI();

        //            //lstbNetWeight.Items.Add(sNetWeight);
        //            //lstbGrossWeight.Items.Add(sGrossWeight);

        //            request.EutPartnumbers += sPartNo + " ; ";
        //            request.GrossWeight += sGrossWeight + " ; ";
        //            request.NetWeight += sNetWeight + " ; ";
        //        }

        //    }
        //    catch (NullReferenceException)
        //    {
        //        MessageBox.Show("please fill in all fields");
        //    }

        //}





        //private void btnRemove_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lstParts.SelectedValue.ToString() != null)
        //    {
        //        lstParts.Items.Remove(lstParts.SelectedValue);
        //        parts.Remove((Part)lstParts.SelectedValue);
        //    }
        //}

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> errors = new List<string>();

                string input_Abbreviation = txtReqInitials.Text.ToString();
                string input_ProjectName = txtEutProjectname.Text.ToString();
                bool input_Battery = false;

                //check if radio buttons are checked

                if ((bool)rbtnBatNo.IsChecked)
                {
                    input_Battery = true;
                }
                else if ((bool)rbtnBatNo.IsChecked && (bool)rbtnBatYes.IsChecked)
                {
                    errors.Add("please check if batteries are needed");
                }
                else
                {
                    input_Battery = false;
                }

                if (!dao.IfPersonExists(input_Abbreviation))
                {
                    errors.Add("the requester inititals do not match any employee");
                }

                DateTime input_EndDate = (DateTime)dateExpectedEnd.SelectedDate;


            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                //MessageBox.Show("Please fill in all fields"):
            }
        }



        //private void refreshGUI()
        //{
        //    lstParts.Items.Clear();
        //    foreach (Part part in parts)
        //    {
        //        lstParts.Items.Add(part);
        //    }
        //}




        //public class Part
        //{
        //    public string partNo { get; set; }
        //    public string NetWeight { get; set; }
        //    public string GrossWeight { get; set; }
        //}

        //private void btnCancel_Click(object sender, RoutedEventArgs e)
        //{
        //    HomeScreen homeScreen = new HomeScreen();
        //    Close();
        //    // homeScreen.Show();
        //    //homeScreen.ShowDialog();
        //}
    }

}