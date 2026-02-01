using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskCollaboration.Api.Data;
using TaskCollaboration.Api.Interfaces;
using TaskCollaboration.Api.Services;
using TaskCollaboration.Api.Settings;
using Microsoft.OpenApi.Models;
using TaskCollaboration.Api.Middleware;
using FluentValidation.AspNetCore;
using FluentValidation;





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
builder.Services.AddSwaggerGen(options =>
{
    // 1. Kapıdaki "Kilit" sembolünü ve kutucuğu aktifleştirir
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Token giriniz. Örnek: Bearer {token}"
    });

    // 2. Kilidi tüm endpoint'lere uygular
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly)

.AddFluentValidationAutoValidation();




builder.Services.AddAuthorization();

//DbContext Configuration Eklendi
builder.Services.AddDbContext<TaskCollaborationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IWorkTaskService, WorkTaskService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/*Middleware sırası çok önemlidir. 
Hata yakalayıcıyı en başa (veya en başa yakın) koyarız ki,
 kendisinden sonra gelen tüm işlemleri
(Authentication, Authorization, Controllers)
 sarmalasın ve oralarda bir hata olursa yakalayabilsin.*/
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();