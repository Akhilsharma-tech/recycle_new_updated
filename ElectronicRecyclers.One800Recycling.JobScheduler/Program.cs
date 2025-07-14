using ElectronicRecyclers.One800Recycling.JobScheduler;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
