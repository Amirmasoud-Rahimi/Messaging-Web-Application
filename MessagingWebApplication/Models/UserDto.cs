using System;

namespace MessagingWebApplication.Models
{
    public class UserDto
    {
        public int Id { get; set; } 
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String DateOfJoining { get; set; }
        public String PhotoFileName { get; set; }
        public String Token { get; set; }

        public static UserDto Mapper(User user)
        {
            return new UserDto
            {
                Id = user.UserId,
                UserName = user.UserName,
                FullName = user.FullName,
                DateOfJoining = user.DateOfJoining.ToString(),
                PhotoFileName = user.PhtotFileName
            };
        }

        public static UserDto SignInMapper(User user,String token)
        {
            UserDto userDto = UserDto.Mapper(user);
            userDto.Token = token;
            return userDto;
        }
    }
}