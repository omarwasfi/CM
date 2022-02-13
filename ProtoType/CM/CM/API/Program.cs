using System.Configuration;
using System.Reflection;
using System.Text;
using CM.Library;
using CM.Library.DataModels;
using CM.Library.DBContexts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;



var builder = WebApplication.CreateBuilder(args);


//serilog Logger
builder.Host.UseSerilog((ctx, lc) => lc
       .WriteTo.Console()
       .ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CM API",
        Description = "The server side api",
        TermsOfService = new Uri("https://omarwasfi.com"),
        Contact = new OpenApiContact
        {
            Name = "Omar Wasfi",
            Email = "contact@omwasfi.com",
            Url = new Uri("https://omarwasfi.com"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://omarwasfi.com"),
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});




builder.Services.AddMediatR(typeof(CMLibraryMediatREntryPoint).Assembly);

builder.Services.AddValidatorsFromAssembly(typeof(CMLibraryMediatREntryPoint).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

var eventsDatabaseConnectionString = builder.Configuration.GetConnectionString("EventsDatabaseConnectionString");
var currentStateDatabaseConnectionString = builder.Configuration.GetConnectionString("CurrentStateDatabaseConnectionString");

builder.Services.AddDbContext<EventsDBContext>(options =>
              options
              .UseMySql(
                   eventsDatabaseConnectionString,
                   ServerVersion.AutoDetect(eventsDatabaseConnectionString),
                   b => b.MigrationsAssembly("CM.API")
                   ));

builder.Services.AddDbContext<CurrentStateDBContext>(options =>
options
.UseMySql(
     currentStateDatabaseConnectionString,
     ServerVersion.AutoDetect(currentStateDatabaseConnectionString),
     b => b.MigrationsAssembly("CM.API")
     ));

builder.Services.AddIdentity<PersonDataModel, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
}
).AddEntityFrameworkStores<CurrentStateDBContext>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.SaveToken = true;
});
builder.Services.AddAuthorization();


builder.Services.AddAutoMapper(typeof(Program));





var app = builder.Build();
Log.Information("CM.API Started");

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
/*
try
{

    var app = builder.Build();
    Log.Information("CM.API Started");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSerilogRequestLogging();


    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();



}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

*/
