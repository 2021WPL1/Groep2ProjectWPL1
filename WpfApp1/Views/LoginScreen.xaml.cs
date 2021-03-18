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
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        private LoginScreenViewModel loginVM;
        public LoginScreen()
        {
            InitializeComponent();
            //BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            //imgLogin.Source = photo;
            loginVM = new LoginScreenViewModel();
            DataContext = loginVM;
        }

        
        //bianca
        private void LoginScreen_Click(object sender, RoutedEventArgs e)
        {
            Form form = new Form();
            Close();
            form.ShowDialog();
        }
    }
}
