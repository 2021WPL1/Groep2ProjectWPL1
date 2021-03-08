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
        //remove this line if working with DAO static class
        private static Barco2021Context DAO = new Barco2021Context();


        private static Barco2021Context context = new Barco2021Context();
        public JobRequest()
        {
            InitializeComponent();

            cmbDivision.ItemsSource = DAO.RqBarcoDivision.ToList();
            cmbDivision.DisplayMemberPath = "Afkorting";
            cmbDivision.SelectedValuePath = "Afkorting";

            cmbJobNature.ItemsSource = DAO.RqJobNature.ToList();
            cmbJobNature.DisplayMemberPath = "Nature";
            cmbJobNature.SelectedValuePath = "Nature";
        }

        /*BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
        imgJobRequest.Source = photo;*/



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RqRequest request = new RqRequest();
                List<string> errors = new List<string>();

                string input_Abbreviation = txtReqInitials.Text.ToString();
                
                if(!DAO.Person.Any(s => s.Afkorting == input_Abbreviation))
                {
                    errors.Add("the requester inititals do not match any employee");
                }





            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                //MessageBox.Show("Please fill in all fields"):
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close(); 
        }
    }
}
