using Microsoft.Data.SqlClient;
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
using System.Data;
using Barco.Data;

namespace Barco 
{
    /// <summary>
    /// Interaction logic for OverviewJobRequest.xaml
    /// </summary>
    public partial class OverviewJobRequest : Window
    {
        private SqlConnection connection;
        public OverviewJobRequest()
        {
            InitializeComponent();
            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            imgOverview.Source = photo;


        }

        //public Barco2021Context context = new Barco2021Context();

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM RqRequest where id = @RequestID";

                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.Parameters.AddWithValue("@RequestID", listOverview.SelectedValue);

                sqlCommand.ExecuteScalar();

                showJobRequests();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void showJobRequests()
        {
            try
            {
                string query = "SELECT JrNumber, BarcoDivision FROM RqRequest";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);

                using (sqlDataAdapter)
                {
                    DataTable JobRequests = new DataTable();

                    sqlDataAdapter.Fill(JobRequests);

                    listOverview.DisplayMemberPath = "JrNumber \t" + " - " + "\t BarcoDivision";

                    listOverview.SelectedValuePath = "Id";

                    listOverview.ItemsSource = JobRequests.DefaultView;

                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
