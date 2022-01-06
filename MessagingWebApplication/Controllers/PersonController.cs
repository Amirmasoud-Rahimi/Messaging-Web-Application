using MessagingWebApplication.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessagingWebApplication.Models;
using MessagingWebApplication.Filter;

namespace MessagingWebApplication.Controllers
{
    [RoutePrefix("api/Person")]
    public class PersonController : ApiController
    {

       private ICrudService crudService;

        public PersonController()
        {
            crudService = new CrudService();
        }

        [AllowAnonymous]
        [Route("AddPerson")]
        [HttpPost]
       public HttpResponseMessage AddPerson(Person person)
        {
            try
            {
               crudService.AddNewPerson(person);
               return new HttpResponseMessage(HttpStatusCode.OK);
            }catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [JwtAuthentication]
        [Route("GetAllPersons")]
        [HttpGet]
        public HttpResponseMessage GetAllPersons()
        {
            try
            {
                var data=crudService.GetAllPersons();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [JwtAuthentication]
        [Route("GetAllPersonsExceptId/{personId}")]
        [HttpGet]
        public HttpResponseMessage GetAllPersonsExceptId(int personId)
        {
            try
            {
                var data = crudService.GetAllPersonsExceptId(personId);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("SignIn")]
        [HttpPost]
        public HttpResponseMessage SignIn(Person person)
        {
            try
            {
                var userValidation = crudService.SignIn(person.UserName,person.Password);
                if( userValidation )
                {
                    String token = JwtManager.GenerateToken(person.UserName);
                    Person p = crudService.GetPersonByUserName(person.UserName);
                    PersonDto dto = PersonDto.SignInMapper(p, token);
                    return Request.CreateResponse(HttpStatusCode.OK,dto);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ex.Message);
            }
        }

        [JwtAuthentication]
        [Route("GetPerson/{personId}")]
        [HttpGet]
        public HttpResponseMessage Get(int personId)
        {
            try
            {
                Person person = crudService.GetPersonById(personId);
                if (person != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, person);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }              
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}