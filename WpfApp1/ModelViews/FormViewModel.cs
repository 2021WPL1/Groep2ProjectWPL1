using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
namespace Barco
{
    class FormViewModel : ViewModelBase
    { 
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public FormViewModel()
        {
            LoginCommand = new DelegateCommand(Login);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public void Login()
        {
            HomeScreen home = new HomeScreen();
            home.Show();
        }
        public void Cancel()
        {
        }
    }
}
