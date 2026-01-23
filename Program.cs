using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskCollaboration.Api.api.Data;
using TaskCollaboration.Api.api.Interfaces;
using TaskCollaboration.Api.api.Services;
using TaskCollaboration.Api.Settings;



var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["AppSettings:TokenKey"]
    ?? throw new InvalidOperationException(
        "JWT TokenKey is missing. Please set AppSettings:TokenKey in appsettings.json.");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});




// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TaskCollaboration API", Version = "v1" });
});
builder.Services.AddControllers();



//DbContext Configuration Eklendi
builder.Services.AddDbContext<TaskCollaborationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
