using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Barco.Data;
using Barco.ModelViews.smtpConfig;
using Barco.Views;
using Prism.Commands;

namespace Barco.ModelViews
{
    public class OverviewApprovedJRViewModel : ViewModelBase
    {
        private DAO dao;

        private OverviewApprovedRequests screen;
        public ICommand CancelJRCommand { get; set; }
        public ICommand OpenDetailsCommand { get; set; }

        public OverviewApprovedJRViewModel(OverviewApprovedRequests screen)
        {
            CancelJRCommand = new DelegateCommand(CancelJRButton);
            OpenDetailsCommand = new DelegateCommand(OpenDetails);
            dao = DAO.Instance();
            this.screen = screen;
        }

        public void CancelJRButton()
        {
            HomeScreen home = new HomeScreen();
            screen.Close();

            home.ShowDialog();
            

        }

        public void OpenDetails()
        {
            PlanningJR planningJr= new PlanningJR();
            screen.Close();
            planningJr.ShowDialog();
        }

    }

}
