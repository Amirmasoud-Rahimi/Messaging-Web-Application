using MessagingWebApplication.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessagingWebApplication.Models;

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

        [Route("Login")]
        [HttpPost]
        public HttpResponseMessage UserLogin(Person person)
        {
            HttpStatusCode statusCode;
            try
            {
                var userValidation = crudService.Login(person);
                statusCode = userValidation == true ? HttpStatusCode.OK : HttpStatusCode.Unauthorized;                                
            }catch(Exception)
            {
                statusCode=HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(statusCode);
        }

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
