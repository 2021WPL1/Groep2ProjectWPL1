using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Barco.Data;

namespace Barco
{
   public class OverviewViewModel: ViewModelBase
    {
        private OverviewJobRequest overview;
        public ICommand CancelCommand { get; set; }
        public ICommand ApproveCommand { get; set; }
        public ICommand OpenDetailsCommand { get; set; }

        public ObservableCollection<RqRequest> rqRequests { get; set; }
        private DAO dao;
        private RqRequest _selectedRequest;


        public OverviewViewModel(OverviewJobRequest overview)
        {
            dao = DAO.Instance();

            this.rqRequests = new ObservableCollection<RqRequest>();

            CancelCommand = new DelegateCommand(CancelButton);
            ApproveCommand = new DelegateCommand(Approve);
            OpenDetailsCommand = new DelegateCommand(OpenDetails);
            this.overview = overview;
        }

        public void Load()
        {
            var rqRequests = dao.getAllRqRequests();
            //rqRequests.Clear();
            foreach (var rqRequest in rqRequests)
            {
                rqRequests.Add(rqRequest);
            }
        }

        public void CancelButton()
        {
            HomeScreen home = new HomeScreen();
            overview.Close();
            home.ShowDialog();

        }
        public void Approve()
        {
            
                dao.approveRqRequest(_selectedRequest);
            
        }

        public void OpenDetails()
        {
            int SelectedId = _selectedRequest.IdRequest;
            JobRequestDetail jobRequestDetail = new JobRequestDetail(SelectedId);
            overview.Close();
            jobRequestDetail.ShowDialog();
        }

    }
}
