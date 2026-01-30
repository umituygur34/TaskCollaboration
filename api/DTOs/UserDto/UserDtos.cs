
namespace TaskCollaboration.Api.DTOs
{

    //Giriş yaparken dönülecek veriler
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //JWT Token buraya gelecek.
        public string Token { get; set; } = string.Empty;

    }

    //Kayıt olurken istenen veriler
    public class RegisterDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    //Giriş yaparken istenen veri
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}



