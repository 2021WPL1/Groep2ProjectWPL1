using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Barco.Views;
namespace Barco
{//bianca
    class HomeScreenViewModel : ViewModelBase
    {
        private HomeScreen home;
        public ICommand HomeScreenCommand { get; set; }
        public ICommand JobRequestCommand { get; set; }
        public ICommand OverviewCommand { get; set; }
        public ICommand PersonalLeaveCommand { get; set; }
        public ICommand CollectiveLeaveCommand { get; set; }
        public ICommand ApprovedJobRequestsCommand { get; set; }
        public ICommand PlannedTestCommand { get; set; }
        public HomeScreenViewModel(HomeScreen home)
        {
            PlannedTestCommand = new DelegateCommand(PlannedTest);
            JobRequestCommand = new DelegateCommand(CreateRequest);
            OverviewCommand = new DelegateCommand(Overview);
            PersonalLeaveCommand = new DelegateCommand(PersonalLeave);
            CollectiveLeaveCommand = new DelegateCommand(CollectiveLeave);
            ApprovedJobRequestsCommand = new DelegateCommand(ApprovedJobRequests);

            this.home = home;
        }
        /// <summary>
        /// Bianca
        /// </summary>
        /// link to planned test
        public void PlannedTests()
        {
            OverviewPlannedTests overviewPlannedTests = new OverviewPlannedTests();
            home.Close();
            overviewPlannedTests.ShowDialog();
        }
        /// <summary>
        /// thibaut, Laurent
        /// </summary>
        /// link to create request
        public void CreateRequest()
        {
            JobRequest createJobRequest = new JobRequest();
            home.Close();
            createJobRequest.ShowDialog();
        }
        /// <summary>
        /// Bianca
        /// </summary>
        /// link to overview approved request
        public void ApprovedJobRequests()
        {
            OverviewApprovedRequests overviewApproved = new OverviewApprovedRequests();
            home.Close();
            overviewApproved.ShowDialog();
        }
        /// <summary>
        /// Laurent
        /// </summary>
        /// link to overview request
        public void Overview()
        {
            OverviewJobRequest overviewJobRequest = new OverviewJobRequest();
            home.Close();
            overviewJobRequest.ShowDialog();
        }

        public void PlannedTest()
        {
            OverviewPlannedTests plannedtest = new OverviewPlannedTests();
            home.Close();
            plannedtest.ShowDialog();
        }
        /// <summary>
        /// Laurent
        /// </summary>
        /// link to personal leave
        public void PersonalLeave()
        {
            PersonalLeave personalLeave = new PersonalLeave();
            home.Close();
            personalLeave.ShowDialog();
        }
        /// <summary>
        /// Laurent
        /// </summary>
        /// link to collective leave
        public void CollectiveLeave()
        {
            CollectiveLeave collectiveLeave = new CollectiveLeave();
            home.Close();
            collectiveLeave.ShowDialog();
        }
    }
}
