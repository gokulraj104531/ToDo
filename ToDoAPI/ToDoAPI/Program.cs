using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;
using ToDoAPI.Repositories;
using ToDoAPI.Repositories.Interfaces;
using ToDoAPI.Services;
using ToDoAPI.Services.Interfaces;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("Log/log.txt",rollingInterval:RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();
builder.Logging.AddSerilog();

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IToDoListRepository,ToDoListRepository>();
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddTransient<IToDoListService, ToDoListService>();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((corsOptions) =>
{
    corsOptions.AddPolicy("Mypolicy", (policyoptions) =>
    {
        policyoptions.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Mypolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
