
namespace TaskCollaboration.Api.DTOs
{

    //Giriş yaparken 
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //JWT Token buraya gelecek.
        public string Token { get; set; } = string.Empty;

    }

    //Kayıt olurken istenenler
    public class RegisterDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    //Giriş yaparken istenenler
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}



