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
        public string dateEut1 { get; set; }
        public string dateEut2 { get; set; }
        public string dateEut3 { get; set; }
        public string dateEut4 { get; set; }
        public string dateEut5 { get; set; }

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
            fillEuts();

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

        //thibaut 
        //transfers the data from the database to the eut checkboxes
        private void fillEuts()
        {
            foreach (Eut e in euts)
            {
                if (e.OmschrijvingEut.Equals("EMC - EUT 1"))
                {
                    cbEmcEut1 = true;
                    cbEmc = true;
                    dateEut1 = e.AvailableDate.ToString();
                    
                }
                if (e.OmschrijvingEut.Equals("EMC - EUT 2"))
                {
                    cbEmcEut2 = true;
                    cbEmc = true;
                    dateEut2 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("EMC - EUT 3"))
                {
                    cbEmcEut3 = true;
                    cbEmc = true;
                    dateEut3 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("EMC - EUT 4"))
                {
                    cbEmcEut4 = true;
                    cbEmc = true;
                    dateEut4 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("EMC - EUT 5"))
                {
                    cbEmcEut5 = true;
                    cbEmc = true;
                    dateEut5 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Environmental - EUT 1"))
                {
                    cmEnvironmental = true;
                    cmEnvironmentalEut1 = true;
                    dateEut1 = e.AvailableDate.ToString();

                }
                if (e.OmschrijvingEut.Equals("Environmental - EUT 2"))
                {
                    cmEnvironmentalEut2 = true;
                    cmEnvironmental = true;
                    dateEut2 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Environmental - EUT 3"))
                {
                    cmEnvironmentalEut3 = true;
                    cmEnvironmental = true;
                    dateEut3 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Environmental - EUT 4"))
                {
                    cmEnvironmentalEut4 = true;
                    cmEnvironmental = true;
                    dateEut4 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Environmental - EUT 5"))
                {
                    cmEnvironmentalEut5 = true;
                    cmEnvironmental = true;
                    dateEut5 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Green Compliance - EUT 1"))
                {
                    cmGrnCompEut1 = true;
                    cmGrnComp = true;
                    dateEut1 = e.AvailableDate.ToString();

                }
                if (e.OmschrijvingEut.Equals("Green Compliance - EUT 2"))
                {
                    cmGrnCompEut2 = true;
                    cmGrnComp = true;
                    dateEut2 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Green Compliance - EUT 3"))
                {
                    cmGrnCompEut3 = true;
                    cmGrnComp = true;
                    dateEut3 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Green Compliance - EUT 4"))
                {
                    cmGrnCompEut4 = true;
                    cmGrnComp = true;
                    dateEut4 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Green Compliance - EUT 5"))
                {
                    cmGrnCompEut5 = true;
                    cmGrnComp = true;
                    dateEut5 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Reliability - EUT 1"))
                {
                    cmRelEut1 = true;
                    cmRel = true;
                    dateEut1 = e.AvailableDate.ToString();

                }
                if (e.OmschrijvingEut.Equals("Reliability - EUT 2"))
                {
                    cmRelEut2 = true;
                    cmRel = true;
                    dateEut2 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Reliability - EUT 3"))
                {
                    cmRelEut3 = true;
                    cmRel = true;
                    dateEut3 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Reliability - EUT 4"))
                {
                    cmRelEut4 = true;
                    cmRel = true;
                    dateEut4 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Reliability - EUT 5"))
                {
                    cmRelEut5 = true;
                    cmRel = true;
                    dateEut5 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Product Safety - EUT 1"))
                {
                    cmProdSafetyEut1 = true;
                    cmProdSafety = true;
                    dateEut1 = e.AvailableDate.ToString();

                }
                if (e.OmschrijvingEut.Equals("Product Safety - EUT 2"))
                {
                    cmProdSafetyEut2 = true;
                    cmProdSafety = true;
                    dateEut2 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Product Safety - EUT 3"))
                {
                    cmProdSafetyEut3 = true;
                    cmProdSafety = true;
                    dateEut3 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Product Safety - EUT 4"))
                {
                    cmProdSafetyEut4 = true;
                    cmProdSafety = true;
                    dateEut4 = e.AvailableDate.ToString();
                }
                if (e.OmschrijvingEut.Equals("Product Safety - EUT 5"))
                {
                    cmProdSafetyEut5 = true;
                    cmProdSafety = true;
                    dateEut5 = e.AvailableDate.ToString();
                }
            }
            
                

            

        }


    }
}
