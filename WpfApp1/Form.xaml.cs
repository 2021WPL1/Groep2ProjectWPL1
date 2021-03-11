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
using System.Data.SqlClient;
using System.Windows.Shapes;
using System.Data;

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
            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            imgLoginForm.Source = photo;
        }


        //bianca
        private void LoginForm_Click(object sender, RoutedEventArgs e)
        {

            //checken als het account bestaat in de database. (SQLconnection moet aangepast worden voor andere personen en er moeten 
            //passwords en usernames in de database gestoken worden)

            //using(SqlConnection sqlCon = new SqlConnection(@"Data Source=laptop-l66k5ce5\vives; Initial Catalog=Barco2021; Integrated Security=true;"))
            //{
            //    string query = "SELECT COUNT(*) FROM LoginTable WHERE Username=@Username AND Password=@Password";
            //    SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
            //    sqlCommand.CommandType = CommandType.Text;
            //    sqlCommand.Parameters.AddWithValue("@Username", EmailTextBox.Text);
            //    sqlCommand.Parameters.AddWithValue("@Password", passwordBox.Password);
            //    sqlCon.Open();
            //    int count = (int)sqlCommand.ExecuteScalar();
            //    sqlCon.Close();
            
            //    if (count == 1)
            //    {

            //        MessageBox.Show("You have successfully logged in!");

            //        HomeScreen homeScreen = new HomeScreen();
            //        Close();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Username or password are incorrect.");
            //    }
            //}

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
