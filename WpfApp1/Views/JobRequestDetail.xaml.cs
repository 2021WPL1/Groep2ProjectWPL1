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
    /// Interaction logic for JobRequestDetail.xaml
    /// </summary>
    ///         private DAO dao;

    public partial class JobRequestDetail : Window
    {
        private JobRequestDetailViewModel DetailsviewModel;

        private int SelectedId;
        public JobRequestDetail(int selectedId): base ()
        {
            InitializeComponent();
            DetailsviewModel = new JobRequestDetailViewModel(this, SelectedId);
            DataContext = DetailsviewModel;
            //DetailsviewModel.Load(selectedId);

            
        }

        //laad de gegevens in van een jobrequest op basis van het id
 

    }
}
