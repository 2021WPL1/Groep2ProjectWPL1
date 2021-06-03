using System.Windows;
namespace Barco
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Window
    {
        private HomeScreenViewModel homeScreenViewModel;
        //bianca
        public HomeScreen()
        {
            InitializeComponent();
            homeScreenViewModel = new HomeScreenViewModel(this);
            DataContext = homeScreenViewModel;
        }
    }
}
