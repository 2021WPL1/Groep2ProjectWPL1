using System.Windows;
using Barco.Data;
using Barco.ModelViews;

namespace Barco.Views
{
    /// <summary>
    /// Interaction logic for TestPlanning.xaml
    /// </summary>
    public partial class TestPlanning : Window
    {
        private static DAO dao;
        private TestPlanningViewModel testPlanningJrModel;
        public TestPlanning(int selectedId, string testDiv)
        {
            testPlanningJrModel = new TestPlanningViewModel(this, selectedId, testDiv);
            DataContext = testPlanningJrModel;
            dao = DAO.Instance();
            InitializeComponent();
            //showResources();
        }
        //bianca-display resources in the combobox-TestPlanning
        public void showResources()
        {
            comboboxResources.ItemsSource = dao.GetResource();
            comboboxResources.DisplayMemberPath = "Naam";
            comboboxResources.SelectedValuePath = "Naam";
        }
    }
}
