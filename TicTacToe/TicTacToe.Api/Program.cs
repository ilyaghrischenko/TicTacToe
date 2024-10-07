using TicTacToe.Data.Repositories;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using TicTacToe.Application.AutentificationOptions;
using Microsoft.AspNetCore.Identity;
using TicTacToe.Application.Services;
using TicTacToe.Application.Services.Controllers;
using TicTacToe.Application.Services.DbModels;
using TicTacToe.Data;
using TicTacToe.Domain.Interfaces.Controllers;
using TicTacToe.Domain.Interfaces.DbModelsServices;
using TicTacToe.Validation;
using TicTacToe.Validation.SettingsModelsValidators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using TicTacToe.Domain.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//TODO: Прописать игровую локигу (WebSockets/SignalR). Сделать сохранения статистики.
//TODO: Почистить стили, код, скрипты.

//TODO: Сделать так, чтобы админ не мог лазить по страницам обычных пользователей, а пользователи не смогли лазить по страницам админа.

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddValidatorsFromAssemblyContaining<LoginModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ChangeEmailModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ChangeLoginModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ChangePasswordModelValidator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepository<Statistic>, StatisticRepository>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<IRepository<Report>, ReportRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddScoped<IAccountControllerService, AccountControllerService>();
builder.Services.AddScoped<IUserControllerService, UserControllerService>();
builder.Services.AddScoped<IFriendsControllerService, FriendsControllerService>();
builder.Services.AddScoped<ISettingsControllerService, SettingsControllerService>();
builder.Services.AddScoped<IAdminControllerService, AdminControllerService>();
builder.Services.AddScoped<IGameControllerService, GameControllerService>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddDbContext<TicTacToeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicTacToeContext")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = JwtOptions.ISSUER,

            ValidateAudience = true,
            ValidAudience = JwtOptions.AUDIENCE,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = JwtOptions.GetKey()
        };
    });
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description =
            "Input your JWT token in the 'Authorization' header like this: \"Authorization: Bearer {yourJWT}\""
    });
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
            Array.Empty<string>()
        }
    });
});

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