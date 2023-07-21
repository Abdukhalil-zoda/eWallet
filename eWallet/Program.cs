using AutoMapper;
using eWallet;
using eWallet.Data.Models;
using eWallet.Repositories;
using eWallet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("PgSQL")));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "CustomScheme";
        options.DefaultChallengeScheme = "CustomScheme";
    })
    .AddScheme<JwtBearerOptions, eWalletAuthenticationHandler>("CustomScheme", jwtOptions =>
    {
        jwtOptions.SaveToken = true;
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"]!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            NameClaimType = "X-Digest",
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "eWallet", Version = "v1" });
    c.OperationFilter<SwaggerOperationFilters>();
});

builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
builder.Services.AddScoped<IBaseRepository<Wallet>, WalletRepository>();
builder.Services.AddScoped<IBaseRepository<Transaction>, TransactionRepository>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IWalletService, WalletService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());


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
