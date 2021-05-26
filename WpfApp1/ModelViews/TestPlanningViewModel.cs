using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Barco.Data;
using Barco.Views;
using Prism.Commands;

namespace Barco.ModelViews
{
    public class TestPlanningViewModel : ViewModelBase
    {
        private DAO dao;
        private TestPlanning screen;
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public TestPlanningViewModel(TestPlanning screen)
        {
            this.screen = screen;
            SaveCommand = new DelegateCommand(SaveButton);
            CancelCommand = new DelegateCommand(CancelButton);
            dao= DAO.Instance();

        }


        public void SaveButton()
        {
            MessageBox.Show("Congratulations, you have submitted a new test planning.");
            OverviewPlannedTests overviewPlannedTests = new OverviewPlannedTests();
            screen.Close();
            overviewPlannedTests.ShowDialog();
        }

        public void CancelButton()
        {
           PlanningJR planningJr = new PlanningJR();
            screen.Close();

           planningJr.ShowDialog();
        }

    }


}

