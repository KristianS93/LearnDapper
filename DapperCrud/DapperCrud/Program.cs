using DapperCrud.Endpoints;
using DapperCrud.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(ServiceProvider =>
{
    var configuration = ServiceProvider.GetRequiredService<IConfiguration>();
    var conString = configuration.GetConnectionString("sqlite") ?? throw new ApplicationException("Connection string is null");
    return new SqlConnectionFactory(conString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCustomerEndpoints();

app.Run();