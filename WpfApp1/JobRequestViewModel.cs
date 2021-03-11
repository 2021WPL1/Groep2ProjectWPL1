using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Barco
{
  public class JobRequestViewModel: ViewModelBase
    {
        public ICommand CancelCommand { get; set; }

        public JobRequestViewModel()
        {
            CancelCommand = new DelegateCommand(CancelButton);

        }

        public void CancelButton()
        {
            JobRequest jobRequest = new JobRequest();
            jobRequest.Close();
           // Application.Current.MainWindow.Close();
        }


    }
}
