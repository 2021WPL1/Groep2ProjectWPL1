using Barco.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for PersonalLeave.xaml
    /// </summary>
    public partial class PersonalLeave : Window
    {
        private DAO dao;

        public PersonalLeave()
        {// //bianca
            InitializeComponent();
            BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
           PLBarco.Source = photo;
            dao = DAO.Instance();
            Department.Items.Clear();
            Department.ItemsSource = dao.getDepartment();
            Department.DisplayMemberPath = "Afkorting";
            Department.SelectedValuePath = "Afkorting";


        }

        // //bianca
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen homeScreen = new HomeScreen();
            Close();
            homeScreen.ShowDialog();
        }

        // //bianca
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string firstname = Firstname.Text;
            string lastname = LastName.Text;
            string nameLeader = NameLeader.Text;
        }
       
             
           
         
        

    }
}
