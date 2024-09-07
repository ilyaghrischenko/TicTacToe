using Data.Repositories;
using Domain.DbModels;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Service.Interfaces;
using Service.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IRepository<Friend>, FriendRepository>();
builder.Services.AddSingleton<IRepository<Role>, RoleRepository>();
builder.Services.AddSingleton<IRepository<Statistic>, StatisticRepository>();
builder.Services.AddSingleton<IAccountService, AccountService>();

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