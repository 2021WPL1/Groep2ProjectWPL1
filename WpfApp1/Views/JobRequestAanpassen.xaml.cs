using Barco.Data;
using System.Windows;

namespace Barco
{
    /// <summary>
    /// Interaction logic for JobRequestAanpassen.xaml
    /// </summary>
    public partial class JobRequestAanpassen : Window
    {
        private static DAO dao;
        private JobRequestAanpassenViewModel jobRequestAanpassenViewModel;
        public JobRequestAanpassen(int selectedId)
        {
            InitializeComponent();
            dao = DAO.Instance();
            showDivision(selectedId);
            getJobNatures();
            jobRequestAanpassenViewModel = new JobRequestAanpassenViewModel(this, selectedId);
            DataContext = jobRequestAanpassenViewModel;
        }
        public void showDivision(int id)
        {
            comboBoxDivision.ItemsSource = dao.GetDepartment();
            comboBoxDivision.DisplayMemberPath = "Afkorting";
            comboBoxDivision.SelectedValuePath = "Afkorting";
            comboBoxDivision.SelectedValue = dao.GetRqRequestById(id).BarcoDivision;
        }
        public void getJobNatures()
        {
            comboBoxJobNature.ItemsSource = dao.GetJobNatures();
            comboBoxJobNature.DisplayMemberPath = "Nature";
            comboBoxJobNature.SelectedValuePath = "Nature";
        }
    }
}
