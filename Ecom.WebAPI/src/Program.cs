using System.Text;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Enum;
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
using Microsoft.OpenApi.Models;
using Npgsql;



var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ecom", Version = "v1" });
    }); builder.Services.AddScoped<ExceptionHandeler>();


// declare services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();

builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();


var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Store"));
dataSourceBuilder.MapEnum<Role>();
dataSourceBuilder.MapEnum<OrderStatus>();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddDbContext<DataBaseContext>(options => options.UseNpgsql(dataSource));


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


app.UseCors(options =>
{
    options
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
});


app.UseSwagger();
app.UseSwaggerUI(c =>
  {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
      c.RoutePrefix = string.Empty;
  }
);


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();
