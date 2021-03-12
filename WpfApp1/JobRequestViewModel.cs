using Barco.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Barco.JobRequest;

namespace Barco
{ //bianca
  public class JobRequestViewModel: ViewModelBase
    {
        private JobRequest screen;

        public ICommand CancelCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        private string textPartNr { get; set; }
        private string textNetWeight { get; set; }
        private string textGrossWeight { get; set; }

        private List<Part> parts = new List<Part>();
        private RqRequest request = new RqRequest();
        public ObservableCollection<Part> ListParts { get; set; }




        public JobRequestViewModel(JobRequest screen)
        {
            CancelCommand = new DelegateCommand(CancelButton);
            SendCommand = new DelegateCommand(SendButton);
            AddCommand = new DelegateCommand(AddButton);
            RemoveCommand = new DelegateCommand(RemoveButton);


            this.screen = screen;

        }

        public void CancelButton()
        {           
            HomeScreen home= new HomeScreen();
            screen.Close();
            home.ShowDialog();
        }
   
        public void SendButton()
        {
           
        }

        public void AddButton()
        {
            try { 

                if (textPartNr == "" || textNetWeight == "" || textGrossWeight == "")
                {
                    MessageBox.Show("please fill in all values");
                }
                else
                {
                    parts.Add(new Part()
                    {
                        NetWeight = textNetWeight,
                        GrossWeight = textGrossWeight,
                        partNo = textPartNr
                    });
                   refreshGUI();

                    

                    request.EutPartnumbers += textPartNr + " ; ";
                    request.GrossWeight += textGrossWeight + " ; ";
                    request.NetWeight += textNetWeight + " ; ";
                }

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("please fill in all fields");
            }


        }

        public void RemoveButton()
        {
            //if (ListParts.SelectedValue.ToString() != null)
            //{
            //    ListParts.Items.Remove(ListParts.SelectedValue);
            //    parts.Remove((Part)ListParts.SelectedValue);
            //}
        }

        public class Part
        {
            public string partNo { get; set; }
            public string NetWeight { get; set; }
            public string GrossWeight { get; set; }
        }

        private void refreshGUI()
        {
            ListParts.Clear();    
            foreach (Part part in parts)
            {
                ListParts.Add(part);
            }
        }
    }
}
