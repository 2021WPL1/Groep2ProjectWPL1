﻿using Barco.Data;
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
        private PersonalLeaveViewModel personalLeaveViewModel;

        private DAO dao;

        //bianca
        public PersonalLeave()
        {
            InitializeComponent();
            //BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            //Barco.Source = photo;
            dao = DAO.Instance();
            //showDepartment();
            personalLeaveViewModel = new PersonalLeaveViewModel(this);
            DataContext = personalLeaveViewModel;
            DateRequest.SelectedDate = DateTime.Today;

            showDivision();
            getFullName();


        }

        public void showDivision()
        {
            Department.Items.Add(getValues("DIVISION"));
            Department.SelectedItem = 0;

        }

        public void showName()
        {
            
        }

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


        //bianca
        //private void showDepartment()
        //{
        //    Department.Items.Clear();
        //    Department.ItemsSource = dao.GetDepartment();
        //    Department.DisplayMemberPath = "Afkorting";
        //    Department.SelectedValuePath = "Afkorting";
        //}


        ////bianca
        //private void CancelButton_Click(object sender, RoutedEventArgs e)
        //{
        //    HomeScreen homeScreen = new HomeScreen();
        //    Close();
        //   // homeScreen.Show();
        //}

        //bianca
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            //string firstname = Firstname.Text;
            //string lastname = LastName.Text;
            //string nameLeader = NameLeader.Text;

            
            //MessageBox.Show(DateRequest.SelectedDate.ToString() + "Firstname:" + firstname + " " + "Lastname:" + lastname);
            //MessageBox.Show("NameLeader:" + nameLeader + " "
            //    + "Absent from:" + AbsentFrom.SelectedDate.ToString() +  "Absent Until:" + AbsentUntil.SelectedDate.ToString());
            //MessageBox.Show(TypeOfLeave.SelectionBoxItem.ToString());
        }
       
             
           
         
        

    }
}