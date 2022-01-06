using MessagingWebApplication.Models;
using BCryptTool = BCrypt.Net.BCrypt;
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
                string salt = BCryptTool.GenerateSalt();
                person.Password=BCryptTool.HashPassword(person.Password, salt);
                context.People.Add(person);
                context.SaveChanges();
            }
        }

        public List<PersonDto> GetAllPersons()
        {
            List<PersonDto> personDtoList = new List<PersonDto>();
            using (var context=new SanayChatDBEntities())
            {
                context.People.ToList().ForEach(p =>
                {    
                    personDtoList.Add(PersonDto.Mapper(p));
                });
            }
            return personDtoList;
        }

        public List<PersonDto> GetAllPersonsExceptId(int personId)
        {
            List<PersonDto> personDtoList = new List<PersonDto>();
            using (var context = new SanayChatDBEntities())
            {
                context.People.Where(p=>p.PersonId!=personId).ToList().ForEach(p =>
                {
                    personDtoList.Add(PersonDto.Mapper(p));
                });
            }
            return personDtoList;
        }

        public Boolean SignIn(String userName, String password)
        {
            using (var context=new SanayChatDBEntities())
            {
               var user = context.People.SingleOrDefault(p => p.UserName == userName);            
                return user != null && BCryptTool.Verify(password, user.Password);
            }                          
        }

        public Person GetPersonById(int id)
        {
            using (var context = new SanayChatDBEntities())
            {
                return context.People.FirstOrDefault(p => p.PersonId == id);
            }
        }

        public Person GetPersonByUserName(String userName)
        {
            using (var context = new SanayChatDBEntities())
            {
                return context.People.FirstOrDefault(p => p.UserName == userName);
            }
        }
    }
}