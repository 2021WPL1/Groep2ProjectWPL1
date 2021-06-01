using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
namespace Barco
{
    class LoginScreenViewModel : ViewModelBase
    {
        public ICommand LoginCommand { get; set; }
        public LoginScreenViewModel()
        {
            LoginCommand = new DelegateCommand(Login);
        }
        public void Login()
        {
            Form f = new Form();
                f.Show();
        }
    }
}
