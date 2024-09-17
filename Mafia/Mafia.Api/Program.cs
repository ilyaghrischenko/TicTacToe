using Mafia.Data.Repositories;
using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mafia.Application.AutentificationOptions;
using Microsoft.AspNetCore.Identity;
using Mafia.Application.Services;
using Mafia.Application.Services.Controllers;
using Mafia.Application.Services.Models;
using Mafia.Data;
using Mafia.Domain.Interfaces.Controllers;
using Mafia.Domain.Interfaces.ModelsServices;
using Mafia.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

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
builder.Services.AddScoped<IUserControllerService, UserControllerService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IMafiaService, MafiaService>();
builder.Services.AddScoped<ISheriffService, SheriffService>();
builder.Services.AddScoped<ISlutService, SlutService>();

builder.Services.AddScoped<PasswordHasher<User>>();
builder.Services.AddDbContext<MafiaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MafiaContext")));
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