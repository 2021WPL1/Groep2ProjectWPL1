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
    /// Interaction logic for OverviewApprovedRequests.xaml
    /// </summary>
    ///
    
    public partial class OverviewApprovedRequests : Window
    {
        private OverviewApprovedJRViewModel overviewApprovedModel;
        private readonly DAO dao;
        public OverviewApprovedRequests()
        {
            InitializeComponent();
           
            overviewApprovedModel = new OverviewApprovedJRViewModel(this);
            DataContext = overviewApprovedModel;
            dao=DAO.Instance();
            showTestNature();
        }

        public void showTestNature()
        {
           cmbTest.ItemsSource = dao.GetTestNature();
            cmbTest.DisplayMemberPath = "Naam";
          cmbTest.SelectedValuePath = "Naam";
        }

    }
}
