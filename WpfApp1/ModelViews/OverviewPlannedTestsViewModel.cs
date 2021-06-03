using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
            public ComboBoxItem selectedStatus { get; set; }


        private PlPlanningsKalender _selectedTest { get; set; }
            //for editing inside vModel
            public ObservableCollection<PlPlanningsKalender> lstPlannedTests = new ObservableCollection<PlPlanningsKalender>();
            //for iterating and adding
            public List<PlPlanningsKalender> tests= new List<PlPlanningsKalender>();
            //for binding
            public ObservableCollection<PlPlanningsKalender> currentTests
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
                tests = dao.listPlannings();
                this.screen = screen;
                Refresh();
               
            }


            public void CancelOverviewButton()
            {
                HomeScreen home = new HomeScreen(); 
                screen.Close();
                home.ShowDialog();
            }


            // Bianca-to change the default status of the test
            //Jimmy- confirmation changing status
            public void ChangeStatusButton()
            {
                if (selectedStatus != null)
                {
                    if (SelectedTest != null)
                    {
                        if (MessageBox.Show("Are you sure you want to change the status of the test?", "Change status", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            dao.ChangeStatus(selectedStatus.Content.ToString(), SelectedTest.Id);
                            Refresh();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Pleas select a test first.");
                    }
                }
                else
                {
                    MessageBox.Show("Pleas select a status first.");
                }
            }


            public PlPlanningsKalender SelectedTest
            {
                get { return _selectedTest; }
                set
                {
                    _selectedTest = value;
                    OnPropertyChanged();
                }
            }

        // Bianca- to load all the planned tests in the listview
        public void Refresh()
        {
            lstPlannedTests.Clear();
            foreach (PlPlanningsKalender c in tests)
            {
                lstPlannedTests.Add(c);
            }
        }

    }
}
