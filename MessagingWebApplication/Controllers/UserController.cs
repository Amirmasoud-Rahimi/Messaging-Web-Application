using MessagingWebApplication.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessagingWebApplication.Models;
using MessagingWebApplication.Filter;

namespace MessagingWebApplication.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {

       private readonly ICrudService crudService;

        public UserController() => crudService = new CrudService();

        [AllowAnonymous]
        [Route("addUser")]
        [HttpPost]
        public HttpResponseMessage AddUser(User user)
        {
            try
            {
               crudService.AddUser(user);
               return new HttpResponseMessage(HttpStatusCode.OK);
            }catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [JwtAuthentication]
        [Route("getAllUsers")]
        [HttpGet]
        public HttpResponseMessage GetAllUsers()
        {
            try
            {
                var data=crudService.GetAllUsers();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("signIn")]
        [HttpPost]
        public HttpResponseMessage SignIn(User user)
        {
            try
            {
                var userValidation = crudService.SignIn(user.UserName,user.Password);

                if( userValidation )
                {
                    string token = JwtManager.GenerateToken(user.UserName);
                    User u = crudService.GetUserByUserName(user.UserName);
                    UserDto dto = UserDto.SignInMapper(u, token);
                    return Request.CreateResponse(HttpStatusCode.OK,dto);
                }             
                 return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ex.Message);
            }
        }

        [JwtAuthentication]
        [Route("getUser/{userId}")]
        [HttpGet]
        public HttpResponseMessage Get(int userId)
        {
            try
            {
                User user = crudService.GetUserById(userId);
                return user != null ? Request.CreateResponse(HttpStatusCode.OK, user) :
                    Request.CreateResponse(HttpStatusCode.NotFound);                       
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ex.Message);
            }
        }
    }
}