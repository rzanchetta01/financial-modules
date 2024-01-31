using AppHouse.Accounts.Core;
using AppHouse.BootsStrap.Endpoints;
var builder = WebApplication.CreateBuilder(args);

#region Services DI
AppHouse.Accounts.Application.StartupServices.AddAccountStartup(builder.Services);
builder.AddNpgsqlDbContext<AccountsContext>("dbPostgres");
builder.AddServiceDefaults();
builder.Services.AddMediatR(c => 
{
    c.RegisterServicesFromAssemblyContaining<AppHouse.Accounts.Application.Init>();
});
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
#region Middlewares


#endregion

#region Controllers
AccountEndpoints.Setup(app);
#endregion

app.Run();
