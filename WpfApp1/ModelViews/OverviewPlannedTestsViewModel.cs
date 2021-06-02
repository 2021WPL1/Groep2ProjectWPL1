using Barco.Data;
using Barco.Views;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Barco.ModelViews
{
    public class OverviewPlannedTestsViewModel
    {
        private DAO dao;
        private OverviewPlannedTests screen;
        public ICommand CancelCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ObservableCollection<PlPlanning> plannings { get; set; }
        

        public OverviewPlannedTestsViewModel(OverviewPlannedTests overviewPlannedTests)
        {
            dao = DAO.Instance();
            screen = overviewPlannedTests;
            CancelCommand = new DelegateCommand(Cancel);
            OpenCommand = new DelegateCommand(Open);

        }

        public void Cancel()
        {
            HomeScreen home = new HomeScreen();
            screen.Close();
            home.Show();
        }

        public void Open()
        {
            //no screen for details yet
        }

        private void load()
        {
            
        }
    }
}
