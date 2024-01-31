using AppHouseApi.Endpoints;

var builder = WebApplication.CreateSlimBuilder(args);

#region Services DI
builder.AddServiceDefaults();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<AppHouse.SharedKernel.Init>());
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
app.UseHttpsRedirection();

#region Controllers
AccountEndpoints.Setup(app);
#endregion
app.MapDefaultEndpoints();
app.Run();

