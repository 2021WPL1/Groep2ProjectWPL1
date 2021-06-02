using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
namespace Barco
{
    public class CollectiveLeaveViewModel : ViewModelBase
    {
        private CollectiveLeave collectiveForm;
        public ICommand CancelCommand { get; set; }
        public CollectiveLeaveViewModel(CollectiveLeave collectiveForm)
        { 
            CancelCommand = new DelegateCommand(CancelButton);
            this.collectiveForm = collectiveForm;
        }
        public void CancelButton()
        {
            HomeScreen home = new HomeScreen();
            collectiveForm.Close();
            home.ShowDialog();
        }
    }
}
