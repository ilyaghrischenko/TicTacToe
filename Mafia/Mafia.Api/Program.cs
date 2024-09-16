using Mafia.Data.Repositories;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Mafia.Application.Services;
using Mafia.Application.Services.Controllers;
using Mafia.Application.Services.Models;
using Mafia.Domain.Interfaces.Controllers;
using Mafia.Domain.Interfaces.ModelsServices;
using Mafia.Validation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddValidatorsFromAssemblyContaining<LoginModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterModelValidator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepository<Friend>, FriendRepository>();
builder.Services.AddScoped<IRepository<GameRole>, RoleRepository>();
builder.Services.AddScoped<IRepository<Statistic>, StatisticRepository>();
builder.Services.AddScoped<IAccountControllerService, AccountControllerService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IMafiaService, MafiaService>();
builder.Services.AddScoped<ISheriffService, SheriffService>();
builder.Services.AddScoped<ISlutService, SlutService>();

builder.Services.AddScoped<PasswordHasher<User>>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();

app.Run();