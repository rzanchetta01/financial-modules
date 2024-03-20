using AppHouse.Accounts.Core;
using AppHouse.Gateway.Endpoints;
using AppHouse.Gateway.Middlewares;
using AppHouse.SharedKernel.BaseClasses;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();


#region Services DI
//Mongo Log Start
builder.AddMongoDBClient("dbMongo");

//Base Context Start
builder.AddNpgsqlDbContext<BaseContext>("dbPostgres", e =>
{
    e.ConnectionString = builder.Configuration.GetConnectionString("psql");
    e.DbContextPooling = true;
});

//Account Start
AppHouse.Accounts.Application.StartupServices.AddAccountStartup(builder.Services);
builder.AddNpgsqlDbContext<AccountsContext>("dbPostgres", e => 
{
    e.ConnectionString = builder.Configuration.GetConnectionString("psql");
    e.DbContextPooling = true;
#if DEBUG
    e.Metrics = true;
    e.Tracing = true;
    e.HealthChecks = true;
    e.DbContextPooling = false;
#endif
});


//MediatR
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblyContaining<AppHouse.Accounts.Application.Init>();
    
    //pipeline middlewares
    c.AddOpenBehavior(typeof(EventLoggingAndValidationMiddleware<,>));
});


#endregion

#region Middlewares services
builder.Services.AddTransient<CorsMiddleware>();
builder.Services.AddTransient<GlobalErrorMiddleware>();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}
#region Middlewares activation
app.UseMiddleware<CorsMiddleware>();
app.UseMiddleware<GlobalErrorMiddleware>();
#endregion

#region Controllers
AccountEndpoints.Setup(app);
#endregion

app.Run();

