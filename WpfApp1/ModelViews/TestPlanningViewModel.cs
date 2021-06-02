using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Barco.Data;
using Barco.Views;
using Org.BouncyCastle.Operators;
using Prism.Commands;
namespace Barco.ModelViews
{
    public class TestPlanningViewModel : ViewModelBase
    {
        private DAO dao;
        private TestPlanning screen;
        public ICommand SaveTestCommand { get; set; }
        public ICommand CancelTestCommand { get; set; }
        public PlPlanningsKalender planning { get; set; }
        public RqRequest request;
        public RqRequestDetail requestDetail;


        public string Omschrijving { get; set; }

        //for editing inside vModel
        public ObservableCollection<PlResources> lstResources = new ObservableCollection<PlResources>();

        //for iterating and adding
        public List<PlResources> resources = new List<PlResources>();
        public PlResources resource;

        //for binding
        public ObservableCollection<PlResources> currentResources
        {
            get { return lstResources; }
        }



        private PlResources _selectedResouce { get; set; }
        public List<PlResources> Resources { get; set; }

        public ICommand AddResourceCommand { get; set; }
        public DateTime dateExpectedStart { get; set; }
        public DateTime dateExpectedEnd { get; set; }
        public string DueDate { get; set; }
       // public string Omschrijving { get; set; }
     //   public TestPlanningViewModel(TestPlanning screen, int selectedId, string testDiv);



     //   private List<string> errors { get; set; }

        public TestPlanningViewModel(TestPlanning screen, int selectedId,string testDiv)
        {
            this.screen = screen;
            SaveTestCommand = new DelegateCommand(SaveButton);
            CancelTestCommand = new DelegateCommand(CancelButton);
            AddResourceCommand = new DelegateCommand(AddResourceButton);
            dao = DAO.Instance();
            dateExpectedStart = DateTime.Now;
            dateExpectedEnd = DateTime.Now;
            Resources = new List<PlResources>();
            populateResources(testDiv);
            _selectedResouce = new PlResources();
      

        }

        //Bianca
        public void SaveButton()
        {
            //check if a start date is selected
            if (dateExpectedStart.Date != null)
            {
                if (dateExpectedStart.Date >= DateTime.Today)
                {
                    planning.Startdatum = dateExpectedStart.Date;
                }
                else
                {
                        
                    MessageBox.Show("The start date has to be in the future");
                }

            }
            else
            {
                MessageBox.Show("please specify a start date");
            }
       

            //check if an end date is selected
            if (dateExpectedEnd.Date != null)
            {
                if (dateExpectedEnd.Date >= DateTime.Today)
                {
                    planning.Einddatum = dateExpectedEnd.Date;
                }
                else
                {

                    MessageBox.Show("The end date has to be in the future");
                }
            }

            
            else
            {
                MessageBox.Show("please specify an end date");
            }


            //check if the resources are selected
            if (selectedResource == null)
            {
                MessageBox.Show("select a resource");
            }
            else
            {
               // foreach (PlResources  resource in currentResources)
              //  {
                   planning.Resources = selectedResource.Id;
              //  } 
              //still unclear how to put the resources in the database 
            }

            if (Omschrijving == null)
            {
                MessageBox.Show("Please give a description of the test");
            }
            else
            {
                planning.Omschrijving = Omschrijving;
            }

            planning.IdRequest = request.IdRequest;
            planning.JrNr = request.JrNumber;
            planning.JrStatus = request.JrStatus;
            planning.Testdiv = requestDetail.Testdivisie;
            dao.AddPlanToCalendar(planning);
            MessageBox.Show("Congratulations, you have submitted a new test planning.");
            OverviewPlannedTests overviewPlannedTests = new OverviewPlannedTests();
            screen.Close();
            overviewPlannedTests.ShowDialog();
        }










        public void CancelButton()
        {
            OverviewApprovedRequests overview = new OverviewApprovedRequests();
            screen.Close();
            overview.ShowDialog();
        }
        public void populateResources(string testDiv)
        {
            //Resources = dao.GetResource(); //alle resources
            Resources = dao.GetResourcesForTestDiv(testDiv);//resources per testDivision
        }
        public void AddResourceButton()
        {
            if (!String.IsNullOrEmpty(_selectedResouce.Naam) )
            {
                resources.Add(_selectedResouce);
                refresh();
            }
        }
        public void refresh()
        {
            lstResources.Clear();
            foreach (PlResources resource in resources)
            {
                lstResources.Add(resource);
            }
        }


        public PlResources selectedResource
        {
            get
            {
                return _selectedResouce;
            }
            set
            {
                _selectedResouce = value;
                OnPropertyChanged();
            }
        }
    }
}


