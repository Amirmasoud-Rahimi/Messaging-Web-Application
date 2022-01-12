using MessagingWebApplication.Models;
using System;
using System.Collections.Generic;

namespace MessagingWebApplication.Services
{
    public interface ICrudService
    {
        void AddUser(User user);

        List<UserDto> GetAllUsers();

        Boolean SignIn(String userName,String password);

        User GetUserById(int id);

        User GetUserByUserName(String userName);

        void AddMessage(Message message);

        List<MessageDto> GetUserMessages(int userId,int contactId);
    }
}