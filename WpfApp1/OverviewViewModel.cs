using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Barco
{
   public class OverviewViewModel: ViewModelBase
    {
        private OverviewJobRequest overview;
        public ICommand CancelCommand { get; set; }


        public OverviewViewModel(OverviewJobRequest overview)
        {
            CancelCommand = new DelegateCommand(CancelButton);
            this.overview = overview;
        }

        public void CancelButton()
        {
            HomeScreen home = new HomeScreen();
            overview.Close();
            home.ShowDialog();

        }

    }
}
