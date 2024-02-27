using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WebAPIDemo.Data;
using WebAPIDemo.Filters.OperationFilters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarStoreManagement"));
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApiVersioning(Options =>
{
    Options.ReportApiVersions = true;
    Options.AssumeDefaultVersionWhenUnspecified = true;
    Options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    //Options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});

builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Api Demo ver 1.0", Version = "Version 1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Web Api Demo ver 2.0", Version = "Version 2" });

    c.OperationFilter<AuthorizationHeaderOperationFilter>();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(Options =>
    {
        Options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi ver 1.0");
        Options.SwaggerEndpoint("/swagger/v2/swagger.json", "WebApi ver 2.0");
    });
}


app.UseHttpsRedirection();

app.MapControllers();


app.Run();

