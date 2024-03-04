using bookstore_backend.Data; // Assuming this is your DbContext file path
using bookstore_backend.Services.Interfaces;
using bookstore_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<IUserService, UserService>();





// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {

        var secret = Encoding.ASCII.GetBytes("this_is_a_very_strong_key_sovidinjso;diifh");
        var ourSecret = new SymmetricSecurityKey(secret);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            // Set your specific issuer (your backend server)
            IssuerSigningKey = ourSecret, // Replace with your implementation

            // Set your valid audience (your Angular app)
            ValidAudience = "Bookstore", // Replace with your app name

            ClockSkew = TimeSpan.Zero // Consider a small tolerance for clock differences
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add authentication middleware
app.UseAuthentication();

// Add authorization middleware
app.UseAuthorization();

app.MapControllers();

app.Run();

// Replace this with your implementation to generate a secret key

