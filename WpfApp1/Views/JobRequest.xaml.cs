using System.Windows;
using Barco.Data;
namespace Barco
{
    /// <summary>
    /// Interaction logic for JobRequest.xaml
    /// </summary>
    public partial class JobRequest : Window
    {
        private JobRequestViewModel jobRequestViewModel;

        private static DAO dao;
        public JobRequest()
        {
            dao = DAO.Instance();
            InitializeComponent();
            showDivision();
            showJobNatures();
            jobRequestViewModel = new JobRequestViewModel(this);
            DataContext = jobRequestViewModel;
        }
        public void showDivision()
        {
            cmbDivision.Items.Add(getValues("DIVISION"));
        }
        public void showJobNatures()
        {
            cmbJobNature.ItemsSource = dao.GetJobNatures();
            cmbJobNature.DisplayMemberPath = "Nature";
            cmbJobNature.SelectedValuePath = "Nature";
        }
        static string getValues(string Name)
        {
            string userRoot = "HKEY_CURRENT_USER";
            string subkey = "Barco2021";
            string keyName = userRoot + "\\" + subkey;
            return Microsoft.Win32.Registry.GetValue(keyName, Name, "default").ToString();
        }
    }
}