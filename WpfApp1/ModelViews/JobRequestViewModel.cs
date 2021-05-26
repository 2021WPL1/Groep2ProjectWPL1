using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Microsoft.Win32;

/// <summary>
/// 
///     (╭ ゜-゜)╮ ┬─┬
///     
///     (╯°□°)╯︵ ┻━┻
///     
/// </summary>

namespace Barco
{ //bianca
    public class JobRequestViewModel : ViewModelBase
    {

        public JobRequest screen;


        public RqRequest request = new RqRequest();

        //binding value
        public Part selectedPart { get; set; }

        // buttons : Add/Remove, Send/Cancel
        public ICommand CancelCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public string txtPartNr { get; set; } // EUT Partnumber
        public string txtNetWeight { get; set; } //net weight
        public string txtGrossWeight { get; set; } //gross weight
        public string txtLinkTestplan { get; set; } // link to testplan
        public string txtReqInitials { get; set; } // requester initials 
        public string txtEutProjectname { get; set; } //EUT Project name
        public string txtRemark { get; set; } // special remarks
        public string txtFunction { get; set; } //function
        public DateTime dateExpectedEnd { get; set; }
        public string SelectedDivision { get; set; }
        public string SelectedJobNature { get; set; }


        // EUT foreseen availability date
        public DateTime DatePickerEUT1 { get; set; }
        public DateTime DatePickerEUT2 { get; set; }
        public DateTime DatePickerEUT3 { get; set; }
        public DateTime DatePickerEUT4 { get; set; }
        public DateTime DatePickerEUT5 { get; set; }

        private ObservableCollection<Part>
            lstParts = new ObservableCollection<Part>(); // for partnumber+ net/gross weight

        public ObservableCollection<Part> listParts
        {
            get { return lstParts; }
        }

        private ObservableCollection<string> _err_output { get; set; } // listview for errors

        //remove this line if working with DAO static class
        //private static Barco2021Context DAO = new Barco2021Context();

        private DAO dao;
        public JobRequestViewModel jobRequestViewModel;
        public RqOptionel optional = new RqOptionel();
        public List<Eut> eutList = new List<Eut>();
        public RqRequestDetail Detail = new RqRequestDetail();
        public List<Part> parts = new List<Part>();

        List<bool> emcBoxes = new List<bool>();
        List<bool> envBoxes = new List<bool>();
        List<bool> relBoxes = new List<bool>();
        List<bool> prodBoxes = new List<bool>();
        List<bool> greenBoxes = new List<bool>();
        List<bool> selectionBoxes = new List<bool>();
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

        public ComboBox cmbDivision { get; set; }
        public ComboBox cmbJobNature { get; set; }






        //radio button
        public bool rbtnBatNo { get; set; }
        public bool rbtnBatYes { get; set; }

        public JobRequestViewModel(JobRequest screen)
        {
            CancelCommand = new DelegateCommand(CancelButton);
            SendCommand = new DelegateCommand(SendButton);
            AddCommand = new DelegateCommand(AddButton);
            RemoveCommand = new DelegateCommand(RemoveButton);

            dao = DAO.Instance();
            this.screen = screen;
            dateExpectedEnd = DateTime.Now;
            DatePickerEUT1 = DateTime.Now;
            DatePickerEUT2 = DateTime.Now;
            DatePickerEUT3 = DateTime.Now;
            DatePickerEUT4 = DateTime.Now;
            DatePickerEUT5 = DateTime.Now;

            _err_output = new ObservableCollection<string>();


            txtFunction = GetValues("FUNCTION");


            txtReqInitials = GetInitialsFromReg();


        }
        public string GetInitialsFromReg()
        {
            string fullName = GetValues("NAME");
            string FirstName = fullName.Split(" ")[0];
            string LastName = fullName.Split(" ")[1];

            return (FirstName.Substring(0, 2) + LastName.Substring(0, 1)).ToUpper();

        }



        static string GetValues(string Name)
        {
            string userRoot = "HKEY_CURRENT_USER";
            string subkey = "Barco2021";
            string keyName = userRoot + "\\" + subkey;


            return Microsoft.Win32.Registry.GetValue(keyName, Name, "default").ToString();
        }

        //working internally with the binding 
        public Part SelectedPart
        {
            get { return selectedPart; }
            set
            {
                selectedPart = value;
                OnPropertyChanged();
            }
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
                if (txtPartNr.Length == 0 || txtNetWeight.Length == 0 || txtGrossWeight.Length == 0)
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

                   
                    RefreshGUI();
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("please fill in all fields");
            }
        }
        private void RefreshGUI()
        {
            lstParts.Clear();
            foreach (Part part in parts)
            {
                lstParts.Add(part);
            }
        }

        public void RemoveButton()
        {
            if (parts.Contains(selectedPart))
            {
                parts.Remove(selectedPart);
                lstParts.Remove(selectedPart);
                RefreshGUI();
                OnPropertyChanged();
            }
        }

        public void SendButton()
        {
            try
            {
                
                //create error sequence
                CreateBoxLists();
                List<string> errors = new List<string>();
                _err_output.Clear();
                //declare var for object
                string input_Abbreviation = txtReqInitials;
                string input_ProjectName = txtEutProjectname;

                bool input_Battery = false;

                DateTime input_EndDate = DateTime.Now;

                if (dateExpectedEnd.Date != null)
                {
                    input_EndDate = dateExpectedEnd.Date;
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

                if ((bool)rbtnBatNo==true)
                {
                    input_Battery = false;
                }
                else if ((bool)rbtnBatNo==false && (bool)rbtnBatYes==false)
                {
                    errors.Add("please check if batteries are needed");
                }
                else
                {
                    input_Battery = true;
                }

                //check if requester exists

                if (dao.IfPersonExists(input_Abbreviation))
                {
                    errors.Add("the requester initials do not match any employee");
                }

                //check if the job nature is selected
                if (SelectedJobNature == null)
                {
                    errors.Add("select a jobnature");
                }

                if (SelectedDivision == null)
                {
                    errors.Add("select a division");
                }

                //checkbox area
                if (!(bool)cbEmc && !(bool)cmEnvironmental && !(bool)cmRel &&
                    !(bool)cmProdSafety && !(bool)cmGrnComp)
                {
                    errors.Add("Please select a test nature");
                }
                else
                {
                    List<string> validation = ValidateCheckboxes();
                    errors.AddRange(validation);
                }

                //check if the dates are set
                List<string> valiDate = CheckDates();
                errors.AddRange(valiDate);

                //check if other fields are empty

                if (txtEutProjectname==null)
                {
                    errors.Add("please fill in a project name");
                }

                //error handling
                if (errors.Count > 0)
                {
                    
                    foreach (string s in errors)
                    {
                        _err_output.Add(s);
                    }

                }
                else
                {
                    //request object 
                    request.Requester = input_Abbreviation;
                    request.BarcoDivision = SelectedDivision;
                    request.JobNature = SelectedJobNature;
                    request.RequestDate = DateTime.Now;
                    request.EutProjectname = txtEutProjectname;
                    request.Battery = input_Battery;
                    request.ExpectedEnddate = dateExpectedEnd.Date;
                    request.NetWeight = netWeights;
                    request.GrossWeight = grossWeights;
                    request.EutPartnumbers = partNums;
                    request.HydraProjectNr = "0";

                    //optional object
                    optional.Link = txtLinkTestplan;
                    optional.IdRequest = request.IdRequest;
                    optional.Remarks = specialRemarks;

                    //eut objects
                    eutList = getEutData();

                    //detail object
                    Detail.Testdivisie = "ECO";



                    dao.AddRequest(request, Detail, optional, eutList);
                    MessageBox.Show("Data has been inserted");
                    
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

        public void CreateBoxLists()
        {

            emcBoxes.Add(cbEmcEut1);
            emcBoxes.Add(cbEmcEut2);
            emcBoxes.Add(cbEmcEut3);
            emcBoxes.Add(cbEmcEut4);
            emcBoxes.Add(cbEmcEut5);

            envBoxes.Add(cmEnvironmentalEut1);
            envBoxes.Add(cmEnvironmentalEut2);
            envBoxes.Add(cmEnvironmentalEut3);
            envBoxes.Add(cmEnvironmentalEut4);
            envBoxes.Add(cmEnvironmentalEut5);

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

            selectionBoxes.Add(cmEnvironmental);
            selectionBoxes.Add(cbEmc);
            selectionBoxes.Add(cmRel);
            selectionBoxes.Add(cmProdSafety);
            selectionBoxes.Add(cmGrnComp);
        }

        private void EnableBoxes(bool selected)
        {
            List<bool> targets = new List<bool>();
            if (cbEmc)
            {
                targets = emcBoxes;
            }
            else if (cmEnvironmental)
            {
                targets = envBoxes;
            }
            else if (cmRel)
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

            //foreach (bool b in targets)
            //{
            //    b = true;
            //}
        }
        //laurent - edit thibaut
        private List<string> ValidateCheckboxes()
        {
            List<string> outcome = new List<string>();
            if ((bool)cbEmc)
            {
                int counter = 0;
                foreach (bool b in emcBoxes)
                {
                    if ((bool)b)
                    {
                        counter++;
                    }
                }

                if (counter < 1)
                {
                    outcome.Add("please check emc data");
                }
            }

            if ((bool)cmEnvironmental)
            {
                int counter = 0;
                foreach (bool b in envBoxes)
                {
                    if ((bool)b)
                    {
                        counter++;
                    }
                }

                if (counter < 1)
                {
                    outcome.Add("please check environmental data");
                }
            }

            if ((bool)cmRel)
            {
                int counter = 0;
                foreach (bool b in relBoxes)
                {
                    if ((bool)b)
                    {
                        counter++;
                    }
                }

                if (counter < 1)
                {
                    outcome.Add("please check reliability data");
                }
            }

            if ((bool)cmProdSafety)
            {
                int counter = 0;
                foreach (bool b in prodBoxes)
                {
                    if ((bool)b)
                    {
                        counter++;
                    }
                }

                if (counter < 1)
                {
                    outcome.Add("please check product safety data");
                }
            }

            if ((bool)cmGrnComp)
            {
                int counter = 0;
                foreach (bool b in greenBoxes)
                {
                    if ((bool)b)
                    {
                        counter++;
                    }
                }

                if (counter < 1)
                {
                    outcome.Add("please check green compliance data");
                }
            }

            return outcome;
        }

        private List<string> CheckDates()
        {
            List<string> result = new List<string>();
            if ((bool)cbEmcEut1 || (bool)cmEnvironmentalEut1 || (bool)cmGrnCompEut1 || (bool)cmProdSafetyEut1 ||
                (bool)cmGrnCompEut1)
            {
                if (DatePickerEUT1.Date == null)
                {
                    result.Add("please provide a date for EUT 1");
                }
            }

            if ((bool)cbEmcEut2 || (bool)cmEnvironmentalEut2 || (bool)cmGrnCompEut2 || (bool)cmProdSafetyEut2 ||
                (bool)cmGrnCompEut2)
            {
                if (DatePickerEUT2.Date == null)
                {
                    result.Add("please provide a date for EUT 2");
                }
            }

            if ((bool)cbEmcEut3 || (bool)cmEnvironmentalEut3 || (bool)cmGrnCompEut3 || (bool)cmProdSafetyEut3 ||
                (bool)cmGrnCompEut3)
            {
                if (DatePickerEUT3.Date == null)
                {
                    result.Add("please provide a date for EUT 3");
                }
            }

            if ((bool)cbEmcEut4 || (bool)cmEnvironmentalEut4 || (bool)cmGrnCompEut4 || (bool)cmProdSafetyEut4 ||
                (bool)cmGrnCompEut4)
            {
                if (DatePickerEUT4.Date == null)
                {
                    result.Add("please provide a date for EUT 4");
                }
            }

            if ((bool)cbEmcEut5 || (bool)cmEnvironmentalEut5 || (bool)cmGrnCompEut5 || (bool)cmProdSafetyEut5 ||
                (bool)cmGrnCompEut5)
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
            if (DatePickerEUT1.Date != DateTime.Now)
            {
                DateTime date = (DateTime) DatePickerEUT1.Date;
                string description = "";
                if ((bool)cbEmcEut1)
                {
                    description = "EMC - EUT 1";
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
                DateTime date = (DateTime) DatePickerEUT2.Date;
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
                DateTime date = (DateTime) DatePickerEUT3.Date;
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
                DateTime date = (DateTime) DatePickerEUT4.Date;
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
                DateTime date = (DateTime) DatePickerEUT5.Date;
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

                if ((bool) cmGrnCompEut5)
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

            return result;
        }
        //thibaut
        private Eut createEut(string description, DateTime date)
        {
            return new Eut()
            {
                IdRqDetail = Detail.IdRqDetail,
                AvailableDate = date,
                OmschrijvingEut = description
            };
        }

        public ObservableCollection<string> err_output
        {
            get { return _err_output; }
            set { _err_output = value; }
        }

        //Bianca
        //check if the person is an internal of external based on the initials
        //later find a solution for the hard-code
        private bool checkInternal(string Name)
        {
            if (Name == "BIC")
            {
                return true;
            }

            else
            {
                return false;

            }
        }

    }
}
