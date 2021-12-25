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

        [AllowAnonymous]
        [Route("GetAllPersons")]
        [HttpGet]
        public HttpResponseMessage GetAllPersons()
        {
            try
            {
                var data=crudService.GetAllPersonsFullName();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public HttpResponseMessage UserLogin(Person person)
        {
            HttpStatusCode statusCode;
            try
            {
                var userValidation = crudService.Login(person);
                if( userValidation )
                {
                    String token = JwtManager.GenerateToken(person.UserName);
                    return Request.CreateResponse(HttpStatusCode.OK,token);
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
                return Request.CreateResponse(HttpStatusCode.OK, person);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
