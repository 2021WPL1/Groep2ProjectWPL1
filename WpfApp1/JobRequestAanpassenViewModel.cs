using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Barco.Data;

namespace Barco
{//bianca
   public class JobRequestAanpassenViewModel : ViewModelBase
    {
        private JobRequestAanpassen screen;
        private DAO dao;
        public ICommand CancelCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public RqRequest Request;


        public JobRequestAanpassenViewModel(JobRequestAanpassen screen, int selectedId)
        {
            dao = DAO.Instance();

            CancelCommand = new DelegateCommand(CancelButton);
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            AddCommand = new DelegateCommand(RemovePart);
            RemoveCommand = new DelegateCommand(AddPart);
            this.Request = dao.getRequest(selectedId);


            this.screen = screen;

        }

        public void CancelButton()
        {
            OverviewJobRequest overview = new OverviewJobRequest();
            screen.Close();
           overview.ShowDialog();
        }

        public void SaveChanges()
        {

        }
        public void RemovePart()
        {

        }

        public void AddPart()
        {

        }

    }
}
