﻿using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Barco.Data;
using static Barco.JobRequestViewModel;
using System.Collections.ObjectModel;
using System.Windows;

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
        public ICommand CancelCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public RqRequest Request { get; set; }
        public RqOptionel rqOptionel { get; set; }
        public RqRequestDetail rqRequestDetail { get; set; }
        public List<String> ListPartsnumbers { get; set; }
        public List<String> ListPartNet { get; set; }
        public List<String> ListPartGross { get; set; }

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
            AddCommand = new DelegateCommand(RemovePart);
            RemoveCommand = new DelegateCommand(AddPart);
            this.Request = dao.getRequest(selectedId);
            this.rqOptionel = dao.getOptionel(selectedId);
            this.rqRequestDetail = dao.getRequestDetail(selectedId);

            LoadPartGrossWeight();
            LoadPartNetWeight();
            LoadPartsNumbers();


            this.screen = screen;

        }
        //biance
        // Sluit aanpassen en opent overview
        public void CancelButton()
        {
            OverviewJobRequest overview = new OverviewJobRequest();
            screen.Close();
           overview.ShowDialog();
        }

        public void SaveChanges()
        {
           
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
                refreshGUI();
                OnPropertyChanged();
            }

        }

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


                    refreshGUI();

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

            if (Partnumbers.Contains(";"))
            {

                int splitIndex = Partnumbers.IndexOf(";");
                Partnumber = Partnumbers.Substring(0, splitIndex);
                ListPartsnumbers.Add(Partnumber);
                int length = Partnumbers.Length;


                if (splitIndex != length)
                {
                    Partnumbers = Partnumbers.Substring((splitIndex + 1), (Partnumbers.Length - 1 - splitIndex));

                }
            }
            else
            {
                ListPartsnumbers.Add(Partnumbers);

            }

        }
        //Jimmy
        //Laden van Jobrequest Net Weight in een list
        public void LoadPartNetWeight()
        {
            string Partnets = Request.NetWeight.Replace(" ", String.Empty);
            string Partnet;

            if (Partnets.Contains(";"))
            {
                int splitIndex = Partnets.IndexOf(";");
                Partnet = Partnets.Substring(0, splitIndex);
                ListPartNet.Add(Partnet);
                int length = Partnets.Length;


                if (splitIndex != length)
                {
                    Partnets = Partnets.Substring((splitIndex + 1), (Partnets.Length - 1 - splitIndex));

                }

            }
            else
            {
                ListPartNet.Add(Partnets);
    
            } 

        }
        //Jimmy
        //Laden van Jobrequest Gross Weight in een list
        public void LoadPartGrossWeight()
        {
            string PartGross = Request.GrossWeight.Replace(" ", String.Empty);
            string GetPartGross;

            if (PartGross.Contains(";"))
            {
                int splitIndex = PartGross.IndexOf(";");
                GetPartGross = PartGross.Substring(0, splitIndex);
                ListPartGross.Add(GetPartGross);
                int length = PartGross.Length;


                if (splitIndex != length)
                {
                    PartGross = PartGross.Substring((splitIndex + 1), (PartGross.Length - 1 - splitIndex));

                }

            }
            else
            {
                ListPartGross.Add(PartGross);
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
        /// <summary>
        /// jimmy
        /// </summary>
        private void refreshGUI()
        {
            lstParts.Clear();
            foreach (Part part in parts)
            {
                lstParts.Add(part);
            }
        }

    }
}
