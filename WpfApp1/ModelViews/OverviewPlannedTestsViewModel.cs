using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Barco.Data;
using Barco.Views;
using Prism.Commands;

namespace Barco.ModelViews
{
  public class OverviewPlannedTestsViewModel: ViewModelBase
    {
            private OverviewPlannedTests screen;
            private DAO dao;

            public ICommand CancelOverviewCommand{ get; set; }



            public OverviewPlannedTestsViewModel(OverviewPlannedTests screen)
            {
                CancelOverviewCommand = new DelegateCommand(CancelOverviewButton);
                dao = DAO.Instance();
                this.screen = screen;
               
            }

           

            public void CancelOverviewButton()
            {
                HomeScreen home = new HomeScreen(); 
                screen.Close();
                home.ShowDialog();

            }
  }
}
