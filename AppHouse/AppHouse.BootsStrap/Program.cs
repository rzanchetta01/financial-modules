using AppHouse.Accounts.Core;
using AppHouse.Gateway.Endpoints;
using AppHouse.Gateway.Middlewares;
using AppHouse.Loans.Core;
using AppHouse.SharedKernel.Core.BaseClasses;
using MediatR;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();


#region Services DI
//Mongo Log Start
builder.AddMongoDBClient("dbMongo");

//Base Context Start
AppHouse.Loans.Application.StartupServices.AddLoansStartup(builder.Services);
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
});

//Loan Start
AppHouse.Loans.Application.StartupServices.AddLoansStartup(builder.Services);
builder.AddNpgsqlDbContext<LoanContext>("dbPostgres", e =>
{
    e.ConnectionString = builder.Configuration.GetConnectionString("psql");
    e.DbContextPooling = true;
});

//Cache Start
builder.Services.AddMemoryCache();//TODO change later to Redis


//MediatR
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblyContaining<AppHouse.Accounts.Application.Init>();
    c.RegisterServicesFromAssemblyContaining<AppHouse.Loans.Application.Init>();

});


#endregion

#region Middlewares services
builder.Services.AddTransient<CorsMiddleware>();
builder.Services.AddTransient<GlobalErrorMiddleware>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(EventLoggingAndValidationMiddleware<,>));
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
LoanEndpoints.Setup(app);
#endregion

app.Run();

