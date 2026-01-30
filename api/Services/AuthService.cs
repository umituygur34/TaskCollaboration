using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskCollaboration.Api.Data;
using TaskCollaboration.Api.DTOs;
using TaskCollaboration.Api.Interfaces;
using TaskCollaboration.Api.Models;
using TaskCollaboration.Api.Settings;
using BCrypt.Net;
using TaskCollaboration.Api.Exceptions;


namespace TaskCollaboration.Api.Services;

public class AuthService : IAuthService
{

    private readonly TaskCollaborationDbContext _context;
    private readonly JwtSettings _jwtSettings;

    // DbContext'i içeri aldık (Dependency Injection)
    public AuthService(TaskCollaborationDbContext context, IOptions<JwtSettings> jwtSettingsOptions)
    {
        _context = context;
        _jwtSettings = jwtSettingsOptions.Value;

    }

    public string GenerateJwtToken(User user)
    {
        //token oluşturma işlemi

        var claims = new[]
        {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Name)
        };

        //secret key üretildi.
        var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_jwtSettings.TokenKey));

        //key'i ve algoritmayı kullanarak imzalama credentials oluşturuldu.
        var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
            key,
            Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler()
            .WriteToken(tokenDescriptor);
    }

    public async Task<UserDto> LoginAsync(LoginDto loginDto)
    {

        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        //db'deki Hash'in "salt"ını al, loginDto.password'dan gelen şifreyi bu salt ile tekrar hashle ve karşılaştır.
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
        {
            return null;
        }


        var token = GenerateJwtToken(user);

        return new UserDto
        {

            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Token = token

        };

    }

    public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

        if (user != null)
        {
            throw new ConflictException("User already exists");
        }
        ;

        //password Hash
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        var newUser = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Password = hashedPassword

        };
        //Console.WriteLine("New User Password: " + newUser.Password);

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        var token = GenerateJwtToken(newUser);


        return new UserDto
        {
            Id = newUser.Id,
            Name = newUser.Name,
            Email = newUser.Email,
            Token = token

        };

    }
}