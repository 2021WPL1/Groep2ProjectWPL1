using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Barco
{//bianca
    public class OverviewViewModel : ViewModelBase
    {
        private OverviewJobRequest overview;
        public ICommand CancelCommand { get; set; }
        public ICommand ApproveCommand { get; set; }
        public ICommand OpenDetailsCommand { get; set; }
        public ICommand DeleteRequestCommand { get; set; }

        public ICommand EditRequestCommand { get; set; }

        public ObservableCollection<RqRequest> RqRequests { get; set; }
        private DAO dao;
        private RqRequest _selectedRequest;


        public OverviewViewModel(OverviewJobRequest overview)
        {
            dao = DAO.Instance();

            this.RqRequests = new ObservableCollection<RqRequest>();

            CancelCommand = new DelegateCommand(CancelButton);
            ApproveCommand = new DelegateCommand(Approve);
            OpenDetailsCommand = new DelegateCommand(OpenDetails);
            DeleteRequestCommand = new DelegateCommand(DeleteRequest);
            EditRequestCommand = new DelegateCommand(EditRequest);
            this.overview = overview;
        }
        //jimmy
        // laad alle requests in een ObservableCollection om zo in de GUI weer te geven
        public void Load()
        {
            var rqRequests = dao.getAllRqRequests();
            RqRequests.Clear();
            foreach (var rqRequest in rqRequests)
            {
                RqRequests.Add(rqRequest);
            }


        }
        //biance
        //Sluit de overview en opent home
        public void CancelButton()
        {
            HomeScreen home = new HomeScreen();
            overview.Close();
            home.ShowDialog();

        }
        //jimmy
        //Verranderd de Jr status van het geselecteerde request
        public void Approve()
        {
            try
            {

                dao.approveRqRequest(_selectedRequest);
            }
            catch (NullReferenceException ex)
            {

                MessageBox.Show(ex.Message + "Select a JobRequest");
            }


        }
        //jimmy
        //opent de Details van de geselecteerde request en geeft het geselecteerde id mee
        public void OpenDetails()
        {
            try
            {
                int SelectedId = _selectedRequest.IdRequest;
                JobRequestDetail jobRequestDetail = new JobRequestDetail(SelectedId);
                overview.Close();
                jobRequestDetail.ShowDialog();

            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message + "Select a JobRequest");
            }


        }
        //jimmy
        //verwijderd de geselecteerd request
        public void DeleteRequest()
        {
            try
            {
                dao.deleteJobRequest(_selectedRequest.IdRequest);
                Load();

            }
            catch (NullReferenceException ex)
            {

                MessageBox.Show(ex.Message + "Select a JobRequest");
            }
        }
        //jimmy
        //opent de request aanpassen window en geeft de geselecteerde id mee
        public void EditRequest()
        {
            try
            {

                int SelectedId = _selectedRequest.IdRequest;

                JobRequestAanpassen jobRequestAanpassen = new JobRequestAanpassen(SelectedId);
                overview.Close();
                jobRequestAanpassen.ShowDialog();
                
            }
            catch (NullReferenceException ex)
            {

                MessageBox.Show(ex.Message + "Select a JobRequest");
            }
        }
        //jimmy
        //geeft de geselecteerde request terug
        public RqRequest SelectedRqRequest
        {
            get { return _selectedRequest; }
            set
            {
                _selectedRequest = value;
                OnPropertyChanged();
            }
        }


    }
}
