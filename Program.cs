using FoodDelivery.Models;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services
//builder.Services.AddScoped<IUserRegisterService, UserRegisterService>(); // exist in one request
//builder.Services.AddTransient<IUserRegisterService, UserRegisterService>(); // exist in one request to servise
//builder.Services.AddSingleton<IUserRegisterService, UserRegisterService>(); // exist all time

IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
builder.Services.AddSingleton(configuration.GetSection("AppSettings").Get<AppSettings>());

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();

//DB
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FDDbContext>(options => options.UseNpgsql(connection));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings").GetSection("JwtKey").Value)),
                            ValidateIssuerSigningKey = true,
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnTokenValidated = async context =>
                            {
                                FDDbContext dbContext = context.HttpContext.RequestServices.GetRequiredService<FDDbContext>();
                                if (context.SecurityToken is JwtSecurityToken jwtSecurityToken)
                                {
                                    string encodedJwtToken = jwtSecurityToken.RawData;

                                    if(await dbContext.BannedTokens.AnyAsync(x => x.Token == encodedJwtToken))
                                    {
                                        context.Fail("Token banned");
                                    }
                                }
                            }
                        };
                    });

builder.Services.AddMvc().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



