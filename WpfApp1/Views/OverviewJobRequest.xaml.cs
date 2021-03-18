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
        //private OverviewViewModel overviewModel;
        private DAO dao;

        public OverviewJobRequest()
        {

            InitializeComponent();
            dao = DAO.Instance();
            loadJobRequests();

            //overviewModel = new OverviewViewModel(this);
            //DataContext = overviewModel;

            //BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/logo.png"));
            //imgOverview.Source = photo;


        }
        private void UpdateListBox(ListBox listBox, string display, string value, IEnumerable source)
        {
            listBox.DisplayMemberPath = display;
            listBox.SelectedValuePath = value;
            listBox.ItemsSource = source;
        }

        private void loadJobRequests()
        {
            ICollection<RqRequest> rqRequests = dao.getAllRqRequests();
            UpdateListBox(listOverview, "IdRequest", "IdRequest", rqRequests);
        }

        //zorgt ervoor dat de status van de aangeduide jobrequest naar goedgekeurd gaat
        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                RqRequest rqRequest = dao.getRqRequestById(Convert.ToInt32(listOverview.SelectedValue));
                dao.approveRqRequest(rqRequest);
                MessageBox.Show("Succesfully deleted jobrequest");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //zorgt ervoor dat je de geselecteerde job request kan verwijderen
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dao.deleteOptinel(Convert.ToInt32(listOverview.SelectedValue));
                dao.deleteDetail(Convert.ToInt32(listOverview.SelectedValue));
                dao.deleteJobRequest(Convert.ToInt32(listOverview.SelectedValue));
                loadJobRequests();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //bianca
        //brengt je naar een scherm waar je de aangeduide jobrequest kan aanpassen
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            JobRequestAanpassen jobRequestAanpassen = new JobRequestAanpassen();
            int IdJr = Convert.ToInt32(listOverview.SelectedValue);
            jobRequestAanpassen.ShowDialog(ref IdJr);
            Close();
            jobRequestAanpassen.Show();


        }
        //bianca
        //brengt je terug naar het home screen
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen homeScreen = new HomeScreen();
            Close();
            homeScreen.Show();
            //homeScreen.ShowDialog();

        }

        //opent de geselecteerde job request
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            JobRequestDetail jobRequestDetail = new JobRequestDetail();
            int IdJr = Convert.ToInt32(listOverview.SelectedValue);
            Close();
            jobRequestDetail.ShowDialog();
        }
    }
}
