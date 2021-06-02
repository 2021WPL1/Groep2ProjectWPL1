using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using Barco.Data;
using System.Collections;
namespace Barco
{
    /// <summary>
    /// Interaction logic for OverviewJobRequest.xaml
    /// </summary>
    public partial class OverviewJobRequest : Window
    {
        private OverviewViewModel overviewModel;
        public OverviewJobRequest()
        {
            InitializeComponent();
            overviewModel = new OverviewViewModel(this);
            DataContext = overviewModel;
            //overviewModel.Load();
            //BitmapImage photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "photo/logo.png"));
            //imgOverview.Source = photo;
        }
    }
}
