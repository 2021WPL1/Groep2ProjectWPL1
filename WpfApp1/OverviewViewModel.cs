using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Barco.Data;

namespace Barco
{//bianca
   //public class OverviewViewModel: ViewModelBase
   // {
   //     private OverviewJobRequest overview;
   //     public ICommand CancelCommand { get; set; }
   //     public ICommand OpenCommand { get; set; }
   //     public ICommand EditCommand { get; set; }
   //     public ICommand DeleteCommand { get; set; }
   //     public ICommand ApproveCommand { get; set; }



   //     public OverviewViewModel(OverviewJobRequest overview)
   //     {
   //         CancelCommand = new DelegateCommand(CancelButton);
   //         OpenCommand = new DelegateCommand(ShowDetails);
   //         EditCommand = new DelegateCommand(Edit);
   //         DeleteCommand = new DelegateCommand(Delete);
   //         ApproveCommand = new DelegateCommand(Approve);
   //         this.overview = overview;
   //     }

   //     public void CancelButton()
   //     {
   //         HomeScreen home = new HomeScreen();
   //         overview.Close();
   //         home.ShowDialog();

   //     }

   //     public void ShowDetails()
   //     {

   //         JobRequestDetail jobRequestDetail = new JobRequestDetail();
   //         overview.Close();
   //         jobRequestDetail.ShowDialog();

   //     }

   //     public void Edit()
   //     {
   //         JobRequestAanpassen jobRequestAanpassen = new JobRequestAanpassen();
   //         int IdJr = Convert.ToInt32(listOverview.SelectedValue);
   //         jobRequestAanpassen.ShowDialog(ref IdJr);

   //     }

   //     public void Delete()
   //     {
   //         try
   //         {
   //             dao.deleteJobRequest(Convert.ToInt32(listOverview.SelectedValue));


   //                 loadJobRequests();
   //             }
   //             catch (SqlException ex)
   //             {
   //                 MessageBox.Show(ex.Message);
   //             }
   //         }

   //     public void Approve()
   //     {
   //         try
   //         {
   //             RqRequest rqRequest = dao.getRqRequestById(Convert.ToInt32(listOverview.SelectedValue));
   //             dao.editRequestStatus(rqRequest, "JrStatus", true);

   //         }
   //         catch (SqlException ex)
   //         {

   //             MessageBox.Show(ex.Message);
   //         }

   //     }
   // }
    
}
