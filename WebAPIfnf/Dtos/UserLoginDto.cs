// Dtos/UserLoginDto.cs
namespace WebApi.Dtos
{
    public class UserLoginDto
    {
        public required string email { get; set; }
        public required string password { get; set; }
    }
}
