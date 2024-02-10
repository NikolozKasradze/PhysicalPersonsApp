using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PhysicalPersons.API.Filters;
using PhysicalPersonsApp.API.Middlewares;
using PhysicalPersonsApp.Application;
using PhysicalPersonsApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddLocalization();//weird
builder.Services.AddControllers(config =>
{
    config.Filters.Add<ValidationFilter>();
})
.AddMvcOptions(options =>
{
    options.EnableEndpointRouting = false;
});
builder.Services.AddCors(x =>
{
    x.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Person API",
        Description = "A test Person API"
    });
    List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlFile => o.IncludeXmlComments(xmlFile));
});

var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var dbcontext = services.GetRequiredService<PersonsAppDbContext>();
    dbcontext.Database.Migrate();
}
// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();

app.RegisterLocalization();
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMvc();

app.Run();
