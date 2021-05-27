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
using Barco.ModelViews;

namespace Barco.Views
{
    /// <summary>
    /// Interaction logic for PlanningJR.xaml
    /// </summary>
    public partial class PlanningJR : Window
    {
        private PlanningJrViewModel planningJrModel;
        public PlanningJR()
        {
            InitializeComponent();
            planningJrModel = new PlanningJrViewModel(this);
            DataContext = planningJrModel;
        }
    }
}
