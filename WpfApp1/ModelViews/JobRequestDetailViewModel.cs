using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Barco
{//bianca
   public class JobRequestDetailViewModel
    {
        private JobRequestDetail screen;
        public ICommand CancelCommand { get; set; }

        public JobRequestDetailViewModel(JobRequestDetail screen)
        {
            CancelCommand = new DelegateCommand(CloseButton);
            this.screen = screen;

        }

        public void CloseButton()
        {
            OverviewJobRequest overview = new OverviewJobRequest();
            screen.Close();
            overview.ShowDialog();


        }
    }
}
