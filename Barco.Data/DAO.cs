using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
            this.context = new Barco2021Context();
        }
        // DBContext
        private Barco2021Context context;
        //bianca - save and send changes to the database
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
        // bianca & jimmy- get a person with the abbreviation
        public Person GetPersonWithAbb(string abb)
        {
            return context.Person.FirstOrDefault(a => a.Afkorting == abb);
        }
        //Laurent
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
        // get the date when the request is made
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
        //Jimmy- delete job request
        //Thibaut,Bianca - delete all the connections with the job request 
        // Delete Selected JobRequest
        public void DeleteJobRequest(int id)
        {
            DeleteEuts(context.RqRequestDetail.Where(a => a.IdRequest == id).ToList()) ;
            DeleteDetails(id);
            context.RqOptionel.Remove(GetOptionel(GetOptionalByRequestId(id).IdRequest));
            //context.RqRequestDetail.Remove(GetRqRequestDetailByRequestId(id));
            context.RqRequest.Remove(GetRqRequestById(id));
            saveChanges();
        }
        //thibaut & stach
        //delete al euts linked to a given request detail
        private void DeleteEuts(List<RqRequestDetail> details)
        {
            foreach (RqRequestDetail d in details)
            {
                context.Eut.RemoveRange(context.Eut.Where(e => e.IdRqDetail == d.IdRqDetail));
            }
            saveChanges();
        }
        //thibaut & stach
        //delete al details linked to given request
        private void DeleteDetails(int requestId)
        {
            context.RqRequestDetail.RemoveRange(context.RqRequestDetail.Where(e => e.IdRequest == requestId).ToList());
            saveChanges();
        }
        //thibaut, bianca
        //get an optional id from requestId
        public RqOptionel GetOptionalByRequestId(int id)
        {
            return context.RqOptionel.FirstOrDefault(r => r.IdRequest == id);
        }
        //thibaut, bianca
        //get a detail id from requestId
        public RqRequestDetail GetRqRequestDetailByRequestId(int id)
        {
            return context.RqRequestDetail.FirstOrDefault(r => r.IdRequest == id);
        }
        // jimmy, laurent
        //when a request is approved, the status will change and a planning object will be made for each request detail 
        public void ApproveRqRequest(RqRequest rqRequest, string jrNumber)
        {
            rqRequest.JrStatus = "Approved";
            rqRequest.JrNumber = jrNumber;
            TimeSpan s = new TimeSpan(5, 0, 0, 0, 0);
            DateTime DueDate = new DateTime();
            DueDate= (DateTime)rqRequest.RequestDate + s;

            List<RqRequestDetail> DetailList = this.GetRqDetailsWithRequestId(rqRequest.IdRequest);
            foreach (RqRequestDetail detail in DetailList)
            {
                PlPlanning planning = new PlPlanning()
                {
                    IdRequest = rqRequest.IdRequest,
                    JrNr = rqRequest.JrNumber,
                    Requestdate = rqRequest.RequestDate,
                    DueDate = DueDate,
                    TestDiv = detail.Testdivisie
                };
                context.PlPlanning.Add(planning);
                saveChanges();
            }
            
        }
        //bianca & jimmy
        //get all the barcoDivisions
        public List<RqBarcoDivision> GetDepartment()
        {
            return context.RqBarcoDivision.ToList();
        }
        //bianca
        //get all the natures of a job
        public List<RqJobNature> GetJobNatures()
        {
            return context.RqJobNature.ToList();
        }
        //request detail opvragen op basis van selected index
        //bianca & jimmy
        public RqRequest GetRequest(int requestId)
        {
            return context.RqRequest.Where(rq => rq.IdRequest == requestId).FirstOrDefault();
        }
        //geeft een requestDetail object op basis van het juiste requestID veld
        //jimmy
        public RqRequestDetail GetRequestDetail(int requestId)
        {
            return context.RqRequestDetail.Where(det => det.IdRequest == requestId).FirstOrDefault();
        }
        //geeft een eut object op basis van het id van RequestDetail tabel
        //bianca
        public Eut GetEut(int idReqDet)
        {
            return context.Eut.Where(eut => eut.IdRqDetail == idReqDet).FirstOrDefault();
        }
        //jimmy
        //get the optionals by their request id
        public RqOptionel GetOptionel(int idReq)
        {
            return context.RqOptionel.Where(opt => opt.IdRequest == idReq).FirstOrDefault();
        }

        //Stach - geeft division op basis van de afkorting
        public RqBarcoDivision GetDivisionByAbb(string abb)
        {
            return context.RqBarcoDivision.FirstOrDefault(a => a.Afkorting == abb);
        }
        //jimmy
        //returns true if the division exists
        public bool IfDivisionExists(string abb)
        {
            bool result = false;
            if (context.RqBarcoDivision.Any(a => a.Afkorting == abb))
            {
                result = true;
            }
            return result;
        }
        // bianca-remove division by abbreviation
        public void RemoveDivisionByAbb(string abb)
        {
            context.RqBarcoDivision.Remove(GetDivisionByAbb(abb));
            saveChanges();
        }
        // Geeft divisies
        //jimmy
        public List<RqTestDevision> GetTestDevisions()
        {
            return context.RqTestDevision.ToList();
        }
        //Geeft devision op basis van de afkorting
        //stach
        public RqTestDevision GetRqTestDevByAbb(string abb)
        {
            return context.RqTestDevision.FirstOrDefault(a => a.Afkorting == abb);
        }
        //voegt een division toe aan de database(?)
        //stach
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
        //stach
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
        //Add request/ detail
        public RqRequest AddRequest(RqRequest request, List<RqRequestDetail> details, RqOptionel optional, List<Eut> eut)
        {
            try
            {
                context.RqRequest.Add(request);
                context.SaveChanges();
                AddOptional(optional);
                AddDetails(details);
                //AddDetail(detail); old way single detail
                AddEut(eut, int.Parse(context.RqRequest.OrderByDescending(p => p.IdRequest).Select(p => p.IdRequest).First().ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return request;
        }

        


        //Bianca
        //add a planning object to planningskalender column
        public PlPlanningsKalender AddPlanToCalendar(PlPlanningsKalender planning)
        {
            try
            {
                context.PlPlanningsKalender.Add(planning);
                context.SaveChanges();

                
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return planning;
        }

        //thibaut - laurent
        //add the given details to the database and give them a primary key
        public void AddDetails(List<RqRequestDetail> listDetails)
        {
            foreach(RqRequestDetail d in listDetails)
            {
                d.IdRequest = int.Parse(context.RqRequest.OrderByDescending(p => p.IdRequest).Select(p => p.IdRequest).First().ToString());
                context.RqRequestDetail.Add(d);
            }
            context.SaveChanges();
        }
        //thibaut - laurent
        //add the optional details from the request to the database
        public RqOptionel AddOptional(RqOptionel optional)
        {
            optional.IdRequest =
              int.Parse(context.RqRequest.OrderByDescending(p => p.IdRequest).Select(p => p.IdRequest).First().ToString());
            context.RqOptionel.Add(optional);
            context.SaveChanges();
            return optional;
        }
        //thibaut - laurent
        //insert the given eut list into the database after some checks
        public void AddEut(List<Eut> eutlist, int requestId)
        {
            List<RqRequestDetail> list = GetRqDetailsWithRequestId(requestId);
            foreach (RqRequestDetail d in list)
            {
                foreach (Eut e in eutlist)
                {
                    if (d.Testdivisie.Equals(e.OmschrijvingEut.Substring(0, 3)))
                    {
                        e.IdRqDetail = d.IdRqDetail;
                        context.Eut.Add(e);
                        
                    }
                }
                context.SaveChanges();
            }
        }
        //retrieve a list of request details based on the request id
        public List<RqRequestDetail> GetRqDetailsWithRequestId(int requestId)
        {
            return context.RqRequestDetail.Where(rq => rq.IdRequest == requestId).ToList();
        }
        //thibaut
        //get a list with eut objects based on a detail id
        public List<Eut> GetEutWithDetailId(int id)
        {
            List<RqRequestDetail> rqRequestDetails= context.RqRequestDetail.Where(d => d.IdRequest == id).ToList();
            List<Eut> lijst = new List<Eut>();
            foreach(RqRequestDetail r  in rqRequestDetails)
            {
                lijst.AddRange(context.Eut.Where(eut => eut.IdRqDetail == r.IdRqDetail));
            }
            return lijst;
        }
        //thibaut
        //returns all persons in the database
        public List<Person> GetAllPerson()
        {
            return context.Person.ToList();
        }
        //thibaut
        //returns the job number
        public string GetJobNumber(bool internRq)
        {
            string result = "";
            foreach(RqRequest rq in context.RqRequest)
            {
                if(rq.JrNumber != null)
                {
                    if (rq.InternRequest == internRq)
                    {
                        result = rq.JrNumber;
                    }
                }
            }
            return result;
        }
        //bianca
        //gets all the requests that are approved
        public ICollection<RqRequest> GetAllApprovedRqRequests()
        {
            return context.RqRequest.Where(s=>s.JrStatus== "Approved").ToList();
        }
        //bianca- a list of the test nature to be linked in the combobox-OVerviewApprovedRequests
        public List<RqTestDevision> GetTestNature()
        {
            return context.RqTestDevision.ToList();
        }
        //Laurent
        //get request, details, the optional and eut data and combine them into
        //a combo object for easier databinding
        public List<ComboObject> combinedObjects()
        {
            List<ComboObject> returnValue = new List<ComboObject>();
            List<Eut> approvedEut = getApprovedEuts();

            foreach (Eut eut in approvedEut)
            {
                RqRequestDetail detail = GetRqRequestDetailById(eut.IdRqDetail);

        //Method used for the overviewApprovedRequests
        //Laurent,Bianca
                ComboObject o = new ComboObject()
                {  
                    EutNr = eut.OmschrijvingEut.Substring(5, 6),
                    Eut = eut,
                    RqRequestDetail = detail,
                    Request = GetRequest(detail.IdRequest),
                    RqOptionel = GetOptionel(detail.IdRequest)
                };
                returnValue.Add(o);
            }
            return returnValue;
        }
        //Bianca- method to get the resources from the database
        public List<PlResources> GetResource()
        {
            return context.PlResources.ToList();
            
        }
        //thibaut - methode om resources op te halen per test nature
        public List<PlResources> GetResourcesForTestDiv(string testDiv)
        {
            List<PlResources> list = new List<PlResources>();
            List<PlResourcesDivision> plrd = context.PlResourcesDivision.ToList();

            foreach(PlResourcesDivision pl in plrd)
            {
                if (pl.DivisionAfkorting.Equals(testDiv))
                {
                    try
                    {
                        list.Add(context.PlResources.Where(e => e.Id == pl.ResourcesId).First());
                    }
                    catch (NullReferenceException) { }
                }
            }
            return list;
        }
        //Laurent
        //get a eut list of all eut objects in approved requests
        public List<Eut> getApprovedEuts()
        {
            var listRqRequests = GetAllApprovedRqRequests();
            
            List<RqRequestDetail> approvedRequestDetails = new List<RqRequestDetail>();
            List<int> approvedRequestIds = new List<int>();
            var eutList = context.Eut.ToList();
            List<Eut> approvedEut = new List<Eut>();
            
            foreach (RqRequest r in listRqRequests)
            {
                approvedRequestIds.Add(r.IdRequest);
            }
            
            foreach (RqRequestDetail r in context.RqRequestDetail.ToList())
            {
                if (approvedRequestIds.Contains(r.IdRequest))
                {
                    approvedRequestDetails.Add(r);
                }
                
            }
            
            foreach (Eut e in eutList)
            {
                if (approvedRequestDetails.Contains(GetRqRequestDetailById(e.IdRqDetail)))
                {
                    approvedEut.Add(e);
                }
            }

            return approvedEut;
        }
        //Laurent
        //returns a request based on the detail id
        public RqRequest getRequestByDetailId(int detailId)
        {
            int requestId = context.RqRequestDetail.FirstOrDefault(a => a.IdRqDetail == detailId).IdRequest;
            RqRequest returnValue = context.RqRequest.FirstOrDefault(a => a.IdRequest == requestId);
            return returnValue;
        }

        //thibaut
        //returns a planning object based on the id of it
        public PlPlanning GetPlanning(int planningsId)
        {
            return context.PlPlanning.FirstOrDefault(p => p.IdPlanning == planningsId);
        }




        //Bianca- to get the list of the planning calendar
        public List<PlPlanningsKalender> listPlannings()
        {
            return context.PlPlanningsKalender.ToList();
        }


        //Jimmy-Bianca
        //method to get the test planning calendar by id
        public PlPlanningsKalender GetPlanningCalendarById(int id)
        {
            return context.PlPlanningsKalender.FirstOrDefault(i => i.Id == id);
        }

        //Jimmy-Bianca
        //method to change the status
        public void ChangeStatus(string status,int testId)
        {
            var value = GetPlanningCalendarById(testId);

            value.TestStatus = status;
            context.SaveChanges();
        }


        public void DeleteEut(int idRequest)
        {
            List<int> detailsId = new List<int>();
            foreach (RqRequestDetail r in GetRqDetailsWithRequestId(idRequest))
            {
                detailsId.Add(r.IdRqDetail);
            }

            List<Eut> listEuts = new List<Eut>();
            foreach (Eut eut in context.Eut.ToList())
            {
                if (detailsId.Contains(eut.IdRqDetail))
                {
                    listEuts.Add(eut);
                }
            }

            foreach (Eut e in listEuts)

            {
                context.Eut.Remove(e);
                context.SaveChanges();
            }
        }
    }
}
