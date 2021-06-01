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
        public ICommand SaveTestCommand { get; set; }
        public ICommand CancelTestCommand { get; set; }

        public DateTime dateExpectedStart { get; set; }
        public DateTime dateExpectedEnd { get; set; }


        public TestPlanningViewModel(TestPlanning screen)
        {
            this.screen = screen;
            SaveTestCommand = new DelegateCommand(SaveButton);
            CancelTestCommand = new DelegateCommand(CancelButton);
            dao= DAO.Instance();
            dateExpectedStart = DateTime.Now;
            dateExpectedEnd = DateTime.Now;

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
            OverviewApprovedRequests overview = new OverviewApprovedRequests();
            screen.Close();
            overview.ShowDialog();
        }

    }


}

