using System.Windows;
namespace Barco
{
    /// <summary>
    /// Interaction logic for OverviewJobRequest.xaml
    /// </summary>
    public partial class OverviewJobRequest : Window
    {
        private OverviewViewModel overviewModel;
        public OverviewJobRequest()
        {
            InitializeComponent();
            overviewModel = new OverviewViewModel(this);
            DataContext = overviewModel;
        }
    }
}
