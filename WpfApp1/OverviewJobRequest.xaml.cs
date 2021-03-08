using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using Barco.Data;
using System.Collections;

namespace Barco 
{
    /// <summary>
    /// Interaction logic for OverviewJobRequest.xaml
    /// </summary>
    public partial class OverviewJobRequest : Window
    {
        private DAO dao;

        public OverviewJobRequest()
        {

            InitializeComponent();
            dao = DAO.Instance();

            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            imgOverview.Source = photo;

            //  MessageBox.Show(context.Person.FirstOrDefault(a => a.Afkorting == "BAS").Afkorting.ToString());


          //   MessageBox.Show(context.RqRequest.FirstOrDefault(a => a.RequestDate == DateTime.Now).RequestDate.ToString());

           
        }
        private void UpdateListBox(ListBox listBox, string display, string value, IEnumerable source)
        {
            listBox.DisplayMemberPath = display;
            listBox.SelectedValuePath = value;
            listBox.ItemsSource = source;
        }

        private void loadJobRequests()
        {
            List<RqRequest> rqRequests = dao.getAllRqRequests();
            UpdateListBox(listOverview, "JrNumber\t -\t BarcoDivision", "IdRequest", rqRequests);
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            RqRequest rqRequest = new dao.getRqRequestById(Convert.ToInt32(listOverview.SelectedValue));
            dao.editRequest(rqRequest, "JrStatus", true);

            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dao.deleteJobRequest(Convert.ToInt32(listOverview.SelectedValue));


                loadJobRequests();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            JobRequestAanpassen jobRequestAanpassen = new JobRequestAanpassen();
            int IdJr = Convert.ToInt32(listOverview.SelectedValue);
            jobRequestAanpassen.ShowDialog(ref IdJr);

            jobRequestAanpassen.ShowDialog();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            JobRequestDetail jobRequestDetail = new JobRequestDetail();
            int IdJR = Convert.ToInt32(listOverview.SelectedValue);
            jobRequestDetail.ShowDialog(ref IdJR);
        }
    }
}
