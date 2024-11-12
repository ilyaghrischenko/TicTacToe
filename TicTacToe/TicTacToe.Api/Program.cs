using TicTacToe.Data.Repositories;
using TicTacToe.Domain.DbModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using TicTacToe.Application.AutentificationOptions;
using Microsoft.AspNetCore.Identity;
using TicTacToe.Application.Services;
using TicTacToe.Application.Services.Controllers;
using TicTacToe.Application.Services.DbModels;
using TicTacToe.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;
using TicTacToe.Api.Middleware;
using TicTacToe.Application.Services.Token;
using TicTacToe.Application.SignalRHub;
using TicTacToe.Contracts;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Contracts.DbModelsServices;
using TicTacToe.Contracts.Repositories;
using TicTacToe.Contracts.TokenServices;
using TicTacToe.Validation.AccountModelsValidators;
using TicTacToe.Validation.MainModelsValidators;
using TicTacToe.Validation.SettingsModelsValidators;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddSignalR();

#region Validators
builder.Services.AddValidatorsFromAssemblyContaining<LoginModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ChangeEmailModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ChangeLoginModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ChangePasswordModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BugModelValidator>();
#endregion

#region Dependency Injection
#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepository<Statistic>, StatisticRepository>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<IRepository<Report>, ReportRepository>();
builder.Services.AddScoped<IRepository<Bug>, BugRepository>();
#endregion

#region Services
#region DbModels
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IStatisticService, StatisticService>();
builder.Services.AddScoped<IBugService, BugService>();
#endregion

#region Controllers
builder.Services.AddScoped<IAccountControllerService, AccountControllerService>();
builder.Services.AddScoped<IUserControllerService, UserControllerService>();
builder.Services.AddScoped<IFriendsControllerService, FriendsControllerService>();
builder.Services.AddScoped<ISettingsControllerService, SettingsControllerService>();
builder.Services.AddScoped<IAdminControllerService, AdminControllerService>();
builder.Services.AddScoped<IBugControllerService, BugControllerService>();
#endregion

#region Other
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITokenBlacklistService, TokenBlacklistService>();
#endregion
#endregion

#region Other
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IMemoryCache, MemoryCache>();
#endregion
#endregion

#region JWT
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
#endregion

builder.Services.AddDbContext<TicTacToeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicTacToeContext")));

WebApplication app = builder.Build();

#region DatabseInitializer
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<TicTacToeContext>();
        DatabaseInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while initializing the database: {ex.Message}");
    }
}
#endregion

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

app.MapHub<GameHub>("/gameHub");

#region Middlewares
app.UseMiddleware<TokenBlacklistMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
#endregion

app.Run();