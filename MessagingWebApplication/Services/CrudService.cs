using MessagingWebApplication.Models;
using BCryptTool = BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessagingWebApplication.Services
{
    public class CrudService : ICrudService
    {
        public void AddUser(User user)
        {
            using (var context = new MessagingDBUsers())
            {
                string salt = BCryptTool.GenerateSalt();
                user.Password=BCryptTool.HashPassword(user.Password, salt);
                user.DateOfJoining = new DateTime();
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public List<UserDto> GetAllUsers()
        {
            List<UserDto> userDtoList = new List<UserDto>();
            using (var context=new MessagingDBUsers())
            {
                context.Users.ToList().ForEach(user =>
                {    
                    userDtoList.Add(UserDto.Mapper(user));
                });
            }
            return userDtoList;
        }

        public Boolean SignIn(string userName, string password)
        {
            using (var context=new MessagingDBUsers())
            {
               var user = context.Users.SingleOrDefault(u => u.UserName == userName);            
                return user != null && BCryptTool.Verify(password, user.Password);
            }                          
        }

        public User GetUserById(int id)
        {
            using (var context = new MessagingDBUsers())
            {
                return context.Users.FirstOrDefault(user => user.UserId == id);
            }
        }

        public User GetUserByUserName(string userName)
        {
            using (var context = new MessagingDBUsers())
            {
                return context.Users.FirstOrDefault(user => user.UserName == userName);
            }
        }

        public void AddMessage(Message message)
        {
           using(var context=new MessagingDBMessages())
            {
                message.SendingDate = DateTime.Now;
                context.Messages.Add(message);
                context.SaveChanges();
            }
        }

        public List<MessageDto> GetUserMessages(int userId,int contactId)
        {
            List<MessageDto> messageDtoList = new List<MessageDto>();
            List<Message> messages;
            using (var context = new MessagingDBMessages())
            {
                    messages = context.Messages.Where(
                    message => (message.SenderId == userId
                    && message.ReceiverId==contactId)||
                    (message.SenderId == contactId
                    && message.ReceiverId == userId)
                    ).ToList();
            }
            messages.ForEach(m =>
            {
                messageDtoList.Add(MessageDto.Mapper(m));
            });
               return messageDtoList;
        }
    }
}