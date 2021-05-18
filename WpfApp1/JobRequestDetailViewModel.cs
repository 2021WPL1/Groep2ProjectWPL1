using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Barco
{//bianca
   public class JobRequestDetailViewModel
    {
        private DAO dao;
        private JobRequestDetail screen;
        public ICommand CancelCommand { get; set; }

        public JobRequestDetailViewModel(JobRequestDetail screen)
        {
            int i = 0;
            dao = DAO.Instance();
            CancelCommand = new DelegateCommand(CloseButton);
            this.screen = screen;
            load(i);
        }

        public void CloseButton()
        {
            OverviewJobRequest overview = new OverviewJobRequest();
            screen.Close();
            overview.ShowDialog();
        }

        private void load(int requestId)
        {
            RqRequest request = dao.getRequest(requestId);
            RqOptionel optionel = dao.getOptionel(requestId);
            RqRequestDetail requestDetail = dao.getRequestDetail(requestId);
            Eut eut = dao.getEut(requestDetail.IdRqDetail);


        }
    }
}
