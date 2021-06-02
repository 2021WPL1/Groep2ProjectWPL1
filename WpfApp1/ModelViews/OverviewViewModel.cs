using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
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
            //CountJobRequestsToday();
            var result = _appSettingsService.GetConfigurationSection<SMPTClientConfig>("SMPTClientConfig");
            smtpMailCommunication = new SMTPMailCommunication(
                result.QueryResult.Username,
                result.QueryResult.SMTPPassword,
                result.QueryResult.SMPTHost);
        }
        /// <summary>
        /// Laurent, Jimmy, Bianca
        /// </summary>
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
            HomeScreen home = new HomeScreen();
            overview.Close();
            home.ShowDialog();

        }
        //jimmy - thibaut jrnumber toewijzen
        //Verranderd de Jr status van het geselecteerde request
        public void Approve()
        {
            if (_selectedRequest != null)
            {
                if(_selectedRequest.JrNumber == null && !(bool)_selectedRequest.InternRequest )
                {
                    dao.ApproveRqRequest(_selectedRequest, CreateJRNumberForExternal());
                    MessageBox.Show("The request is approved", "Approved", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("The request was already approved", "Approved", MessageBoxButton.OK);
                }
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
        //bianca- method to send an email to the responsible once a day 
        /// <summary>
        /// Thibaut, Bianca
        /// </summary>
        public void SendMailWithSMTPRelay()
       {
           smtpMailCommunication.CreateMail(CountJobRequestsToday().ToString());
           var toAddress = _appSettingsService.GetConfigurationSection<EmailAdresses>("EmailAdresses");
           MessageBox.Show(toAddress.QueryResult.Address1);
       }
        /// <summary>
        /// Laurent, Thibaut
        /// </summary>
        /// <param name="count"></param>
       public void scheduleMail(int count)
       {
           DateTime datenow = DateTime.Now;
           DateTime date = new DateTime(datenow.Year, datenow.Month, datenow.Day, 9, 12, 0);
           if (datenow >= date)
           {
               date = new DateTime(date.Year, date.Month, (date.Day + 1), date.Hour, date.Minute, date.Second);
           }
           TimeSpan span = date - datenow;
           if (!mailScheduled)
           {
               mailScheduled = true;
               Task.Delay(span.Milliseconds).ContinueWith((x) =>
               {
                   smtpMailCommunication.CreateMail(count.ToString());
               });
           }
       }
        /// <summary>
        /// Thibaut
        /// </summary>
        /// <returns></returns>
        private string CreateJRNumberForExternal()
        {
            string result = dao.GetJobNumber(false);
            if (result != null && result != "")
            {
                int value = Convert.ToInt32(result.Substring(2));
                value++;
                result = "EX" + value;
                while (result.Length < 6)
                {
                    result = result.Insert(2, "0");
                }
            }
            else//bij nieuwe DB wordt gereset
            {
                result = "EX0001";
            }
            return result;
        }
    }
}