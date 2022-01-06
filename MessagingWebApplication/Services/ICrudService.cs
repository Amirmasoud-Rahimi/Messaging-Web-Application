using MessagingWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingWebApplication.Services
{
    public interface ICrudService
    {
        void AddNewPerson(Person person);

        List<PersonDto> GetAllPersons();

        Boolean SignIn(String userName,String password);

        Person GetPersonById(int id);

        Person GetPersonByUserName(String userName);

        List<PersonDto> GetAllPersonsExceptId(int personId);


    }
}
