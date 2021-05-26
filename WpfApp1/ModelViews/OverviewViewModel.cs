using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Barco.ModelViews.Settings;
using Barco.ModelViews.smtpConfig;


namespace Barco
{//bianca
    public class OverviewViewModel : ViewModelBase
    {

        private static AppSettingsService<AppSettings> _appSettingsService = AppSettingsService<AppSettings>.Instance;



        private static SMTPMailCommunication smtpMailCommunication { get; set; }

        public bool mailScheduled = false;

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
            Load();
            this.overview = overview;
            CountJobRequestsToday();
            var result = _appSettingsService.GetConfigurationSection<SMPTClientConfig>("SMPTClientConfig");
            smtpMailCommunication = new SMTPMailCommunication(
                result.QueryResult.Username,
                result.QueryResult.SMTPPassword,
                result.QueryResult.SMPTHost);
        }
        //jimmy
        // laad alle requests in een ObservableCollection om zo in de GUI weer te geven
        public void Load()
        {
            var rqRequests = dao.GetAllRqRequests();
            RqRequests.Clear();
            foreach (var rqRequest in rqRequests)
            {
                RqRequests.Add(rqRequest);
        }
            }
        //bianca
      
        public void CancelButton()
        {
            SendMailWithSMTPRelay();
            //    HomeScreen home = new HomeScreen();
            //    overview.Close();
            //    home.ShowDialog();

        }
        //jimmy
        //Verranderd de Jr status van het geselecteerde request
        public void Approve()
        {

            if (_selectedRequest != null)
            {
                dao.ApproveRqRequest(_selectedRequest);
            }
            else
            {

                MessageBox.Show("Select a JobRequest");
                
            }

        }
        //jimmy
        //opent de Details van de geselecteerde request en geeft het geselecteerde id mee
        public void OpenDetails()
        {
            if (_selectedRequest != null)
            {
                int SelectedId = _selectedRequest.IdRequest;
                JobRequestDetail jobRequestDetail = new JobRequestDetail(SelectedId);
                overview.Close();
                jobRequestDetail.ShowDialog();


            }
            else
            {
                MessageBox.Show("Select a JobRequest");
            }
            


        }
        //jimmy
        //verwijderd de geselecteerd request
        public void DeleteRequest()
        {
            if (_selectedRequest != null)
            {
                dao.DeleteJobRequest(_selectedRequest.IdRequest);
                Load();

            }
            else
            {
                MessageBox.Show("Select a JobRequest");

            }
            
        }
        //jimmy
        //opent de request aanpassen window en geeft de geselecteerde id mee
        public void EditRequest()
        {

            if (_selectedRequest != null)
            {
                int SelectedId = _selectedRequest.IdRequest;

                JobRequestAanpassen jobRequestAanpassen = new JobRequestAanpassen(SelectedId);
                overview.Close();
                jobRequestAanpassen.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Select a JobRequest");

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

        //bianca- method to count the request that were sent today 
        //thibaut
        //laurent - schedule the mailing task
       private int CountJobRequestsToday()
       {
           var datenow = DateTime.Today;
           var count = 0;

            var rqRequests = dao.GetAllRqRequests();

            foreach (var rqRequest in rqRequests)
            {
                TimeSpan span = (TimeSpan) (datenow - rqRequest.RequestDate);
                if (span.Days < 1)
                {
                    count++;
                }
            }
            scheduleMail(count);
            return count;
       }

       //bianca- method to send an email to the responsible once a day s
       public void SendMailWithSMTPRelay()
       {
           smtpMailCommunication.CreateMail(CountJobRequestsToday().ToString());
           var toAddress = _appSettingsService.GetConfigurationSection<EmailAdresses>("EmailAdresses");
           MessageBox.Show(toAddress.QueryResult.Address1);


       }
       
       public void scheduleMail(int count)
       {
           DateTime datenow = DateTime.Now;
           DateTime date = new DateTime(datenow.Year, datenow.Month, datenow.Day, 10, 22, 0);
           TimeSpan span = datenow - date;

           if (!mailScheduled)
           {
               Task.Delay(span).ContinueWith((x) =>
               {
                   smtpMailCommunication.CreateMail(count.ToString());
               });
           }
       }
    }
}
