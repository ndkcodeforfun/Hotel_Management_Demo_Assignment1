using BAL.Mapper;
using BAL.Service.Implements;
using BAL.Service.Interfaces;
using DAL.Models;
using DAL.UnitOfWork.Implement;
using DAL.UnitOfWork.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FUMiniHotelManagementContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
builder.Services.AddScoped<IRoomInformationService, RoomInformationService>();
builder.Services.AddScoped<IBookingReservationService, BookingReservationService>();
builder.Services.AddScoped<IBookingDetailService, BookingDetailService>();

builder.Services.AddAutoMapper(typeof(Program), typeof(Mapping));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireClaim("Role", "ADMIN"));
    options.AddPolicy("RequireContractorRole", policy => policy.RequireClaim("Role", "CONTRACTOR"));
    options.AddPolicy("RequireCustomerRole", policy => policy.RequireClaim("Role", "CUSTOMER"));
    options.AddPolicy("RequireAdminOrContractorRole", policy => policy.RequireClaim("Role", "ADMIN", "CONTRACTOR"));
    options.AddPolicy("RequireAdminOrCustomerRole", policy => policy.RequireClaim("Role", "ADMIN", "CUSTOMER"));
    options.AddPolicy("RequireContractorOrCustomerRole", policy => policy.RequireClaim("Role", "CONTRACTOR", "CUSTOMER"));
    options.AddPolicy("RequireAllRoles", policy => policy.RequireClaim("Role", "ADMIN", "CONTRACTOR", "CUSTOMER"));
});

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
