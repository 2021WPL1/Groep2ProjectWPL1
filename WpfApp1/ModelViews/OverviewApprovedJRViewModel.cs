﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Barco;
using Barco.Data;
using Barco.ModelViews.Settings;
using Barco.ModelViews.smtpConfig;
using Barco.Views;
using Prism.Commands;

namespace Barco.ModelViews
{ // bianca- show a viewlist with an overview of the approved requests
    public class OverviewApprovedJRViewModel : ViewModelBase
    {
        private static AppSettingsService<AppSettings> _appSettingsService = AppSettingsService<AppSettings>.Instance;

        private DAO dao;
        public ObservableCollection<RqRequest> RqApprovedRequests { get; set; }
        private int _selectedApprovedRequest;
        private string _selectedTestNature;

        public List<RqTestDevision> TestDevisions { get; set; }
        public List<ComboObject> ComboObjects { get; set; }
        private OverviewApprovedRequests screen;
        public ComboObject _SelectedRqRequest { get; set; }

        public ICommand BackCommand { get; set; }
        public ICommand PlanTestCommand { get; set; }


       public List<RqTestDevision> EMC { get; set; }
       public List<RqTestDevision> ECO { get; set; }
       public List<RqTestDevision> ENV { get; set; }
       public List<RqTestDevision> REL { get; set; }
       public List<RqTestDevision> SAF { get; set; }

     

        public OverviewApprovedJRViewModel(OverviewApprovedRequests screen)
        {
           BackCommand = new DelegateCommand(BackButton);
           PlanTestCommand = new DelegateCommand(PlanTestButton);
            dao = DAO.Instance();
            this.screen = screen;
            Load();
            TestDevisions = dao.GetTestNature();
            ComboObjects = dao.combinedObjects();
            EMC = new List<RqTestDevision>();
            ECO = new List<RqTestDevision>();
            ENV = new List<RqTestDevision>();
            REL = new List<RqTestDevision>();
            SAF = new List<RqTestDevision>();
        }



        //jimmy laad alle requests in een ObservableCollection om zo in de GUI weer te geven
        //Bianca --> but only the approved ones
        public void Load()
        {
            var rqApprovedRequests = dao.GetAllApprovedRqRequests();
            RqApprovedRequests = new ObservableCollection<RqRequest>();
            if (rqApprovedRequests.Count > 0)
            {
                RqApprovedRequests.Clear();
                foreach (var rqRequest in rqApprovedRequests)
                {
                    RqApprovedRequests.Add(rqRequest);
                }
            }
        }


      // Bianca-- used to select a request for a further test planning
        //jimmy-geeft de geselecteerde request terugd
        public int SelectedApprovedRqRequest
        {
            get { return _selectedApprovedRequest; }
            set
            {
                _selectedApprovedRequest = value;
                OnPropertyChanged();
            }
        }



        //bianca- cancel the overview screen and open the home screen
        public void BackButton()
        {
            HomeScreen home = new HomeScreen();
            screen.Close();

            home.ShowDialog();


        }

        

        //bianca- when one request is selected, the overview screen is closed and the test planning page is opened
        public void PlanTestButton()
        {

            if (_selectedApprovedRequest != null)
            { 
                int SelectedId = _selectedApprovedRequest;
                TestPlanning testPlanning = new TestPlanning();
                screen.Close();
                testPlanning.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select a JobRequest");
            }
        }

        // Bianca-- used to select a request for a further test planning
        //jimmy-geeft de geselecteerde request terugd
        public string SelectedTestNature
        {
            get { return _selectedTestNature;}
            set
            {
                _selectedTestNature = value;
                OnPropertyChanged();
            }
        }


        public void fillList()
        {


        }

        public ComboObject SelectedRqRequest
        {
            get { return _SelectedRqRequest;}
            set
            {
                _SelectedRqRequest = value;
                OnPropertyChanged();
            }
        }

    }
}