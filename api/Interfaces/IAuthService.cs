using TaskCollaboration.Api.DTOs;
using TaskCollaboration.Api.Models;



namespace TaskCollaboration.Api.Interfaces;

public interface IAuthService
{

    public Task<UserDto> RegisterAsync(RegisterDto registerDto);
    public Task<UserDto> LoginAsync(LoginDto loginDto);

    //girdi user çıktı jwt token 
    public string GenerateJwtToken(User user);


}