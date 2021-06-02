using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        private PlPlanning _selectedTest { get; set; }

            //for editing inside vModel
            public ObservableCollection<PlPlanning> lstPlannedTests = new ObservableCollection<PlPlanning>();
            //for iterating and adding
            public List<PlPlanning> tests= new List<PlPlanning>();
            //for binding
            public ObservableCollection<PlPlanning> currentTests
            {
                get
                {
                    return lstPlannedTests;
                }
            }


            public ICommand CancelOverviewCommand{ get; set; }
            public ICommand ChangeStatusCommand { get; set; }



            public OverviewPlannedTestsViewModel(OverviewPlannedTests screen)
            {
                CancelOverviewCommand = new DelegateCommand(CancelOverviewButton);
                ChangeStatusCommand = new DelegateCommand(ChangeStatusButton);
                dao = DAO.Instance();
                this.screen = screen;
               
            }

           

            public void CancelOverviewButton()
            {
                HomeScreen home = new HomeScreen(); 
                screen.Close();
                home.ShowDialog();
            }

            public void ChangeStatusButton()
            {

            }


            public PlPlanning SelectedTest
            {
                get { return _selectedTest; }
                set
                {
                    _selectedTest = value;
                    OnPropertyChanged();
                }
            }
}
}
