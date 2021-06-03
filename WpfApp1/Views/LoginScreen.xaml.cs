using System.Windows;
namespace Barco
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        private LoginScreenViewModel loginVM;
        public LoginScreen()
        {
            InitializeComponent();
            loginVM = new LoginScreenViewModel();
            DataContext = loginVM;
        }
    }
}
