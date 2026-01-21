using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskCollaboration.Api.api.Data;
using TaskCollaboration.Api.api.DTOs;
using TaskCollaboration.Api.api.Interfaces;
using TaskCollaboration.Api.api.Models;





namespace TaskCollaboration.Api.api.Services;

public class AuthService : IAuthService{

    private readonly TaskCollaborationDbContext _context;

    // DbContext'i içeri aldık (Dependency Injection)
    public AuthService(TaskCollaborationDbContext context)
    {
        _context = context;

    }



    public async Task<UserDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if(user == null){
            throw new Exception("User not found");
        }
        
        if(user.Password != loginDto.Password){
            throw new Exception("Invalid Password");
        }

        return new UserDto{

            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Token = "DUMMY_TOKEN"

        };

    }

    public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

        if(user != null){
            throw new Exception("User already exists");
        };

        var newUser = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Password = registerDto.Password
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Id = newUser.Id,
            Name = newUser.Name,
            Email = newUser.Email,
            Token = "DUMMY_TOKEN"
        };
    }
}