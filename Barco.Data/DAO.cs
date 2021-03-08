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

      // add a new person in the database
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
 
        // get a person with the abbreviation
        public Person getPersonWithAbb(string abb)
        {
            return context.Person.FirstOrDefault(a => a.Afkorting == abb);

        }

        // delete a person from the database
      public void removePerson(string abb)
            {
                context.Person.Remove(getPersonWithAbb(abb));
                saveChanges();
            }

        //  get the request's date
        public RqRequest getRequestDate()
        {
            return context.RqRequest.FirstOrDefault(a => a.RequestDate == DateTime.Now);

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


        
        //job nature
        public RqRequest getJobNature()
        {
            return context.RqRequest.FirstOrDefault(a => a.JobNature == "NULL");
        }

    }
}
