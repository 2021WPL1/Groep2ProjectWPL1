using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Barco
{//bianca
   public class JobRequestDetailViewModel : ViewModelBase
    {
        private JobRequestDetail screen;
        public ICommand CancelCommand { get; set; }
        private DAO dao;
        public RqRequest Request { get; set; }
        public RqOptionel rqOptionel { get; set; }
        public RqRequestDetail rqRequestDetail { get; set; }
       

        public JobRequestDetailViewModel(JobRequestDetail screen, int selectedId)
        {
            CancelCommand = new DelegateCommand(CloseButton);
            dao = DAO.Instance();

            this.Request = dao.getRequest(selectedId);
            this.rqOptionel = dao.getOptionel(selectedId);
            this.rqRequestDetail = dao.getRequestDetail(selectedId);

            this.screen = screen;
            //load(selectedId);

        }

        public void CloseButton()
        {
            OverviewJobRequest overview = new OverviewJobRequest();
            screen.Close();
            overview.ShowDialog();


        }
    }
}
