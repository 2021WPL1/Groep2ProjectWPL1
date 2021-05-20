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
        public Person GetPersonWithAbb(string abb)
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
        public void RemovePerson(string abb)
        {
            context.Person.Remove(GetPersonWithAbb(abb));
            saveChanges();
        }


        
        //  bianca
        public RqRequest GetRequestDate()
        {
            return context.RqRequest.FirstOrDefault(a => a.RequestDate == DateTime.Now);
            

        }
        //Jimmy
        // get all the RqRequests 
        public ICollection<RqRequest> GetAllRqRequests()
        {
            return context.RqRequest.ToList();
        }
        //Jimmy
        // Get RqRequest by Id
        public RqRequest GetRqRequestById(int id)
        {
            return context.RqRequest.FirstOrDefault(r => r.IdRequest == id);
        }

        //jimmy
        // Get RqRequestDetails by Id
        public RqRequestDetail GetRqRequestDetailById(int id)
        {
            return context.RqRequestDetail.FirstOrDefault(r => r.IdRqDetail == id);
        }

       

        //Jimmy
        // Delete Selected JobRequest
        public void DeleteJobRequest(int id)
        {
            context.RqRequest.Remove(GetRqRequestById(id));
            saveChanges();
        }
        // jimmy
        public void ApproveRqRequest(RqRequest rqRequest)
        {
            rqRequest.JrStatus = "Approved";
            saveChanges();
        }

       

     

        //bianca  
        public List<RqBarcoDivision> GetDepartment()
        { 
            return context.RqBarcoDivision.ToList();
        }

        //bianca 

        public List<RqBarcoDivision> GetDivisions()
        {
            return context.RqBarcoDivision.ToList();
        }
        
        public List<RqJobNature> GetJobNatures()
        {
            return context.RqJobNature.ToList();
        }

        //request detail opvragen op basis van selected index
        public RqRequest GetRequest(int requestId)
        {
            return context.RqRequest.Where(rq => rq.IdRequest == requestId).FirstOrDefault() ;
        }
        //geeft een requestDetail object op basis van het juiste requestID veld
        public RqRequestDetail GetRequestDetail(int requestId)
        {
            return context.RqRequestDetail.Where(det => det.IdRequest == requestId).FirstOrDefault();
        }
        //geeft een eut object op basis van het id van RequestDetail tabel
        public Eut GetEut(int idReqDet)
        {
            return context.Eut.Where(eut => eut.IdRqDetail == idReqDet).FirstOrDefault() ;
        }
        public RqOptionel GetOptionel(int idReq)
        {
            return context.RqOptionel.Where(opt => opt.IdRequest == idReq).FirstOrDefault();
        }

        //Stach - geeft division op basis van de afkotring
        public RqBarcoDivision GetDivisionByAbb(string abb)
        {
            return context.RqBarcoDivision.FirstOrDefault(a => a.Afkorting == abb);
        }

        public bool IfDivisionExists(string abb)
        {
            bool result = false;
            if (context.RqBarcoDivision.Any(a => a.Afkorting == abb))
            {
                result = true;
            }
            return result;
        }
        // remove division
        public void RemoveDivisionByAbb(string abb)
        {
            context.RqBarcoDivision.Remove(GetDivisionByAbb(abb));
            saveChanges();
        }

        // Geeft devision
        public List<RqTestDevision> GetTestDevisions()
        {
            return context.RqTestDevision.ToList();
        }
        //Geeft devision op basis van de afkorting
        public RqTestDevision GetRqTestDevByAbb(string abb)
        {
            return context.RqTestDevision.FirstOrDefault(a => a.Afkorting == abb);
        }

        //voegt een division toe aan de database(?)
        public RqBarcoDivision AddDivision(string abb, string alias, bool active)
        {
            RqBarcoDivision rqBarcoDivision = new RqBarcoDivision
            {
                Afkorting = abb,
                Alias = alias,
                Actief = active
            };
            context.RqBarcoDivision.Add(rqBarcoDivision);
            saveChanges();
            return rqBarcoDivision;
        }

        //voegt een persoon toe aan een division(?)
        public RqBarcoDivisionPerson AddDivPer(string AbbDevision, string AbbPerson, string PvgGroup)
        {
            RqBarcoDivisionPerson rqBarcoDivisionPerson = new RqBarcoDivisionPerson
            {
                AfkDevision = AbbDevision,
                AfkPerson = AbbPerson,
                Pvggroup = PvgGroup
            };
            context.RqBarcoDivisionPerson.Add(rqBarcoDivisionPerson);
            saveChanges();
            return rqBarcoDivisionPerson;
        }


        //Bianca
        // Add request/ detail / optional

        public RqRequest AddRequest(RqRequest request, RqRequestDetail detail, RqOptionel optional)
        { 
            try
            {
                context.RqRequest.Add(request);
                context.SaveChanges();
                AddOptional(optional);
                AddDetail(detail);

            }
            
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return request;
          

        }


        public RqRequestDetail AddDetail(RqRequestDetail detail)
        {
            detail.IdRequest = int.Parse(context.RqRequest.OrderByDescending(p => p.IdRequest).Select(p => p.IdRequest).First().ToString());
            context.RqRequestDetail.Add(detail);
            context.SaveChanges();
            return detail;
        }

        public RqOptionel AddOptional(RqOptionel optional)
        {
            optional.IdRequest =
              int.Parse(context.RqRequest.OrderByDescending(p => p.IdRequest).Select(p => p.IdRequest).First().ToString() );
            context.RqOptionel.Add(optional);
            context.SaveChanges();
            return optional;
        }

    }
}

