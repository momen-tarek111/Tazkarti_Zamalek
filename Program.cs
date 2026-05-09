using WorkerService1;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
		services.AddHostedService<Worker>();
	})
	.Build();

host.Run();

