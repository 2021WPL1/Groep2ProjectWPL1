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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using Barco.ModelViews;

namespace Barco.Views
{
    /// <summary>
    /// Interaction logic for InplannenJobRequest.xaml
    /// </summary>
    public partial class InplannenJobRequest : Window
    {
        private InplannenViewModel ScheduleviewModel;

        public InplannenJobRequest(int id)
        {
            InitializeComponent();
            ScheduleviewModel = new InplannenViewModel(this,id);
            DataContext = ScheduleviewModel;

        }

    
}
}
