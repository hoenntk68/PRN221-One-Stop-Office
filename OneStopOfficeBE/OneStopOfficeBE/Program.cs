using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OneStopOfficeBE.CustomAttributes;
using OneStopOfficeBE.Models;
using OneStopOfficeBE.Services;
using OneStopOfficeBE.Services.Impl;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);
        IConfiguration configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);
        builder.Services.AddCors(o => o.AddPolicy(name: MyAllowSpecificOrigins, policy =>
        {
            policy.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options => options.AddDefaultPolicy(
            policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

        var secretKey = configuration.GetSection("AppSettings:SecretKey").Value;
        var secretKeyByte = Encoding.UTF8.GetBytes(secretKey);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),

                    ClockSkew = TimeSpan.Zero
                };

            });

        builder.Services.AddDbContext<PRN221_OneStopOfficeContext>();
        builder.Services.AddScoped<ValidateTokenAttribute>();
        builder.Services.AddScoped<UserService, UserServiceImpl>();
        builder.Services.AddScoped<RequestService, RequestServiceImpl>();
        builder.Services.AddScoped<CategoryService, CategoryServiceImpl>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}