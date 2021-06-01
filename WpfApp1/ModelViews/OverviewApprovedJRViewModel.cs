using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private OverviewApprovedRequests screen;
        private DAO dao;
        //public ObservableCollection<RqRequest> RqApprovedRequests { get; set; }
        private RqTestDevision _selectedTestNature;
        private RqRequest _selectedApprovedRequest;
<<<<<<< HEAD
        public ComboObject _selectedRqRequest { get; set; }

=======
       public ComboObject _selectedRqRequest { get; set; }
>>>>>>> 2309501dd7c5fb02d8774754189a85362e07d773
        public List<RqTestDevision> TestDevisions { get; set; }
        public List<ComboObject> ComboObjects { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand PlanTestCommand { get; set; }
        public List<ComboObject> EMC { get; set; }
        public List<ComboObject> ECO { get; set; }
        public List<ComboObject> ENV { get; set; }
        public List<ComboObject> REL { get; set; }
        public List<ComboObject> SAF { get; set; }
<<<<<<< HEAD

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



=======
>>>>>>> 2309501dd7c5fb02d8774754189a85362e07d773
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
        }
        //jimmy laad alle requests in een ObservableCollection om zo in de GUI weer te geven
        //Bianca --> but only the approved ones
        /*public void Load()
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
<<<<<<< HEAD
            
        }*/

        

        public void replaceInitialList()
        {
=======
            fillList();
        }
        public void replaceInitialList()
        {
            RqApprovedRequests.Clear();
>>>>>>> 2309501dd7c5fb02d8774754189a85362e07d773
            if (SelectedTestNature != null)
            { 
                if (SelectedTestNature.Afkorting == "EMC")
                {
<<<<<<< HEAD
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

                
=======
                    ComboObjects = EMC;
                }
                if (SelectedTestNature.Afkorting == "ECO")
                {
                    ComboObjects = ECO;
                }
                if (SelectedTestNature.Afkorting == "ENV")
                {
                    ComboObjects = ENV;
                }
                if (SelectedTestNature.Afkorting == "REL")
                {
                    ComboObjects = REL;
                }
                if (SelectedTestNature.Afkorting == "SAF")
                {
                    ComboObjects = SAF;
                }
>>>>>>> 2309501dd7c5fb02d8774754189a85362e07d773
            }
            else if (SelectedTestNature is null)
            {
                requests = dao.combinedObjects();
            }
            Refresh();
        }
<<<<<<< HEAD

   
    

=======
>>>>>>> 2309501dd7c5fb02d8774754189a85362e07d773
    // Bianca- used to select a request for a further test planning
        //jimmy-geeft de geselecteerde request terug
        public RqRequest SelectedApprovedRqRequest
        {
            get { return _selectedApprovedRequest; }
            set
            {
                _selectedApprovedRequest = value;
                OnPropertyChanged();
            }
        }
<<<<<<< HEAD

        public void Refresh()
        {
            lstRequests.Clear();
            foreach (ComboObject c in requests)
            {
                lstRequests.Add(c);
            }
        }

=======
>>>>>>> 2309501dd7c5fb02d8774754189a85362e07d773
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
            if (_selectedRqRequest != null)
            {
                var SelectedId = _selectedRqRequest.Request.IdRequest;
                TestPlanning testPlanning = new TestPlanning(SelectedId);
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
        public RqTestDevision SelectedTestNature
        {
            get { return _selectedTestNature; }
            set
            {
                _selectedTestNature = value;
                OnPropertyChanged();
<<<<<<< HEAD
                replaceInitialList();
=======
               replaceInitialList();
>>>>>>> 2309501dd7c5fb02d8774754189a85362e07d773
            }
        }
//bianca-method used to fill in different lists based on the test nature
        public void fillList()
        {
            var initialList = ComboObjects;
            foreach (var request in initialList)
            {
                if (request.TestDivisie.Contains("ECO"))
                {
                    ECO.Add(request);
                }
<<<<<<< HEAD

                if (request.TestDivisie.Contains("ENV"))
=======
                else if (request.TestDivisie.Contains("ENV"))
>>>>>>> 2309501dd7c5fb02d8774754189a85362e07d773
                {
                    ENV.Add(request);
                }
                if (request.TestDivisie.Contains("REL"))
                {
                    REL.Add(request);
                }
                if (request.TestDivisie.Contains("EMC"))
                {
                    EMC.Add(request);
                }
                if (request.TestDivisie.Contains("SAF"))
                {
                    SAF.Add(request);
                }
<<<<<<< HEAD

=======
                else
                {
                    MessageBox.Show("Please select a test nature");
                }
>>>>>>> 2309501dd7c5fb02d8774754189a85362e07d773
            }
        }
        public ComboObject SelectedRqRequest
        {
            get { return _selectedRqRequest; }
            set
            {
                _selectedRqRequest = value;
                OnPropertyChanged();
            }
        }
<<<<<<< HEAD


        

=======
>>>>>>> 2309501dd7c5fb02d8774754189a85362e07d773
    }
}