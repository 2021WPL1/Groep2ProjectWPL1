using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Barco.Data;
using Barco.Views;
using Prism.Commands;

namespace Barco.ModelViews
{
    public class PlanningJrViewModel : ViewModelBase
    {
        private DAO dao;

        private PlanningJR screen;

        public ICommand PlanTestCommand { get; set; }

        public PlanningJrViewModel(PlanningJR screen)
        {
            this.screen = screen;
            PlanTestCommand = new DelegateCommand(PlanTestButton);


        }

        public void PlanTestButton()
        {

            TestPlanning testPlanning = new TestPlanning();
            screen.Close();

            testPlanning.ShowDialog();

        }
    }
}