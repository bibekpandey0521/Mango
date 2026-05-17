using AutoMapper;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.OpenApi;
using Microsoft.IdentityModel.Tokens;
using Mango.Services.CouponAPI.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingConfig>();
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddSwaggerGen(option =>
//{
//    option.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });
//    option.AddSecurityRequirement(document => new OpenApiSecurityRequirement
//    {
//        {
//            // Pass the scheme ID and the document context into the reference constructor
//            new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme, document),
//            new List<string>() // Fix: Swashbuckle v10 requires List<string> instead of string[] / Array.Empty
//        }
//    });
//});

//var settingsSection = builder.Configuration.GetSection("ApiSettings");

//var secret = settingsSection.GetValue<string>("Secret");
//var issuer = settingsSection.GetValue<string>("Issuer");
//var audience = settingsSection.GetValue<string>("Audience");

//var key = Encoding.ASCII.GetBytes(secret);
//// Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFkbWluQGdtYWlsLmNvbSIsInN1YiI6ImQ2Mjc0NWU0LTdhZWUtNDFhMC04MTU3LTZiMDNjYWQxOTAwNiIsIm5hbWUiOiJhZG1pbkBnbWFpbC5jb20iLCJyb2xlIjoiQURNSU4iLCJuYmYiOjE3Nzg5OTgwOTQsImV4cCI6MTc3OTYwMjg5NCwiaWF0IjoxNzc4OTk4MDk0LCJpc3MiOiJtYW5nby1hdXRoLWFwaSIsImF1ZCI6Im1hbmdvLWNsaWVudCJ9.xzi8nXTzbpYFgcgtyNp4r7pWbhO4_b4uEu7VZDJio04
//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = true,
//        ValidIssuer = issuer,
//        ValidAudience = audience,
//        ValidateAudience = true
//    };
//});

builder.AddAppAuthentication();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthorization();

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
ApplyMigration();


app.Run();


void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}