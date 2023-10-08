using AstraLicenceManager.Entities;
using AstraLicenceManager.Service;
using Vjezba.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<ICompanyService, CompanyService>();

builder.Services.AddTransient<IAppService,AppService>();

builder.Services.AddTransient<ILicenceService, LicenceService>();

builder.Services.AddTransient<IAppLevelService, AppLevelService>();

builder.Services.AddTransient<IAuthenticateService, AuthenticateService>();

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<ErrorHandlingMiddleware>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJNYXJrY0BnbWFpbC5jb20ifQ.RHiZsiQBAXNqc6TxQdbK6uXPnhSM5E91f-qD08TXpbE"))
        };
    });

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseAuthentication();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
