using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessagingWebApplication.Models
{
    public class PersonDto
    {
        public int Id { get; set; } 
        public String UserName { get; set; }
        public String DateOfJoining { get; set; }
        public String PhotoFileName { get; set; }
    
        public PersonDto Mapper(Person person)
        {
            PersonDto personDto = new PersonDto();
            personDto.Id = person.PersonId;
            personDto.UserName = person.UserName;
            personDto.DateOfJoining = person.DateOfJoining.ToString();
            personDto.PhotoFileName = person.PhtotFileName;
            return personDto;
        }
    }
}