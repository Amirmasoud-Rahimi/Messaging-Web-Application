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
        public String FullName { get; set; }
        public String DateOfJoining { get; set; }
        public String PhotoFileName { get; set; }
        public String token { get; set; }

        public static PersonDto Mapper(Person person)
        {
            PersonDto personDto = new PersonDto();
            personDto.Id = person.PersonId;
            personDto.UserName = person.UserName;
            personDto.FullName = person.UserName;
            personDto.DateOfJoining = person.DateOfJoining.ToString();
            personDto.PhotoFileName = person.PhtotFileName;
            return personDto;
        }

        public static PersonDto SignInMapper(Person person,String token)
        {
            PersonDto personDto = new PersonDto();
            personDto.Id = person.PersonId;
            personDto.UserName = person.UserName;
            personDto.FullName = person.UserName;
            personDto.DateOfJoining = person.DateOfJoining.ToString();
            personDto.PhotoFileName = person.PhtotFileName;
            personDto.token = token;
            return personDto;
        }
    }
}