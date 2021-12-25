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

        List<string> GetAllPersonsFullName();

        Boolean Login(Person person);

        Person GetPersonById(int id);
    }
}
