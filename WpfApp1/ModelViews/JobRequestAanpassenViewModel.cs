using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Barco
{//jimmy
    public class JobRequestAanpassenViewModel : ViewModelBase
    {
        private JobRequestAanpassen screen;
        private DAO dao;
        public List<Part> parts = new List<Part>();
        public Part selectedPart { get; set; }
        public string txtPartNumber { get; set; } // EUT Partnumber
        public string txtPartNetWeight { get; set; } //net weight
        public string txtPartGrossWeight { get; set; } //gross weight
        public string lblRequestDate { get; set; } //request date
        public string txtLinkTestplan { get; set; } // link to testplan
        public string txtReqInitials { get; set; } // requester initials 
        public string txtEutProjectname { get; set; } //EUT Project name
        public string txtRemark { get; set; } // special remarks
        public string txtFunction { get; set; } //function
        public string txtPvgRes { get; set; }
        public RqRequest CurrentRequest { get; set; }
        public RqOptionel CurrentOptionel { get; set; }
        public RqRequestDetail CurrentRequestDetail { get; set; }
        public DateTime dateExpectedEnd { get; set; }


        public ICommand CancelCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public RqRequest Request { get; set; }
        public RqOptionel RqOptionel { get; set; }
        public RqRequestDetail RqRequestDetail { get; set; }
        public List<String> ListPartsnumbers { get; set; }
        public List<String> ListPartNet { get; set; }
        public List<String> ListPartGross { get; set; }
        public List<RqRequestDetail> rqRequestDetails { get; set; }
        public string selectedDivision { get; set; }
        public string selectedJobNature { get; set; }

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
        public DateTime dateEut1 { get; set; }
        public DateTime dateEut2 { get; set; }
        public DateTime dateEut3 { get; set; }
        public DateTime dateEut4 { get; set; }
        public DateTime dateEut5 { get; set; }
        public string pvgEmc { get; set; }
        public string pvgEnv { get; set; }
        public string pvgRel { get; set; }
        public string pvgSaf { get; set; }
        public string pvgEco { get; set; }

        public bool rbtnBatYes { get; set; }
        public bool rbtnBatNo { get; set; }


        private ObservableCollection<Part> lstParts = new ObservableCollection<Part>(); // for partnumber+ net/gross weight

        public ObservableCollection<Part> listParts
        {
            get { return lstParts; }
        }


        public JobRequestAanpassenViewModel(JobRequestAanpassen screen, int selectedId)
        {
            dao = DAO.Instance();

            this.ListPartsnumbers = new List<string>();
            this.ListPartGross = new List<string>();
            this.ListPartNet = new List<string>();

            CancelCommand = new DelegateCommand(CancelButton);
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            AddCommand = new DelegateCommand(AddPart);
            RemoveCommand = new DelegateCommand(RemovePart);
            this.Request = dao.GetRequest(selectedId);
            this.RqOptionel = dao.GetOptionel(selectedId);
            this.rqRequestDetails = dao.GetRqDetailsWithRequestId(selectedId);

            euts = dao.GetEutWithDetailId(Request.IdRequest);


            


            this.screen = screen;
            CurrentRequest = dao.GetRequest(selectedId);
            CurrentOptionel = dao.GetOptionel(selectedId);
            CurrentRequestDetail = dao.GetRequestDetail(selectedId);
            FillData();


        }
        
        // Sluit aanpassen en opent overview
        public void CancelButton()
        {
            OverviewJobRequest overview = new OverviewJobRequest();
            screen.Close();
           overview.ShowDialog();
        }
        public class Part
        {
            public string partNo { get; set; }
            public string NetWeight { get; set; }
            public string GrossWeight { get; set; }
        }

        public void SaveChanges()
        {
            Request.Requester = txtReqInitials;
            if(txtPvgRes!=null)
            {
                RqRequestDetail.Pvgresp = txtPvgRes;
            }
            if(txtRemark!=null)
            {
                RqOptionel.Remarks = txtRemark;
            }
            
            Request.EutProjectname = txtEutProjectname;
            Request.ExpectedEnddate = dateExpectedEnd.Date;
            RqOptionel.Link = txtLinkTestplan;
            if(rbtnBatYes==true)
            {
                Request.Battery = true;
            }
            else
            {
                Request.Battery=false;
            }
            //Request.BarcoDivision = selectedDivision;
            //Request.JobNature = SelectedJobNature;
            dao.saveChanges();
            MessageBox.Show("The jobrequest is changed");
        }
        /// <summary>
        /// jimmy
        /// </summary>
        public void RemovePart()
        {

            if (parts.Contains(selectedPart))
            {
                parts.Remove(selectedPart);
                lstParts.Remove(selectedPart);
                RefreshGUI();
                OnPropertyChanged();
            }

        }

        /// <summary>
        /// jimmy
        /// </summary>
        public void AddPart()
        {
            try
            {
                if (txtPartNumber == "" || txtPartNetWeight == "" || txtPartGrossWeight == "")
                {
                    MessageBox.Show("please fill in all values");
                }
                else
                {
                    parts.Add(new Part()
                    {
                        NetWeight = txtPartNetWeight,
                        GrossWeight = txtPartGrossWeight,
                        partNo = txtPartNumber

                    });

                    Request.EutPartnumbers += txtPartNumber + " ; ";
                    Request.GrossWeight += txtPartNetWeight + " ; ";
                    Request.NetWeight += txtPartGrossWeight + " ; ";

                    RefreshGUI();
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("please fill in all fields");
            }
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
        //Laden van Jobrequest Net Weight in een list
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
        //Laden van Jobrequest Gross Weight in een list
        public void LoadPartGrossWeight()
        {
            string PartGross = Request.GrossWeight.Replace(" ", String.Empty);
            string GetPartGross;

            do
            {
                int splitIndex = PartGross.IndexOf(";");
                GetPartGross = PartGross.Substring(0, splitIndex);
                ListPartGross.Add(GetPartGross);
                int length = PartGross.Length;


                if (splitIndex != length)
                {
                    PartGross = PartGross.Substring((splitIndex + 1), (PartGross.Length - 1 - splitIndex));

                }

            } while (PartGross.Contains(";"));

        }
        /// <summary>
        /// jimmy
        /// </summary>
        private void fillEuts()
        {
            foreach (Eut e in euts)
            {
                if (e.OmschrijvingEut.Equals("EMC - EUT 1"))
                {
                    cbEmcEut1 = true;
                    cbEmc = true;
                    dateEut1 = e.AvailableDate;

                }
                if (e.OmschrijvingEut.Equals("EMC - EUT 2"))
                {
                    cbEmcEut2 = true;
                    cbEmc = true;
                    dateEut2 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("EMC - EUT 3"))
                {
                    cbEmcEut3 = true;
                    cbEmc = true;
                    dateEut3 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("EMC - EUT 4"))
                {
                    cbEmcEut4 = true;
                    cbEmc = true;
                    dateEut4 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("EMC - EUT 5"))
                {
                    cbEmcEut5 = true;
                    cbEmc = true;
                    dateEut5 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("ENV - EUT 1"))
                {
                    cmEnvironmental = true;
                    cmEnvironmentalEut1 = true;
                    dateEut1 = e.AvailableDate;

                }
                if (e.OmschrijvingEut.Equals("ENV - EUT 2"))
                {
                    cmEnvironmentalEut2 = true;
                    cmEnvironmental = true;
                    dateEut2 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("ENV - EUT 3"))
                {
                    cmEnvironmentalEut3 = true;
                    cmEnvironmental = true;
                    dateEut3 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("ENV - EUT 4"))
                {
                    cmEnvironmentalEut4 = true;
                    cmEnvironmental = true;
                    dateEut4 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("ENV - EUT 5"))
                {
                    cmEnvironmentalEut5 = true;
                    cmEnvironmental = true;
                    dateEut5 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("ECO - EUT 1"))
                {
                    cmGrnCompEut1 = true;
                    cmGrnComp = true;
                    dateEut1 = e.AvailableDate;

                }
                if (e.OmschrijvingEut.Equals("ECO - EUT 2"))
                {
                    cmGrnCompEut2 = true;
                    cmGrnComp = true;
                    dateEut2 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("ECO - EUT 3"))
                {
                    cmGrnCompEut3 = true;
                    cmGrnComp = true;
                    dateEut3 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("ECO - EUT 4"))
                {
                    cmGrnCompEut4 = true;
                    cmGrnComp = true;
                    dateEut4 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("ECO - EUT 5"))
                {
                    cmGrnCompEut5 = true;
                    cmGrnComp = true;
                    dateEut5 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("REL - EUT 1"))
                {
                    cmRelEut1 = true;
                    cmRel = true;
                    dateEut1 = e.AvailableDate;

                }
                if (e.OmschrijvingEut.Equals("REL - EUT 2"))
                {
                    cmRelEut2 = true;
                    cmRel = true;
                    dateEut2 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("REL - EUT 3"))
                {
                    cmRelEut3 = true;
                    cmRel = true;
                    dateEut3 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("REL - EUT 4"))
                {
                    cmRelEut4 = true;
                    cmRel = true;
                    dateEut4 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("REL - EUT 5"))
                {
                    cmRelEut5 = true;
                    cmRel = true;
                    dateEut5 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("SAF - EUT 1"))
                {
                    cmProdSafetyEut1 = true;
                    cmProdSafety = true;
                    dateEut1 = e.AvailableDate;

                }
                if (e.OmschrijvingEut.Equals("SAF - EUT 2"))
                {
                    cmProdSafetyEut2 = true;
                    cmProdSafety = true;
                    dateEut2 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("SAF - EUT 3"))
                {
                    cmProdSafetyEut3 = true;
                    cmProdSafety = true;
                    dateEut3 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("SAF - EUT 4"))
                {
                    cmProdSafetyEut4 = true;
                    cmProdSafety = true;
                    dateEut4 = e.AvailableDate;
                }
                if (e.OmschrijvingEut.Equals("SAF - EUT 5"))
                {
                    cmProdSafetyEut5 = true;
                    cmProdSafety = true;
                    dateEut5 = e.AvailableDate;
                }
            }
        }
        /// <summary>
        /// jimmy
        /// </summary>
        private void fillPvgResp()
        {
            foreach (RqRequestDetail rq in rqRequestDetails)
            {
                if (rq.Testdivisie.Equals("EMC"))
                {
                    pvgEmc = rq.Pvgresp;
                }
                if (rq.Testdivisie.Equals("ENV"))
                {
                    pvgEnv = rq.Pvgresp;
                }
                if (rq.Testdivisie.Equals("REL"))
                {
                    pvgRel = rq.Pvgresp;
                }
                if (rq.Testdivisie.Equals("SAF"))
                {
                    pvgSaf = rq.Pvgresp;
                }
                if (rq.Testdivisie.Equals("ECO"))
                {
                    pvgEco = rq.Pvgresp;
                }

            }
        }
        /// <summary>
        /// jimmy
        /// </summary>
        public Part SelectedPart
        {
            get { return selectedPart; }
            set
            {
                selectedPart = value;
                OnPropertyChanged();
            }
        }

        public string SelectedDivision
        {
            get { return selectedDivision; }
            set
            {
                selectedDivision = value;
                OnPropertyChanged();
            }
        }

        public string SelectedJobNature
        {
            get { return selectedJobNature; }
            set
            {
                selectedJobNature = value;
                OnPropertyChanged();

            }
        }
        /// <summary>
        /// jimmy
        /// </summary>
        private void RefreshGUI()
        {
            lstParts.Clear();
            foreach (Part part in parts)
            {
                lstParts.Add(part);
            }
            LoadPartGrossWeight();
            LoadPartNetWeight();
            LoadPartsNumbers();
        }
        private void SetBatteries()
        {
            if (Request.Battery)
            {
                rbtnBatYes = true;
            }
            else
            {
                rbtnBatNo = true;
            }
        }


        //Stach
        private void SetExpectedEndDate()
        {
            dateExpectedEnd = Request.ExpectedEnddate;
        }

        //Stach
        private void SetRequesterInitials()
        {
            txtReqInitials = Request.Requester;
        }

        //Stach
        private void SettxtEutProjectname()
        {
            txtEutProjectname = Request.EutProjectname;
        }

        //Stach
        private void SetTxtRemark()
        {
            txtRemark = RqOptionel.Remarks;
        }

        //Stach
        private void SetLinkToPlan()
        {
            txtLinkTestplan = RqOptionel.Link;
        }

        private void FillData()
        {
            selectedDivision = Request.BarcoDivision;
            LoadPartGrossWeight();
            LoadPartNetWeight();
            LoadPartsNumbers();
            fillEuts();
            fillPvgResp();
            SetBatteries();
            SetExpectedEndDate();
            SetRequesterInitials();
            SettxtEutProjectname();
            SetTxtRemark();
            SetLinkToPlan();

        }



    }
}
