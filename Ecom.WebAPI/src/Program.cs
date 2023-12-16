using System.Text;
using Ecom.Core.src.Abstraction;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.Service;
using Ecom.Service.src.Shared;
using Ecom.WebAPI.src.Database;
using Ecom.WebAPI.src.Middleware;
using Ecom.WebAPI.src.Repository;
using Ecom.WebAPI.src.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ExceptionHandeler>();


// declare services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddDbContext<DataBaseContext>(options => options.UseNpgsql());


// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });


var app = builder.Build();

app.UseMiddleware<ExceptionHandeler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();
