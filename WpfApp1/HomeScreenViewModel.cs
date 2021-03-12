using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

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

        public HomeScreenViewModel(HomeScreen home)
        {
            JobRequestCommand = new DelegateCommand(CreateRequest);
            OverviewCommand = new DelegateCommand(Overview);
            PersonalLeaveCommand = new DelegateCommand(PersonalLeave);
            CollectiveLeaveCommand = new DelegateCommand(CollectiveLeave);
            HomeScreenCommand = new DelegateCommand(HomeScreen);
            this.home = home;


        }

        public void HomeScreen()
        {
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Close();
        }
    

        public void CreateRequest()
        {
            JobRequest createJobRequest = new JobRequest();
            home.Close();
            createJobRequest.ShowDialog();
     
        }

        public void Overview()
        {
            OverviewJobRequest overviewJobRequest = new OverviewJobRequest();
            home.Close();
            overviewJobRequest.ShowDialog();
        }

        public void PersonalLeave()
        {
            PersonalLeave personalLeave = new PersonalLeave();
            home.Close();
            personalLeave.ShowDialog();
        }

        public void CollectiveLeave()
        {
            CollectiveLeave collectiveLeave = new CollectiveLeave();
            home.Close();
            collectiveLeave.ShowDialog();
        }

    }
}
