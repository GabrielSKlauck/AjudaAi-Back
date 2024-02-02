using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rest.Contracts.Repository;
using Rest.Repository;
using Rest.Infrastructure;
using System.Configuration;
using System.Text;
using Configuration = Rest.Infrastructure.Configuration;
using ProjetoBack.Contracts.Repository;
using ProjetoBack.Repository;
using ProjetoBack.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<INGORepository, NGORepository>();

builder.Services.AddTransient<IStateRepository, StateRepository>();

builder.Services.AddTransient<ICityRepository, CityRepository>();

builder.Services.AddTransient<ICausesRepository, CausesRepository>();

builder.Services.AddTransient<IAdsRepository, AdsRepository>();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IUserAdsRepository, UserAdsRepository>();

builder.Services.AddTransient<IUserCausesRepository, UserCausesRepository>();

builder.Services.AddTransient<INgoImagesRepository, NgoImagesRepository>();

builder.Services.AddTransient<IAchievementsRepository, AchievementRepository>();

builder.Services.AddTransient<IAchievementsUserRepository, AchievementUserRepository>();

builder.Services.AddTransient<IUserImageRepository, UserImageRepository>();

builder.Services.AddCors();

var key = Encoding.ASCII.GetBytes(Configuration.JWTSecret);

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>() { }
        }
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
});

app.Run();
