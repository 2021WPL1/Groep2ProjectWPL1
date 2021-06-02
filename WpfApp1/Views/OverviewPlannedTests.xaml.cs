using System.Windows;

namespace Barco.Views
{
    /// <summary>
    /// Interaction logic for OverviewPlannedTests.xaml
    /// </summary>
    public partial class OverviewPlannedTests : Window
    {

        private OverviewPlannedTestsViewModel overviewTestModel;

        public OverviewPlannedTests()
        {
            InitializeComponent();
            overviewTestModel = new OverviewPlannedTestsViewModel(this);
            DataContext = overviewTestModel;
        }

    }
}
