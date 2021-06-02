using System.Windows;

namespace Barco
{
    /// <summary>
    /// Interaction logic for Form.xaml
    /// </summary>
    public partial class Form : Window
    {
        public Form()
        {
            InitializeComponent();
            //BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            //imgLoginForm.Source = photo;
        }
        //bianca
        private void LoginForm_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = passwordBox.Password;
            MessageBox.Show("You have successfully logged in!");
            HomeScreen homeScreen = new HomeScreen();
            Close();
            homeScreen.ShowDialog();
        }
        //bianca
        private void CancelForm_Click(object sender, RoutedEventArgs e)
        {
            const string message =
      "Are you sure that you would like to close the form?";
            const string caption = "Form Closing";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                LoginScreen login = new LoginScreen();
                Close();
                login.ShowDialog();
            }
        }
    }
}
