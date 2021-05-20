using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Barco
{//jimmy
   public class JobRequestDetailViewModel : ViewModelBase
    {
        private JobRequestDetail screen;
        public ICommand CancelCommand { get; set; }
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


        public JobRequestDetailViewModel(JobRequestDetail screen, int selectedId)
        {
            CancelCommand = new DelegateCommand(CloseButton);
            dao = DAO.Instance();

            this.ListPartsnumbers = new List<string>();
            this.ListPartGross = new List<string>();
            this.ListPartNet = new List<string>();
            this.Request = dao.GetRequest(selectedId);
            this.rqOptionel = dao.GetOptionel(selectedId);
            this.rqRequestDetail = dao.GetRequestDetail(selectedId);
            LoadPartsNumbers();
            LoadPartGrossWeight();
            LoadPartNetWeight();
            euts = dao.GetEutWithDetailId(rqRequestDetail.IdRqDetail);
            this.screen = screen;


        }
        //Biance
        //Sluit Details en open de overview
        public void CloseButton()
        {
            OverviewJobRequest overview = new OverviewJobRequest();
            screen.Close();
            overview.ShowDialog();


        }
        //Jimmy
        //Laden van Jobrequest Partnumbers in een list
        public void LoadPartsNumbers()
        {
            string Partnumbers = Request.EutPartnumbers.Replace(" ", String.Empty);
            string Partnumber;

            do
            {
                int splitIndex = Partnumbers.IndexOf(";");
                Partnumber = Partnumbers.Substring(0, splitIndex);
                ListPartsnumbers.Add(Partnumber);
                int length = Partnumbers.Length;


                if (splitIndex != length)
                {
                    Partnumbers = Partnumbers.Substring((splitIndex + 1), (Partnumbers.Length - 1 - splitIndex));

                }

            } while (Partnumbers.Contains(";"));
            
                


            
        }
        //Jimmy
        //Laden van Jobrequest net weights in een list
        public void LoadPartNetWeight()
        {
            string Partnets = Request.NetWeight.Replace(" ", String.Empty);
            string Partnet;

            do
            {
                int splitIndex = Partnets.IndexOf(";");
                Partnet = Partnets.Substring(0, splitIndex);
                ListPartNet.Add(Partnet);
                int length = Partnets.Length;


                if (splitIndex != length)
                {
                    Partnets = Partnets.Substring((splitIndex + 1), (Partnets.Length - 1 - splitIndex));

                }

            } while (Partnets.Contains(";"));


        }
        //Jimmy
        //Laden van Jobrequest Grossweight in een list
        public void LoadPartGrossWeight()
        {
            string partGross = Request.GrossWeight.Replace(" ", String.Empty);
            string getPartGross;

            do
            {

                int splitIndex = partGross.IndexOf(";");
                getPartGross = partGross.Substring(0, splitIndex);
                ListPartGross.Add(getPartGross);
                int length = partGross.Length;


                if (splitIndex != length)
                {
                    partGross = partGross.Substring((splitIndex + 1), (partGross.Length - 1 - splitIndex));

                }

            }
            while (partGross.Contains(";"));

        }

        private void fillEuts()
        {
            foreach(Eut e in euts)
            {
                if(e.Equals("EMC - EUT 1"))
                {

                }
            }
            //get first eut and date
            if (DatePickerEUT1.Date != DateTime.Now)
            {
                DateTime date = (DateTime)DatePickerEUT1.Date;
                string description = "";
                if ((bool)cbEmcEut1)
                {
                    description = 
                    result.Add(createEut(description, date));
                }

                if ((bool)cmEnvironmentalEut1)
                {
                    description = "Environmental - EUT 1";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmGrnCompEut1)
                {
                    description = "Green Compliance - EUT 1";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmRelEut1)
                {
                    description = "Reliability - EUT 1";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmProdSafetyEut1)
                {
                    description = "Product Safety - EUT 1";
                    result.Add(createEut(description, date));
                }

            }

            //get second eut and date
            if (DatePickerEUT2.Date != DateTime.Now)
            {
                DateTime date = (DateTime)DatePickerEUT2.Date;
                string description = "";
                if ((bool)cbEmcEut2)
                {
                    description = "EMC - EUT 2";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmEnvironmentalEut2)
                {
                    description = "Environmental - EUT 2";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmGrnCompEut2)
                {
                    description = "Green Compliance - EUT 2";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmRelEut2)
                {
                    description = "Reliability - EUT 2";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmProdSafetyEut2)
                {
                    description = "Product Safety - EUT 2";
                    result.Add(createEut(description, date));
                }

            }

            //get third eut and date
            if (DatePickerEUT3.Date != DateTime.Now)
            {
                DateTime date = (DateTime)DatePickerEUT3.Date;
                string description = "";
                if ((bool)cbEmcEut3)
                {
                    description = "EMC - EUT 3";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmEnvironmentalEut3)
                {
                    description = "Environmental - EUT 3";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmGrnCompEut3)
                {
                    description = "Green Compliance - EUT 3";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmRelEut3)
                {
                    description = "Reliability - EUT 3";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmProdSafetyEut3)
                {
                    description = "Product Safety - EUT 3";
                    result.Add(createEut(description, date));
                }


            }

            //get fourth eut and date
            if (DatePickerEUT4.Date != DateTime.Now)
            {
                DateTime date = (DateTime)DatePickerEUT4.Date;
                string description = "";
                if ((bool)cbEmcEut4)
                {
                    description = "EMC - EUT 4";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmEnvironmentalEut4)
                {
                    description = "Environmental - EUT 4";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmGrnCompEut4)
                {
                    description = "Green Compliance - EUT 4";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmRelEut4)
                {
                    description = "Reliability - EUT 4";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmProdSafetyEut4)
                {
                    description = "Product Safety - EUT 4";
                    result.Add(createEut(description, date));
                }
            }

            //get fifth eut and date
            if (DatePickerEUT5.Date != DateTime.Now)
            {
                DateTime date = (DateTime)DatePickerEUT5.Date;
                string description = "";
                if ((bool)cbEmcEut5)
                {
                    description = "EMC - EUT 5";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmEnvironmentalEut5)
                {

                    description = "Environmental - EUT 5";
                    result.Add(createEut(description, date));
                }


                if ((bool)cmGrnCompEut5)
                {
                    description = "Green Compliance - EUT 5";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmRelEut5)
                {
                    description = "Reliability - EUT 5";
                    result.Add(createEut(description, date));
                }

                if ((bool)cmProdSafetyEut5)
                {
                    description = "Product Safety - EUT 5";
                    result.Add(createEut(description, date));
                }


            }

        }


    }
}
