
using TaskCollaboration.Api.api.DTOs;
using TaskCollaboration.Api.api.Models;



namespace TaskCollaboration.Api.api.Interfaces;

public interface IAuthService{
    
    public Task<UserDto> RegisterAsync (RegisterDto registerDto);
    public Task<UserDto> LoginAsync (LoginDto loginDto);

    
}