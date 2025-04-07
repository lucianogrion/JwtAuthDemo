using System.Text;
using JwtAuthDemo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_key"))
//    };
//});

builder.Services.AddAuthentication()
  .AddJwtBearer(opt =>
  {
      //...
      opt.Events = new JwtBearerEvents()
      {
          OnMessageReceived = msg =>
          {
              var token = msg?.Request.Headers.Authorization.ToString();
              string path = msg?.Request.Path ?? "";
              if (!string.IsNullOrEmpty(token))

              {
                  Console.WriteLine("Access token");
                  Console.WriteLine($"URL: {path}");
                  Console.WriteLine($"Token: {token}\r\n");
              }
              else
              {
                  Console.WriteLine("Access token");
                  Console.WriteLine("URL: " + path);
                  Console.WriteLine("Token: No access token provided\r\n");
              }
              return Task.CompletedTask;
          }
      };
  });


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();

app.Run();

