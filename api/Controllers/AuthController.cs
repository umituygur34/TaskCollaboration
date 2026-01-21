using Microsoft.AspNetCore.Mvc;
using TaskCollaboration.Api.api.DTOs;
using TaskCollaboration.Api.api.Interfaces;
using TaskCollaboration.Api.api.Models;
using System.Threading.Tasks;



namespace TaskCollaboration.Api.api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{



    private readonly IAuthService _authService;

    public AuthController(IAuthService authservice){
        _authService = authservice;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto){
        
        var result = await _authService.RegisterAsync(registerDto);

        return Ok(result);

    } 

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto){
        var result = await _authService.LoginAsync(loginDto);

        return Ok(result);
    }
    
}


    
