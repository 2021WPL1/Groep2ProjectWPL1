using Barco.ModelViews;
using System.Windows;

namespace Barco.Views
{
    /// <summary>
    /// Interaction logic for OverviewPlannedTests.xaml
    /// </summary>
    public partial class OverviewPlannedTests : Window
    {
        private OverviewPlannedTestsViewModel vm;
        public OverviewPlannedTests()
        {
            InitializeComponent();
            vm = new OverviewPlannedTestsViewModel(this);
            DataContext = vm;
        }
    }
}
