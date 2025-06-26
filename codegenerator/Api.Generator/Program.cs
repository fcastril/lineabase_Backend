using Api;
using Api.MiddleException;
using CoreGenerator;
using CoreGenerator.LogicGenerator;
using CoreGenerator.LogicGenerator.Domain;
using CoreGenerator.LogicGenerator.UnitMSTest;
using CoreGenerator.LogicSqlServer;
using Microsoft.EntityFrameworkCore;
using SqliteConnector;
using SqlServerConnector;
using SqlServerConnector.SqlContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region SqlServer

var rest = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(rest, b => b.MigrationsAssembly("Api.Generator")));

builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

#endregion

#region Sqlite
builder.Services.AddScoped<ISqliteContext, SqliteContext>();

#region Repository
builder.Services.AddScoped<IArchitectureRepository, ArchitectureRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
#endregion
#endregion

#region BL
builder.Services.AddTransient<ICoreGenerator, GeneratorBL>();
builder.Services.AddTransient<IGeneratorDomainBL, GeneratorDomainBL>();
builder.Services.AddTransient<IGenerateUnitMSTEstBL, GenerateUnitMSTEstBL>();

builder.Services.AddTransient<IGeneratorInfraestructureBL, GeneratorInfraestructureBL>();
builder.Services.AddTransient<IGeneratorApi, GeneratorApiBL>();
builder.Services.AddTransient<IGeneratorServicesApplicationsBL, GeneratorServicesApplicationsBL>();
builder.Services.AddTransient<ISqlServerBL, SqlServerBL>();

//Arch
builder.Services.AddTransient<IArchitectureBL, ArchitectureBL>();
builder.Services.AddTransient<IProjectBL, ProjectBL>();

#endregion

#region cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                          policy.WithOrigins("http://localhost:4200", "https://localhost:4200");
                      });
});
#endregion

#region Mapper
builder.Services.AddAutoMapper(typeof(Mapper));
#endregion



var app = builder.Build();

app.UseMiddleware<MiddleHandlerException>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
