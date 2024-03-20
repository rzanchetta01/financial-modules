using AppHouse.Loans.Core;
using AppHouse.Loans.WebApi.Middleware;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddServiceDefaults();
//Mongo Log Start
builder.AddMongoDBClient("dbMongo");

//Loan Start
AppHouse.Loans.Application.StartupServices.AddLoansStartup(builder.Services);
builder.AddNpgsqlDbContext<LoanContext>("dbPostgres", e =>
{
    e.ConnectionString = builder.Configuration.GetConnectionString("psql");
    e.DbContextPooling = true;
#if DEBUG
    e.Metrics = true;
    e.Tracing = true;
    e.HealthChecks = true;
    e.DbContextPooling = false;
#endif
}, db =>
{
#if DEBUG
    db.EnableDetailedErrors();
    db.EnableSensitiveDataLogging();
#endif
});

//MediatR
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblyContaining<AppHouse.Loans.Application.Init>();

    //pipeline middlewares
    c.AddOpenBehavior(typeof(EventLoggingAndValidationMiddleware<,>));
});

//Cache Start
builder.Services.AddMemoryCache();//TODO change later to Redis


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<GlobalErroHandlingMIddleware>();

var app = builder.Build();

app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseMiddleware<GlobalErroHandlingMIddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
