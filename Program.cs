using SupportCenter;
var builder = WebApplication.CreateBuilder(args);


var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

builder.Configuration.AddJsonFile("emailsettings.json", optional: true, reloadOnChange: true);
var app = builder.Build();


startup.Configure(app, app.Environment);

app.Run();