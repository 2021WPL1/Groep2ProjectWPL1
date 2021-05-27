using System;
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
        private RqRequest _selectedApprovedRequest;

        private OverviewApprovedRequests screen;

        public ICommand CancelJRCommand { get; set; }
        public ICommand OpenDetailsCommand { get; set; }

        public OverviewApprovedJRViewModel(OverviewApprovedRequests screen)
        {
            CancelJRCommand = new DelegateCommand(CancelJRButton);
            OpenDetailsCommand = new DelegateCommand(OpenDetails);
            dao = DAO.Instance();
            this.screen = screen;
            Load();
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


      // Bianca-- used later to select a request and to show its details in the next screen(PlanningJR)
        //jimmy-geeft de geselecteerde request terugd
        public RqRequest SelectedApprovedRqRequest
        {
            get { return _selectedApprovedRequest; }
            set
            {
                _selectedApprovedRequest = value;
                OnPropertyChanged();
            }
        }



        //bianca- cancel the overview screen and open the home screen
        public void CancelJRButton()
        {
            HomeScreen home = new HomeScreen();
            screen.Close();

            home.ShowDialog();


        }

        

        //bianca- when one request is selected, the overview screen is closed and a new window is opened with the details of the request
        public void OpenDetails()
        {
            



            if (_selectedApprovedRequest != null)
            {
                int SelectedId = _selectedApprovedRequest.IdRequest;
                PlanningJR planningJr = new PlanningJR();
                screen.Close();
                planningJr.ShowDialog();


            }
            else
            {
                MessageBox.Show("Select a JobRequest");
            }
        }
    }
}