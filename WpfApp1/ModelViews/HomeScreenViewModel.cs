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
        public ICommand PlannedTestsCommand { get; set; }


        public HomeScreenViewModel(HomeScreen home)
        {
            JobRequestCommand = new DelegateCommand(CreateRequest);
            OverviewCommand = new DelegateCommand(Overview);
            PersonalLeaveCommand = new DelegateCommand(PersonalLeave);
            CollectiveLeaveCommand = new DelegateCommand(CollectiveLeave);
            HomeScreenCommand = new DelegateCommand(HomeScreen);
            ApprovedJobRequestsCommand = new DelegateCommand(ApprovedJobRequests);
            PlannedTestsCommand = new DelegateCommand(PlannedTests);

            this.home = home;
        }

        public void PlannedTests()
        {
            OverviewPlannedTests overviewPlannedTests = new OverviewPlannedTests();
            home.Close();
            overviewPlannedTests.ShowDialog();
        }

        /// <summary>
        /// Laurent 
        /// </summary>
        public void HomeScreen()
        {
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Close();
        }
        /// <summary>
        /// thibaut, Laurent
        /// </summary>
        public void CreateRequest()
        {
            JobRequest createJobRequest = new JobRequest();
            home.Close();
            createJobRequest.ShowDialog();
        }
        /// <summary>
        /// Bianca
        /// </summary>
        public void ApprovedJobRequests()
        {
            OverviewApprovedRequests overviewApproved = new OverviewApprovedRequests();
            home.Close();
            overviewApproved.ShowDialog();
        }
        /// <summary>
        /// thibaut, bianca
        /// </summary>
        public void Approved()
        {
            JobRequest createJobRequest = new JobRequest();
            home.Close();
            createJobRequest.ShowDialog();
        }
        /// <summary>
        /// Laurent
        /// </summary>
        public void Overview()
        {
            OverviewJobRequest overviewJobRequest = new OverviewJobRequest();
            home.Close();
            overviewJobRequest.ShowDialog();
        }
        /// <summary>
        /// Laurent
        /// </summary>
        public void PersonalLeave()
        {
            PersonalLeave personalLeave = new PersonalLeave();
            home.Close();
            personalLeave.ShowDialog();
        }
        /// <summary>
        /// Laurent
        /// </summary>
        public void CollectiveLeave()
        {
            CollectiveLeave collectiveLeave = new CollectiveLeave();
            home.Close();
            collectiveLeave.ShowDialog();
        }
    }
}
