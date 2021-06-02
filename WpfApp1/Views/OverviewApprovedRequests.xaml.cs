using System.Windows;
using Barco.Data;
using Barco.ModelViews;
namespace Barco.Views
{
    /// <summary>
    /// Interaction logic for OverviewApprovedRequests.xaml
    /// </summary>
    ///
    public partial class OverviewApprovedRequests : Window
    {
        private OverviewApprovedJRViewModel overviewApprovedModel;
        private readonly DAO dao;
        public OverviewApprovedRequests()
        {
            InitializeComponent();
            overviewApprovedModel = new OverviewApprovedJRViewModel(this);
            DataContext = overviewApprovedModel;
            dao=DAO.Instance();
            showTestNature();
        }
        public void showTestNature()
        {
           cmbTest.ItemsSource = dao.GetTestNature();
           cmbTest.DisplayMemberPath = "Afkorting";
          cmbTest.SelectedValuePath = "Afkorting";
        }
    }
}
