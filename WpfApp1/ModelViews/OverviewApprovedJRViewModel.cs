using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Barco.Data;
using Barco.Views;
using Prism.Commands;

namespace Barco.ModelViews
{ // bianca- show a viewlist with an overview of the approved requests
    public class OverviewApprovedJRViewModel : ViewModelBase
    {
        private OverviewApprovedRequests screen;
        private DAO dao;
        //public ObservableCollection<RqRequest> RqApprovedRequests { get; set; }
        private RqTestDevision _selectedTestNature;
        private RqRequest _selectedApprovedRequest;
        public ComboObject _selectedRqRequest { get; set; }

        public List<RqTestDevision> TestDevisions { get; set; }
        public List<ComboObject> ComboObjects { get; set; }

        public ICommand BackCommand { get; set; }
        public ICommand PlanTestCommand { get; set; }


        public List<ComboObject> EMC { get; set; }
        public List<ComboObject> ECO { get; set; }
        public List<ComboObject> ENV { get; set; }
        public List<ComboObject> REL { get; set; }
        public List<ComboObject> SAF { get; set; }

        //for editing inside vModel
        public ObservableCollection<ComboObject> lstRequests = new ObservableCollection<ComboObject>();
        //for iterating and adding
        public List<ComboObject> requests = new List<ComboObject>();
        //for binding
        public ObservableCollection<ComboObject> currentRequests
        {
            get
            {
                return lstRequests;
            }
        }



        public OverviewApprovedJRViewModel(OverviewApprovedRequests screen)
        {
            BackCommand = new DelegateCommand(BackButton);
            PlanTestCommand = new DelegateCommand(PlanTestButton);
            dao = DAO.Instance();
            this.screen = screen;
            TestDevisions = dao.GetTestNature();
            ComboObjects = dao.combinedObjects();
            _selectedRqRequest = new ComboObject();
            EMC = new List<ComboObject>();
            ECO = new List<ComboObject>();
            ENV = new List<ComboObject>();
            REL = new List<ComboObject>();
            SAF = new List<ComboObject>();
            fillList();
            requests = dao.combinedObjects();
            Refresh();
        }
        /// <summary>
        /// Laurent, Bianca, Thibaut
        /// </summary>
        public void replaceInitialList()
        {
            if (SelectedTestNature != null)
            { 
                if (SelectedTestNature.Afkorting == "EMC")
                {
                    requests = EMC;
                }
                if (SelectedTestNature.Afkorting == "ECO")
                {
                    requests = ECO;
                }
                if (SelectedTestNature.Afkorting == "ENV")
                {
                    requests = ENV;
                }
                if (SelectedTestNature.Afkorting == "REL")
                {
                    requests = REL;
                }
                if (SelectedTestNature.Afkorting == "SAF")
                {
                    requests = SAF;
                }

                

                
            }
            
            Refresh();
        }

    //  used to select a request for a further test planning
    /// <summary>
    /// Laurent, Bianca
    /// </summary>
        public RqRequest SelectedApprovedRqRequest
        {
            get { return _selectedApprovedRequest; }
            set
            {
                _selectedApprovedRequest = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Laurent, Bianca
        /// </summary>
        public void Refresh()
        {
            lstRequests.Clear();
            foreach (ComboObject c in requests)
            {
                lstRequests.Add(c);
            }
        }

        //bianca- cancel the overview screen and open the home screen
        public void BackButton()
        {
            HomeScreen home = new HomeScreen();
            screen.Close();

            home.ShowDialog();


        }



        //bianca, Laurent, Thibaut- when one request is selected, the overview screen is closed and the test planning page is opened
        public void PlanTestButton()
        {

            if (_selectedRqRequest.Request != null)
            {
                var SelectedId = _selectedRqRequest.Request.IdRequest;
                TestPlanning testPlanning = new TestPlanning(SelectedId, SelectedTestNature.Afkorting);
                screen.Close();
                testPlanning.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select a JobRequest");
            }           
        }

        //  used to select a request for a further test planning
        /// <summary>
        /// Bianca,Laurent, Thibaut, Jimmy
        /// </summary>
        public RqTestDevision SelectedTestNature
        {
            get { return _selectedTestNature; }
            set
            {
                _selectedTestNature = value;
                OnPropertyChanged();
                replaceInitialList();
            }
        }

      

        //method used to fill in different lists based on the test nature
        /// <summary>
        /// Laurent, Bianca, Thiabaut
        /// </summary>
        public void fillList()
        {
            var initialList = ComboObjects;

            foreach (var request in initialList)
            {
                if (request.RqRequestDetail.Testdivisie.Contains("ECO"))
                {
                    ECO.Add(request);
                }

                if (request.RqRequestDetail.Testdivisie.Contains("ENV"))
                {
                    ENV.Add(request);
                }
                if (request.RqRequestDetail.Testdivisie.Contains("REL"))
                {
                    REL.Add(request);
                }
                if (request.RqRequestDetail.Testdivisie.Contains("EMC"))
                {
                    EMC.Add(request);
                }
                if (request.RqRequestDetail.Testdivisie.Contains("SAF"))
                {
                    SAF.Add(request);
                }
            }
        }
        /// <summary>
        /// Bianca, Laurent, Jimmy
        /// </summary>
        public ComboObject SelectedRqRequest
        {
            get { return _selectedRqRequest; }
            set
            {
                _selectedRqRequest = value;
                OnPropertyChanged();
            }
        }


        

    }
}