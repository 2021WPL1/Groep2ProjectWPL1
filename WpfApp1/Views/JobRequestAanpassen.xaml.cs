using Barco.Data;
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

namespace Barco
{
    /// <summary>
    /// Interaction logic for JobRequestAanpassen.xaml
    /// </summary>
    public partial class JobRequestAanpassen : Window
    {
        private JobRequestAanpassenViewModel jobRequestAanpassenViewModel;


        public JobRequestAanpassen(int selectedId)
        {
            InitializeComponent();
            jobRequestAanpassenViewModel = new JobRequestAanpassenViewModel(this, selectedId);
            DataContext = jobRequestAanpassenViewModel;



        }
    }
}
