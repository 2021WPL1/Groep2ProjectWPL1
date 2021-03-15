﻿using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Barco
{ //bianca
    public class JobRequestViewModel : ViewModelBase
    {
        public JobRequest screen;


        public RqRequest request = new RqRequest();


        public Part selectedPart { get; set; }

        // buttons : Add/Remove, Send/Cancel
        public ICommand CancelCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public string txtPartNr { get; set; }             // EUT Partnumber
        public string txtNetWeight { get; set; }          //net weight
        public string txtGrossWeight { get; set; }       //gross weight
        public string txtLinkTestplan { get; set; }      // link to testplan
        public string txtReqInitials { get; set; }       // requester initials 
        public string txtEutProjectname { get; set; }   //EUT Project name
        public string txtRemark { get; set; }           // special remarks
        public DateTime dateExpectedEnd { get; set; }


        // EUT foreseen availability date
        public DateTime DatePickerEUT1 { get; set; }
        public DateTime DatePickerEUT2 { get; set; }
        public DateTime DatePickerEUT3 { get; set; }
        public DateTime DatePickerEUT4 { get; set; }
        public DateTime DatePickerEUT5 { get; set; }

        private ObservableCollection<Part> lstParts = new ObservableCollection<Part>();// for partnumber+ net/gross weight
        
        public ObservableCollection<Part> listParts { get { return lstParts; } }

        public List<ListView> err_output { get; set; } // listview for errors


        //remove this line if working with DAO static class
        //private static Barco2021Context DAO = new Barco2021Context();

        public Barco.Data.DAO dao;
        public JobRequestViewModel jobRequestViewModel;
        public RqOptionel optional = new RqOptionel();
        public List<Eut> eutList = new List<Eut>();
        public RqRequestDetail Detail = new RqRequestDetail();
        public List<Part> parts = new List<Part>();


         List<CheckBox> emcBoxes = new List<CheckBox>();
         List<CheckBox> envBoxes = new List<CheckBox>();
         List<CheckBox> relBoxes = new List<CheckBox>();
         List<CheckBox> prodBoxes = new List<CheckBox>();
         List<CheckBox> greenBoxes = new List<CheckBox>();
         List<CheckBox> selectionBoxes = new List<CheckBox>();



        public bool cbEmc { get; set; }
        public bool cmEnvironmental { get; set; }
        public bool cmRel { get; set; }
        public bool cmProdSafety { get; set; }
        public bool cmGrnComp { get; set; }

        public ComboBox cmbDivision { get; set; }
        public ComboBox cmbJobNature { get; set; }



        public bool rbtnBatNo { get; set; }
        public bool rbtnBatYes { get; set; }

        public JobRequestViewModel(JobRequest screen)
        {
            CancelCommand = new DelegateCommand(CancelButton);
            SendCommand = new DelegateCommand(SendButton);
            AddCommand = new DelegateCommand(AddButton);
            RemoveCommand = new DelegateCommand(RemoveButton);


            this.screen = screen;

        }


    

        public Part SelectedPart
        {
            get { return selectedPart; }
            set { selectedPart = value; }
        }

        public void CancelButton()
        {
            HomeScreen home = new HomeScreen();
            screen.Close();
            home.ShowDialog();
        }



        public void AddButton()
        { 
            try
            {
                if (txtPartNr == "" || txtNetWeight == "" || txtGrossWeight == "")
                {
                    MessageBox.Show("please fill in all values");
                }
                else
                {
                    parts.Add(new Part()
                    {
                        NetWeight = txtNetWeight,
                        GrossWeight = txtGrossWeight,
                        partNo = txtPartNr
                        
                    });

                    request.EutPartnumbers += txtPartNr + " ; ";
                    request.GrossWeight += txtNetWeight + " ; ";
                    request.NetWeight += txtGrossWeight + " ; ";


                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("please fill in all fields");
            }
        }


        //bianca
        public void RemoveButton()
        {
            if (lstParts.Contains(selectedPart))
            {
                lstParts.Remove(selectedPart);
            }
        }

        public void SendButton()
        {
            try
            {
                //create error sequence
                List<string> errors = new List<string>();

                //declare vars for object
                string input_Abbreviation = txtReqInitials;
                string input_ProjectName = txtEutProjectname;

                bool input_Battery = false;

                if (dateExpectedEnd.Date != null)
                {
                    DateTime input_EndDate = (DateTime)dateExpectedEnd.Date;

                }
                else
                {
                    errors.Add("please specify a end date");
                }

                string specialRemarks = txtRemark;

                string netWeights = "";
                string grossWeights = "";
                string partNums = "";

                //parts section
                if (parts.Count > 0)
                {
                    foreach (Part part in parts)
                    {
                        netWeights += part.NetWeight + "; ";
                        grossWeights += part.GrossWeight + "; ";
                        partNums += part.partNo + "; ";
                    }
                }
                else
                {
                    errors.Add("Please add parts to test");
                }


                //check if radio buttons are checked

                if ((bool)rbtnBatNo)
                {
                    input_Battery = true;
                }
                else if ((bool)rbtnBatNo && (bool)rbtnBatYes)
                {
                    errors.Add("please check if batteries are needed");
                }
                else
                {
                    input_Battery = false;
                }

                //check if requester exists

                if (!dao.IfPersonExists(input_Abbreviation))
                {
                    errors.Add("the requester initials do not match any employee");
                }

                //check if the job nature is selected
                //checkbox area
                if (!(bool)cbEmc && !(bool)cmEnvironmental && !(bool)cmRel &&
                     !(bool)cmProdSafety&& !(bool)cmGrnComp)
                {
                    errors.Add("Please select a job nature");
                }
                else
                {
                    List<string> validation = ValidateCheckboxes();
                    errors.AddRange(validation);
                }

                //check if the dates are set
                List<string> valiDate = checkDates();
                    errors.AddRange(valiDate);





                //check if other fields are empty

                if (txtEutProjectname is null)
                {
                    errors.Add("please fill in a project name");
                }
                //error handling
                if (errors.Count > 0)
                {
                    // err_output.ItemsSource = errors;
                }
                else
                {
                    //request object 
                    request.Requester = input_Abbreviation;
                    request.BarcoDivision = cmbDivision.SelectedValue.ToString();
                    request.JobNature = cmbJobNature.SelectedValue.ToString();
                    request.RequestDate = DateTime.Now;
                    request.EutProjectname = txtEutProjectname;
                    request.Battery = input_Battery;
                    request.ExpectedEnddate = (DateTime)dateExpectedEnd.Date;
                    request.NetWeight = netWeights;
                    request.GrossWeight = grossWeights;
                    request.EutPartnumbers = partNums;

                    //optional object
                    optional.Link = txtLinkTestplan;
                    optional.IdRequest = request.IdRequest;

                    //eut objects
                    //  eutList = getEutData();


                }


            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                //MessageBox.Show("Please fill in all fields"):
            }

        }

        // to show in the listview
        public class Part
        {
            public string partNo { get; set; }
            public string NetWeight { get; set; }
            public string GrossWeight { get; set; }
        }


        /*
        public void createBoxLists()
         {

             emcBoxes.Add(cbEmcEut1);
             emcBoxes.Add(cbEmcEut2);
             emcBoxes.Add(cbEmcEut3);
             emcBoxes.Add(cbEmcEut4);
             emcBoxes.Add(cbEmcEut5);

             envBoxes.Add(cmEnvorimentalEut1);
             envBoxes.Add(cmEnvorimentalEut2);
             envBoxes.Add(cmEnvorimentalEut3);
             envBoxes.Add(cmEnvorimentalEut4);
             envBoxes.Add(cmEnvorimentalEut5);

             relBoxes.Add(cmRelEut1);
             relBoxes.Add(cmRelEut2);
             relBoxes.Add(cmRelEut3);
             relBoxes.Add(cmRelEut4);
             relBoxes.Add(cmRelEut5);

             prodBoxes.Add(cmProdSafetyEut1);
             prodBoxes.Add(cmProdSafetyEut2);
             prodBoxes.Add(cmProdSafetyEut3);
             prodBoxes.Add(cmProdSafetyEut4);
             prodBoxes.Add(cmProdSafetyEut5);

             greenBoxes.Add(cmGrnCompEut1);
             greenBoxes.Add(cmGrnCompEut2);
             greenBoxes.Add(cmGrnCompEut3);
             greenBoxes.Add(cmGrnCompEut4);
             greenBoxes.Add(cmGrnCompEut5);

             selectionBoxes.Add(cmEnvorimental);
             selectionBoxes.Add(cbEmc);
             selectionBoxes.Add(cmRel);
             selectionBoxes.Add(cmProdSafety);
             selectionBoxes.Add(cmGrnComp);
         }
*/


        private void enableBoxes(CheckBox selected)
        {
            List<CheckBox> targets = new List<CheckBox>();
            if (cbEmc)
            {
                targets = emcBoxes;
            }
            else if (cmEnvironmental)
            {
                targets = envBoxes;
            }
            else if ( cmRel)
            {
                targets = relBoxes;
            }
            else if (cmProdSafety)
            {
                targets = prodBoxes;
            }
            else if (cmGrnComp)
            {
                targets = greenBoxes;
            }

            foreach (CheckBox box in targets)
            {
                box.IsEnabled = true;
            }
        }




        private List<string> ValidateCheckboxes()
         {
             List<string> outcome = new List<string>();
             if ((bool)cbEmc)
             {
                 int counter = 0;
                 foreach (CheckBox box in emcBoxes)
                 {
                     if ((bool)box.IsChecked)
                     {
                         counter++;
                     }
                 }
                 if (counter != 1)
                 {
                     outcome.Add("please check emc data");
                 }
             }
             if ((bool)cmEnvironmental)
             {
                 int counter = 0;
                 foreach (CheckBox box in envBoxes)
                 {
                     if ((bool)box.IsChecked)
                     {
                         counter++;
                     }
                 }
                 if (counter != 1)
                 {
                     outcome.Add("please check environmental data");
                 }
             }
             if ((bool)cmRel)
             {
                 int counter = 0;
                 foreach (CheckBox box in relBoxes)
                 {
                     if ((bool)box.IsChecked)
                     {
                         counter++;
                     }
                 }
                 if (counter != 1)
                 {
                     outcome.Add("please check reliability data");
                 }
             }
             if ((bool)cmProdSafety)
             {
                 int counter = 0;
                 foreach (CheckBox box in prodBoxes)
                 {
                     if ((bool)box.IsChecked)
                     {
                         counter++;
                     }
                 }
                 if (counter != 1)
                 {
                     outcome.Add("please check product safety data");
                 }
             }
             if ((bool)cmGrnComp)
             {
                 int counter = 0;
                 foreach (CheckBox box in greenBoxes)
                 {
                     if ((bool)box.IsChecked)
                     {
                         counter++;
                     }
                 }
                 if (counter != 1)
                 {
                     outcome.Add("please check green compliance data");
                 }
             }
             return outcome;
         }


        public bool cbEmcEut1 { get; set; }
        public bool cbEmcEut2 { get; set; }
        public bool cbEmcEut3 { get; set; }
        public bool cbEmcEut4 { get; set; }
        public bool cbEmcEut5 { get; set; }


        public bool cmEnvorimentalEut1 { get; set; }
        public bool cmEnvorimentalEut2 { get; set; }
        public bool cmEnvorimentalEut3 { get; set; }
        public bool cmEnvorimentalEut4 { get; set; }
        public bool cmEnvorimentalEut5 { get; set; }



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




        private List<string> checkDates()
         {
             List<string> result = new List<string>();
             if ((bool)cbEmcEut1|| (bool)cmEnvorimentalEut1 || (bool)cmGrnCompEut1 || (bool)cmProdSafetyEut1|| (bool)cmGrnCompEut1)
             {
                 if (DatePickerEUT1.Date == null)
                 {
                     result.Add("please provide a date for EUT 1");
                 }
             }
             if ((bool)cbEmcEut2 || (bool)cmEnvorimentalEut2 || (bool)cmGrnCompEut2 || (bool)cmProdSafetyEut2 || (bool)cmGrnCompEut2)
             {
                 if (DatePickerEUT2.Date == null)
                 {
                     result.Add("please provide a date for EUT 2");
                 }
             }
             if ((bool)cbEmcEut3 || (bool)cmEnvorimentalEut3 || (bool)cmGrnCompEut3 || (bool)cmProdSafetyEut3 || (bool)cmGrnCompEut3)
             {
                 if (DatePickerEUT3.Date == null)
                 {
                     result.Add("please provide a date for EUT 3");
                 }
             }
             if ((bool)cbEmcEut4 || (bool)cmEnvorimentalEut4 || (bool)cmGrnCompEut4 || (bool)cmProdSafetyEut4 || (bool)cmGrnCompEut4)
             {
                 if (DatePickerEUT4.Date == null)
                 {
                     result.Add("please provide a date for EUT 4");
                 }
             }
             if ((bool)cbEmcEut5 || (bool)cmEnvorimentalEut5 || (bool)cmGrnCompEut5|| (bool)cmProdSafetyEut5 || (bool)cmGrnCompEut5)
             {
                 if (DatePickerEUT5.Date == null)
                 {
                     result.Add("please provide a date for EUT 5");
                 }
             }
             return result;
         }

        private List<Eut> getEutData()
        {
            List<Eut> result = new List<Eut>();

            //get first eut and date
            if (DatePickerEUT1.Date != null)
            {
                DateTime date = (DateTime)dateExpectedEnd.Date;
                string description = "";
                if ((bool)cbEmcEut1)
                {
                    description = "EMC - EUT 1";
                }
                if ((bool)cmEnvorimentalEut1)
                {
                    description = "Environmental - EUT 1";
                }
                if ((bool)cmGrnCompEut1)
                {
                    description = "Green Compliance - EUT 1";
                }
                if ((bool)cmRelEut1)
                {
                    description = "Reliability - EUT 1";
                }
                if ((bool)cmProdSafetyEut1)
                {
                    description = "Product Safety - EUT 1";
                }
                result.Add(new Eut()
                {
                    IdRqDetail = Detail.IdRqDetail,
                    AvailableDate = date,
                    OmschrijvingEut = description
                });
            }

            //get second eut and date
            if (DatePickerEUT2.Date != null)
            {
                DateTime date = (DateTime)dateExpectedEnd.Date;
                string description = "";
                if ((bool)cbEmcEut2)
                {
                    description = "EMC - EUT 2";
                }
                if ((bool)cmEnvorimentalEut2)
                {
                    description = "Environmental - EUT 2";
                }
                if ((bool)cmGrnCompEut2)
                {
                    description = "Green Compliance - EUT 2";
                }
                if ((bool)cmRelEut2)
                {
                    description = "Reliability - EUT 2";
                }
                if ((bool)cmProdSafetyEut2)
                {
                    description = "Product Safety - EUT 2";
                }
                result.Add(new Eut()
                {
                    IdRqDetail = Detail.IdRqDetail,
                    AvailableDate = date,
                    OmschrijvingEut = description
                });
            }

            //get third eut and date
            if (DatePickerEUT3.Date != null)
            {
                DateTime date = (DateTime)dateExpectedEnd.Date;
                string description = "";
                if ((bool)cbEmcEut3)
                {
                    description = "EMC - EUT 3";
                }
                if ((bool)cmEnvorimentalEut3)
                {
                    description = "Environmental - EUT 3";
                }
                if ((bool)cmGrnCompEut3)
                {
                    description = "Green Compliance - EUT 3";
                }
                if ((bool)cmRelEut3)
                {
                    description = "Reliability - EUT 3";
                }
                if ((bool)cmProdSafetyEut3)
                {
                    description = "Product Safety - EUT 3";
                }
                result.Add(new Eut()
                {
                    IdRqDetail = Detail.IdRqDetail,
                    AvailableDate = date,
                    OmschrijvingEut = description
                });
            }
            //get fourth eut and date
            if (DatePickerEUT4.Date != null)
            {
                DateTime date = (DateTime)dateExpectedEnd.Date;
                string description = "";
                if ((bool)cbEmcEut4)
                {
                    description = "EMC - EUT 4";
                }
                if ((bool)cmEnvorimentalEut4)
                {
                    description = "Environmental - EUT 4";
                }
                if ((bool)cmGrnCompEut4)
                {
                    description = "Green Compliance - EUT 4";
                }
                if ((bool)cmRelEut4)
                {
                    description = "Reliability - EUT 4";
                }
                if ((bool)cmProdSafetyEut4)
                {
                    description = "Product Safety - EUT 4";
                }
                result.Add(new Eut()
                {
                    IdRqDetail = Detail.IdRqDetail,
                    AvailableDate = date,
                    OmschrijvingEut = description
                });
            }

            //get fifth eut and date
            if (DatePickerEUT5.Date!= null)
            {
                DateTime date = (DateTime)dateExpectedEnd.Date;
                string description = "";
                if ((bool)cbEmcEut5)
                {
                    description = "EMC - EUT 5";
                }
                if ((bool)cmEnvorimentalEut5)
                {
                    description = "Environmental - EUT 5";
                }
                if ((bool)cmGrnCompEut5)
                {
                    description = "Green Compliance - EUT 5";
                }
                if ((bool)cmRelEut5)
                {
                    description = "Reliability - EUT 5";
                }
                if ((bool)cmProdSafetyEut5)
                {
                    description = "Product Safety - EUT 5";
                }
                result.Add(new Eut()
                {
                    IdRqDetail = Detail.IdRqDetail,
                    AvailableDate = date,
                    OmschrijvingEut = description
                });
            }
            return result;
        }
        
    }
}

