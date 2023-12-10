using Ecom.Core.src.Abstraction;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.Service;
using Ecom.Service.src.Shared;
using Ecom.WebAPI.src.Database;
using Ecom.WebAPI.src.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// declare services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddDbContext<DataBaseContext>(options => options.UseNpgsql());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
