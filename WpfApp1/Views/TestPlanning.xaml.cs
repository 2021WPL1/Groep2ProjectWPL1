using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Barco.Data;
using Barco.ModelViews;

namespace Barco.Views
{
    /// <summary>
    /// Interaction logic for TestPlanning.xaml
    /// </summary>
    public partial class TestPlanning : Window
    {

        private static DAO dao;

        private TestPlanningViewModel testPlanningJrModel;

        public TestPlanning(int selectedId)
        {

            testPlanningJrModel = new TestPlanningViewModel(this, selectedId);
            DataContext = testPlanningJrModel;
            dao = DAO.Instance();
            InitializeComponent();
            showResources();
        }

        //bianca-display resources in the combobox-TestPlanning
        public void showResources()
        {

            comboboxResources.ItemsSource = dao.GetResource();
            comboboxResources.DisplayMemberPath = "Naam";
            comboboxResources.SelectedValuePath = "Naam";

        }

    }
}
