using MessagingWebApplication.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessagingWebApplication.Services
{
    public class CrudService : ICrudService
    {
        public void AddNewPerson(Person person)
        {
            using (var context = new SanayChatDBEntities())
            {
                context.People.Add(person);
                context.SaveChanges();
            }
        }

        public List<string> GetAllPersonsFullName()
        {
            List<string> personsFullName = new List<String>();
            using (var context=new SanayChatDBEntities())
            {
                context.People.ToList().ForEach(p =>
                {
                    personsFullName.Add(p.FullName);
                });
            }
            return personsFullName;
        }

        public Boolean Login(Person person)
        {
            using (var context=new SanayChatDBEntities())
            {
               var user = context.People.FirstOrDefault(p => p.UserName == person.UserName &&
                      p.Password == person.Password);
                return user != null;
            }                          
        }

        public Person GetPersonById(int id)
        {
            using (var context = new SanayChatDBEntities())
            {
                Person person = context.People.FirstOrDefault(p => p.PersonId == id);
                return person;
            }
        }
    }
}