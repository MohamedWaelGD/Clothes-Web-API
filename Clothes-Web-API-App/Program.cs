using BloggerAPIApp.Services.ImagesServices;
using Clothes_Web_API_App;
using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Repository.CartRepository;
using Clothes_Web_API_App.Repository.CategoryRepository;
using Clothes_Web_API_App.Repository.ClothImageRepository;
using Clothes_Web_API_App.Repository.ClothItemRepository;
using Clothes_Web_API_App.Repository.ClothRepository;
using Clothes_Web_API_App.Repository.OrderRepository;
using Clothes_Web_API_App.Repository.ReviewRepository;
using Clothes_Web_API_App.Repository.UserRepository;
using Clothes_Web_API_App.Repository.UserRepository.UserAddressRepository;
using Clothes_Web_API_App.Repository.UserRepository.UserNumberRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme, e.g. \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("RedisConnection").GetValue<string>("Configuration");
    options.InstanceName = builder.Configuration.GetSection("RedisConnection").GetValue<string>("InstanceName");
});

builder.Services.AddScoped<IClothRepository, ClothRepository>();
builder.Services.AddScoped<IClothItemRepository, ClothItemRepository>();
builder.Services.AddScoped<IClothImageRepository, ClothImageRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserAddressRepository, UserAddressRepository>();
builder.Services.AddScoped<IUserNumberRepository, UserNumberRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(CustomMapper));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(options =>
{
    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
