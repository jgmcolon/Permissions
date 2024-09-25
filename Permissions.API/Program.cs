using Permissions.Application;
using Permissions.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Permissions.API;
using Permissions.Shared;
using Microsoft.Extensions.Configuration;
using System.Runtime;
using Asp.Versioning;
using Permissions.Infrastructure.indexSearch;
using Permissions.Infrastructure.Streaming;

var builder = WebApplication.CreateBuilder(args);


var AllowSpecificOrigins = "AllowAccess";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});



// Add services to the container.
builder.Host.UseSerilog();

var appsettings = new Appsettings(); 

builder.Configuration.Bind(appsettings);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(appsettings);
builder.Services.AddElasticsearch(appsettings);
builder.Services.AddKafka(appsettings);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services
               .AddApiVersioning(options =>
               {
                   options.DefaultApiVersion = new ApiVersion(1);
                   options.ReportApiVersions = true;
                   options.ApiVersionReader = new UrlSegmentApiVersionReader();
               })
               .AddMvc()
               .AddApiExplorer(options =>
               {
                   options.GroupNameFormat = "'v'V";
                   options.SubstituteApiVersionInUrl = true;
               });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
//}

app.ApplyMigrations();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

app.UseCors(AllowSpecificOrigins);
app.UseCors(builder =>
{
    builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true);
});




app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.Run();
