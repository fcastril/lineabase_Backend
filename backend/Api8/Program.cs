using Api.Common.MiddleException;
using Api.Installers;
using BlobStorageMtow;
using Domain.Port;
using Infrastructure;
using Infrastructure.Integrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceApplication;
using System.Reflection;
using System.Text;
using Util.Common;
using Utilidades;
using UtilNuget.Class;
using UtilNuget.Interfaces;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);

var app = builder.Build();

app.UseMiddleware<MiddleHandlerException>();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(Constants.MyAllowSpecificOrigins);
app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddResponseCompression();
    services.AddHttpContextAccessor();

    services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    services.AddTransient(typeof(IHttpClient), typeof(HttpClientCustom));
    services.AddTransient(typeof(IUtil), typeof(Utilities));
    services.AddTransient<ITableStorage<Transactions>, AzureTableBlobStorage<Transactions>>();
    services.AddTransient<ITableStorage<EntityBridge>, AzureTableBlobStorage<EntityBridge>>();
    services.AddTransient<IAzureStorage, AzureStorage>();

    services.AddDependencyInjectionsInfrastructure(builder.Configuration);
    services.AddDependencyInjectionsApplications();
    services.AddMapperDependencyInjection();
    services.AddMediatrDependecyInjection();
    JwtNoSql(services);
    services.AddAuthorization();
    services.AddControllers();

    ConfiguracionBase(services);

    services.AddDependencyInjectionsQueue(builder.Configuration);
}

void ConfiguracionBase(IServiceCollection services)
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();

    #region swagger
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GithubApp - DevEx", Version = "v1", Description = "Api for the communication and orchestrator with source and destine VCS" });
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
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {{
        new OpenApiSecurityScheme {
          Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
          }},
        new string[] {}
      }
    });
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
    #endregion

    #region Cors
    services.AddCors(options =>
    {
        options.AddPolicy(name: Constants.MyAllowSpecificOrigins, builder =>
        {
            builder.WithOrigins("http://example.com", "http://localhost:4200","http://localhost:3000", "*",
              "https://salmon-sky-0e72e2f0f.4.azurestaticapps.net")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
    });
    #endregion
}

void JwtNoSql(IServiceCollection services)
{
    #region JWT-mongo
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTMONGO:Secret"]))
        };
    });
    #endregion
}
