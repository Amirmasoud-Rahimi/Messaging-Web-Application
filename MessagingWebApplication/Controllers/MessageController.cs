using MessagingWebApplication.Filter;
using MessagingWebApplication.Models;
using MessagingWebApplication.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MessagingWebApplication.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {
        private readonly ICrudService crudService;

        public MessageController() => crudService = new CrudService();

        [JwtAuthentication]
        [Route("addMessage")]
        [HttpPost]
        public HttpResponseMessage AddMessage(Message message)
        {
            try
            {
                crudService.AddMessage(message);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [JwtAuthentication]
        [Route("getUserMessages/{userId}/{contactId}")]
        [HttpGet]
        public HttpResponseMessage GetUserMessages(int userId,int contactId)
        {
            try
            {
                var data = crudService.GetUserMessages(userId,contactId);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}