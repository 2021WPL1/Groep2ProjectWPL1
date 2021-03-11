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
        private JobRequest screen;
        public ICommand CancelCommand { get; set; }

        public JobRequestViewModel(JobRequest screen)
        {
            CancelCommand = new DelegateCommand(CancelButton);
            this.screen = screen;

        }

        public void CancelButton()
        {           
            HomeScreen home= new HomeScreen();
            screen.Close();
            home.ShowDialog();
          

        }


    }
}
