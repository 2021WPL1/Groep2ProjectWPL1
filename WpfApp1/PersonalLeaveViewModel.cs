using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Barco
{//bianca
   public class PersonalLeaveViewModel: ViewModelBase
    {
        private PersonalLeave personalLeave;
        public ICommand CancelCommand { get; set; }


     

        public PersonalLeaveViewModel(PersonalLeave personalLeave)
        {
            CancelCommand = new DelegateCommand(CancelButton);
            this.personalLeave = personalLeave;
        }

        public void CancelButton()
        {
            HomeScreen home = new HomeScreen();
            personalLeave.Close();
            home.ShowDialog();


        }
    }
}
