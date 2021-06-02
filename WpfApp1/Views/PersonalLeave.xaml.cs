using Barco.Data;
using System;
using System.Windows;

namespace Barco
{
    /// <summary>
    /// Interaction logic for PersonalLeave.xaml
    /// </summary>
    public partial class PersonalLeave : Window
    {
        private PersonalLeaveViewModel personalLeaveViewModel;
        private DAO dao;
        //bianca
        public PersonalLeave()
        {
            InitializeComponent();
            dao = DAO.Instance();
            personalLeaveViewModel = new PersonalLeaveViewModel(this);
            DataContext = personalLeaveViewModel;
            DateRequest.SelectedDate = DateTime.Today;
            showDivision();
            getFullName();
        }
        //bianca
        public void showDivision()
        {
            Department.Items.Add(getValues("DIVISION"));
            Department.SelectedItem = 0;
        }
    //get the name out of registry
        static string getValues(string Name)
        {
            string userRoot = "HKEY_CURRENT_USER";
            string subkey = "Barco2021";
            string keyName = userRoot + "\\" + subkey;
            return Microsoft.Win32.Registry.GetValue(keyName, Name, "default").ToString();
        }
        public void getFullName()
        {
            string fullName = getValues("NAME");
            string sFirstName = fullName.Split(" ")[0];
            string sLastName = fullName.Split(" ")[1];
            Firstname.Text = sFirstName;
            LastName.Text = sLastName;
        }
    }
}
