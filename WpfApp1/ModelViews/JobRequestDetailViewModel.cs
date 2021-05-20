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


    }
}
