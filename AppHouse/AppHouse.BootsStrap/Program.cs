using AppHouse.Accounts.Core;
using AppHouse.BootsStrap.Endpoints;
using AppHouse.BootsStrap.Middlewares;
using AppHouse.Loans.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();


#region Services DI
//Mongo Log Start
builder.AddMongoDBClient("dbMongo");


//Account Start
AppHouse.Accounts.Application.StartupServices.AddAccountStartup(builder.Services);
builder.AddNpgsqlDbContext<AccountsContext>("dbPostgres", e => 
{
    e.ConnectionString = builder.Configuration.GetConnectionString("account");
    e.DbContextPooling = true;
});

//Loan Start
AppHouse.Loans.Application.StartupServices.AddLoansStartup(builder.Services);
builder.AddNpgsqlDbContext<LoanContext>("dbPostgres", e =>
{
    e.ConnectionString = builder.Configuration.GetConnectionString("account");
    e.DbContextPooling = true;
});

//Cache Start
builder.Services.AddMemoryCache();//TODO change later to redis


//MediatR
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(EventLoggingAndValidationMiddleware<,>));
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblyContaining<AppHouse.Accounts.Application.Init>();
    c.RegisterServicesFromAssemblyContaining<AppHouse.Loans.Application.Init>();

})
    .AddScoped(typeof(IPipelineBehavior<,>), typeof(EventLoggingAndValidationMiddleware<,>));


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
LoanEndpoints.Setup(app);
#endregion

app.Run();

