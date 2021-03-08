using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barco.Data
{
     public class DAO
     {
        private static readonly DAO instance = new DAO();

        public static DAO Instance()
        {
            return instance;
        }

        // Private constructor!
        private DAO()

        {
            this.context= new Barco2021Context();
        }

       
        // DBContext
        private Barco2021Context context;


        public void saveChanges()
        {
            context.SaveChanges();
        }

      // bianca- add a new person in the database
       public Person AddPerson(string abb, string firstname, string lastname)
        {
            Person person = new Person
            {
                Afkorting = abb,
                Voornaam = firstname,
                Familienaam = lastname
            };
            context.Person.Add(person);
            saveChanges();
            return person;
        }
       
        // bianca- get a person with the abbreviation
        public Person getPersonWithAbb(string abb)
        {
            return context.Person.FirstOrDefault(a => a.Afkorting == abb);
        }

        public bool IfPersonExists(string abb)
        {
            bool result = false;
            if (context.Person.Any(a => a.Afkorting == abb))
            {
                result = true;
            }
            return result;
        }
        //bianca
        // delete a person from the database
        public void removePerson(string abb)
        {
            context.Person.Remove(getPersonWithAbb(abb));
            saveChanges();
        }


        
        //  bianca- get the request's date
        public RqRequest getRequestDate()
        {
            return context.RqRequest.FirstOrDefault(a => a.RequestDate == DateTime.Now);

        }
        //Jimmy
        // get all the RqRequests 
        public ICollection<RqRequest> getAllRqRequests()
        {
            return context.RqRequest.ToList();
        }
        //Jimmy
        // Get RqRequest by Id
        public RqRequest getRqRequestById(int id)
        {
            return context.RqRequest.FirstOrDefault(r => r.IdRequest == id);
        }

        //jimmy
        // Get RqRequestDetails by Id
        public RqRequestDetail getRqRequestDetailById(int id)
        {
            return context.RqRequestDetail.FirstOrDefault(r => r.IdRqDetail == id);
        }

       

        //Jimmy
        // Delete Selected JobRequest
        public void deleteJobRequest(int id)
        {
            context.RqRequest.Remove(getRqRequestById(id));
            saveChanges();
        }

        // add current date in the database
        public RqRequest addRequestDate()
        { RqRequest rqRequest = new RqRequest
           { 
            RequestDate = DateTime.Now

              };
            context.RqRequest.Add(rqRequest);

            saveChanges();

            return rqRequest;
        }


     

        //bianca  - getDepartment
        public List<RqBarcoDivision> getDepartment()
        { 
            return context.RqBarcoDivision.ToList();
        }

        //bianca - getNature

        public List<RqBarcoDivision> getDivisions()
        {
            return context.RqBarcoDivision.ToList();
        }

        public List<RqJobNature> getJobNatures()
        {
            return context.RqJobNature.ToList();
        }

        //request detail opvragen op basis van selected index
        public RqRequest getRequest(int requestId)
        {
            return context.RqRequest.Where(rq => rq.IdRequest == requestId).FirstOrDefault() ;
        }
        //geeft een requestDetail object op basis van het juiste requestID veld
        public RqRequestDetail getRequestDetail(int requestId)
        {
            return context.RqRequestDetail.Where(det => det.IdRequest == requestId).FirstOrDefault();
        }
        //geeft een eut object op basis van het id van RequestDetail tabel
        public Eut getEut(int idReqDet)
        {
            return context.Eut.Where(eut => eut.IdRqDetail == idReqDet).FirstOrDefault() ;
        }
        public RqOptionel getOptionel(int idReq)
        {
            return context.RqOptionel.Where(opt => opt.IdRequest == idReq).FirstOrDefault();
        }
     }
}

