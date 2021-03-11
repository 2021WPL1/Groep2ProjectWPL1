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
        private OverviewViewModel overviewModel;
        private DAO dao;

        public OverviewJobRequest()
        {

            InitializeComponent();
            dao = DAO.Instance();
            //loadJobRequests();

           overviewModel = new OverviewViewModel(this);
            DataContext = overviewModel;
            overviewModel.Load();

            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            imgOverview.Source = photo;


        }
        //private void UpdateListBox(ListBox listBox, string display, string value, IEnumerable source)
        //{
        //    listBox.DisplayMemberPath = display;
        //    listBox.SelectedValuePath = value;
        //    listBox.ItemsSource = source;
        //}

        //private void loadJobRequests()
        //{

        //    ICollection<RqRequest> rqRequests = dao.getAllRqRequests();
        //    UpdateListBox(listOverview, "JrNumber", "Id", rqRequests);

            
        //}
        //Jimmy
        // changes JrStatus to "Approved".
        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            RqRequest rqRequest = dao.getRqRequestById(Convert.ToInt32(listOverview.SelectedValue)+2);
            dao.approveRqRequest(rqRequest);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //Jimmy
        // Deletes a Job Request and reloads the list.
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dao.deleteJobRequest(Convert.ToInt32(listOverview.SelectedValue)+2);


                overviewModel.Load();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //bianca
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedId = Convert.ToInt32(listOverview.SelectedValue)+2;
                JobRequestAanpassen jobRequestAanpassen = new JobRequestAanpassen(selectedId);
                jobRequestAanpassen.ShowDialog();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




    }
}
