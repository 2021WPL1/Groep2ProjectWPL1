using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Barco
{
    /// <summary>
    /// Interaction logic for Form.xaml
    /// </summary>
    public partial class Form : Window
    {
        private FormViewModel formVM;
        public Form()
        {
            InitializeComponent();
            //BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            //imgLoginForm.Source = photo;

            formVM = new FormViewModel();
            DataContext = formVM;
        }

        /*
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
        */

        
    }
}
