using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Barco
{
    class HomeScreenViewModel : ViewModelBase
    {

        public ICommand HomeScreenCommand { get; set; }
        public ICommand JobRequestCommand { get; set; }
        public ICommand OverviewCommand { get; set; }
        public ICommand PersonalLeaveCommand { get; set; }
        public ICommand CollectiveLeaveCommand { get; set; }

        public HomeScreenViewModel()
        {
            JobRequestCommand = new DelegateCommand(CreateRequest);
            OverviewCommand = new DelegateCommand(Overview);
            PersonalLeaveCommand = new DelegateCommand(PersonalLeave);
            CollectiveLeaveCommand = new DelegateCommand(CollectiveLeave);
            HomeScreenCommand = new DelegateCommand(HomeScreen);


        }

        public void HomeScreen()
        {
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Close();
        }
    

        public void CreateRequest()
        {
            JobRequest createJobRequest = new JobRequest();
            createJobRequest.ShowDialog();
     
        }

        public void Overview()
        {
            OverviewJobRequest overviewJobRequest = new OverviewJobRequest();
            overviewJobRequest.ShowDialog();
        }

        public void PersonalLeave()
        {
            PersonalLeave personalLeave = new PersonalLeave();

            personalLeave.ShowDialog();
        }

        public void CollectiveLeave()
        {
            CollectiveLeave collectiveLeave = new CollectiveLeave();
            collectiveLeave.ShowDialog();
        }

    }
}
