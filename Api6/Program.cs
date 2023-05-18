using Api.Common.MiddleException;
using Api.Installers;
using Api6.Common;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceApplication;
using ServiceBus.HandlerAzureServiceBus;
using System.Text;
using Util.Common;
using Utilidades;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);

var app = builder.Build();

app.UseMiddleware<MiddleHandlerException>();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{

    //this.Sql(services);
    services.AddResponseCompression();
    services.AddHttpContextAccessor();

    services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    services.AddTransient<IUtil, Utilities>();
    builder.Services.AddTransient<IServicesBusHandler, ServiceSenderHandler>();

    services.AddDependencyInjectionsInfrastructure(builder.Configuration);
    services.AddDependencyInjectionsApplications();

    services.AddMediatrDependecyInjection();

    Jwt(services);
    services.AddAuthorization();
    services.AddControllers();


    ConfiguracionBase(services);
}

void ConfiguracionBase(IServiceCollection services)
{


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();


    #region swagger
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(ConstantsAPI.VersionProject,new OpenApiInfo
        {
            Title = ConstantsAPI.NameProject,
            Version = ConstantsAPI.VersionProject,
            Description = ConstantsAPI.DescriptionProject,
            Contact = new OpenApiContact
            {
                Email = ContactProject.Email,
                Name = ContactProject.Name
            }
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "Bearer {token}",
            In = ParameterLocation.Header,
            Description = "Enter �Bearer� [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        });
        c.OperationFilter<RequiredHeaderParameter>();
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                            {
                            new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}
                            }
                    });
    });
    #endregion

    #region Cors
    services.AddCors(options =>
    {
        options.AddPolicy(name: Constants.MyAllowSpecificOrigins,
                          builder =>
                          {
                              builder.WithOrigins("http://example.com",
                                                  "http://localhost:4200", "*")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials();
                          });
    });
    #endregion

}

void Jwt(IServiceCollection services)
{
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                //ValidAudience = Configuration["JWT:ValidAudience"],
                ValidateIssuer = false,
                //ValidIssuer = Configuration["JWT:ValidIssuer"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
            };
        });
}

