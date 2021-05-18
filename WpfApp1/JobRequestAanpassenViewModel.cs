using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Barco
{//bianca
   public class JobRequestAanpassenViewModel : ViewModelBase
    {
        private JobRequestAanpassen screen;
        public ICommand CancelCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        private DAO dao;

        public RqOptionel optionel { get; set; }
        public RqRequest request { get; set; }
        public RqRequestDetail requestDetail { get; set; }

        public JobRequestAanpassenViewModel(JobRequestAanpassen screen)
        {
            CancelCommand = new DelegateCommand(CancelButton);
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            AddCommand = new DelegateCommand(RemovePart);
            RemoveCommand = new DelegateCommand(AddPart);

            dao = DAO.Instance();
            this.screen = screen;

        }

        public void load(int requestId)
        {
            request = dao.getRequest(requestId);
            optionel = dao.getOptionel(requestId);
            requestDetail = dao.getRequestDetail(requestId);
            

            OnPropertyChanged();
        }

        public void CancelButton()
        {
            OverviewJobRequest overview = new OverviewJobRequest();
            screen.Close();
           overview.ShowDialog();
        }

        public void SaveChanges()
        {
            OnPropertyChanged();
            dao.saveChanges();
            
        }
        public void RemovePart()
        {

        }

        public void AddPart()
        {

        }

    }
}
