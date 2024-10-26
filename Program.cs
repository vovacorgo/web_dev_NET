using SupportCenter;
var builder = WebApplication.CreateBuilder(args);

// Pass the configuration to the Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

builder.Configuration.AddJsonFile("emailsettings.json", optional: true, reloadOnChange: true);
var app = builder.Build();

// Configure the HTTP request pipeline using the Startup class
startup.Configure(app, app.Environment);

app.Run();