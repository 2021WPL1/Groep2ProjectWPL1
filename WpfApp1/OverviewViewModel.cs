using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Barco.Data;
using System.Windows.Media.Imaging;

namespace Barco
{//bianca
   public class OverviewViewModel: ViewModelBase
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

        public void Load()
        {
            var rqRequests = dao.getAllRqRequests();
            RqRequests.Clear();
            foreach (var rqRequest in rqRequests)
            {
                RqRequests.Add(rqRequest);
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
            try
            {

            int SelectedId = _selectedRequest.IdRequest;
            JobRequestDetail jobRequestDetail = new JobRequestDetail(SelectedId);
            overview.Close();
            jobRequestDetail.ShowDialog();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteRequest()
        {
            dao.deleteJobRequest(_selectedRequest.IdRequest);
            Load();
        }

        public void EditRequest()
        {
            
            JobRequestAanpassen jobRequestAanpassen = new JobRequestAanpassen(_selectedRequest);

            jobRequestAanpassen.ShowDialog();
        }
        public RqRequest SelectedRqRequest
        {
            get { return _selectedRequest; }
            set
            {
                _selectedRequest = value;
                OnPropertyChanged();
            }
        }

        public void ShowDetails()
        {

            JobRequestDetail jobRequestDetail = new JobRequestDetail();
            overview.Close();
            jobRequestDetail.ShowDialog();

        }

        public void Edit()
        {
            //JobRequestAanpassen jobRequestAanpassen = new JobRequestAanpassen();
            //int IdJr = Convert.ToInt32(listOverview.SelectedValue);
            //jobRequestAanpassen.ShowDialog(ref IdJr);

        }

        public void Delete()
        {
           // try
        //    {
        //        dao.deleteJobRequest(Convert.ToInt32(listOverview.SelectedValue));


            //        loadJobRequests();
            //    }
            //    catch (SqlException ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            }

        public void Approve()
        {
            //try
            //    //{
            //    //    RqRequest rqRequest = dao.getRqRequestById(Convert.ToInt32(listOverview.SelectedValue));
            //    //    dao.editRequestStatus(rqRequest, "JrStatus", true);

            //    //}
            //    //catch (SqlException ex)
            //    //{

            //    //    MessageBox.Show(ex.Message);
            //    //}

            //}
        }
    }
}
