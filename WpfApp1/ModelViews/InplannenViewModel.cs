using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Barco.Data;
using Barco.Views;
using Prism.Commands;

namespace Barco.ModelViews
{ //bianca
   public class InplannenViewModel
    {
        private InplannenJobRequest screen;

        public ICommand CancelScheduleCommand { get; set; }
        public ICommand SaveScheduleCommand { get; set; }

        private DAO dao;
        public RqRequest Request { get; set; }
        public RqOptionel rqOptionel { get; set; }
        public RqRequestDetail rqRequestDetail { get; set; }
        public List<String> ListPartsnumbers { get; set; }
        public List<String> ListPartNet { get; set; }
        public List<String> ListPartGross { get; set; }

        private List<Eut> euts;
        public bool cbEmcEut1 { get; set; }
        public bool cbEmcEut2 { get; set; }
        public bool cbEmcEut3 { get; set; }
        public bool cbEmcEut4 { get; set; }
        public bool cbEmcEut5 { get; set; }

        public bool cmEnvironmentalEut1 { get; set; }
        public bool cmEnvironmentalEut2 { get; set; }
        public bool cmEnvironmentalEut3 { get; set; }
        public bool cmEnvironmentalEut4 { get; set; }
        public bool cmEnvironmentalEut5 { get; set; }

        public bool cmGrnCompEut1 { get; set; }
        public bool cmGrnCompEut2 { get; set; }
        public bool cmGrnCompEut3 { get; set; }
        public bool cmGrnCompEut4 { get; set; }
        public bool cmGrnCompEut5 { get; set; }

        public bool cmProdSafetyEut1 { get; set; }
        public bool cmProdSafetyEut2 { get; set; }
        public bool cmProdSafetyEut3 { get; set; }
        public bool cmProdSafetyEut4 { get; set; }
        public bool cmProdSafetyEut5 { get; set; }

        public bool cmRelEut1 { get; set; }
        public bool cmRelEut2 { get; set; }
        public bool cmRelEut3 { get; set; }
        public bool cmRelEut4 { get; set; }
        public bool cmRelEut5 { get; set; }

        public bool cbEmc { get; set; }
        public bool cmEnvironmental { get; set; }
        public bool cmRel { get; set; }
        public bool cmProdSafety { get; set; }
        public bool cmGrnComp { get; set; }
        public string dateEut1 { get; set; }
        public string dateEut2 { get; set; }
        public string dateEut3 { get; set; }
        public string dateEut4 { get; set; }
        public string dateEut5 { get; set; }
        public bool rbtnBatNo { get; set; }
        public bool rbtnBatYes { get; set; }



        public InplannenViewModel(InplannenJobRequest screen,int selectedId)
        
        {
            CancelScheduleCommand = new DelegateCommand(CloseScheduleButton);
            SaveScheduleCommand = new DelegateCommand(SaveScheduleButton);
            this.screen = screen;
            dao= DAO.Instance();
            this.Request = dao.GetRequest(selectedId);
        }


        //bianca-closing schedule/inplannen and opening JobRequestDetail
        public void CloseScheduleButton()
        {

                JobRequestDetail jobRequestDetail = new JobRequestDetail(Request.IdRequest);
                screen.Close();
                jobRequestDetail.ShowDialog();


          
        }

        public void SaveScheduleButton()
        {

        }
    }
}
