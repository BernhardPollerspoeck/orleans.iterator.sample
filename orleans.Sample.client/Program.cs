
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;
using Orleans.Iterator.Abstraction;
using Orleans.Iterator.Abstraction.Abstraction;
using Orleans.Runtime;
using orleansSample.grain;

var builder = Host.CreateDefaultBuilder(args);

const string? CONNECTION = "server=localhost;database=orleansIterator;user=root;password=unsecure1Admin";

var invariant = "MySql.Data.MySqlClient";
var connectionString = CONNECTION;

builder.UseOrleansClient(clientBuilder =>
{

	clientBuilder.UseAdoNetClustering(options =>
	{
		options.Invariant = invariant;
		options.ConnectionString = connectionString;
	})
	.Configure<ClusterOptions>(o =>
	{
		o.ClusterId = "iteartor";
		o.ServiceId = "tester";
	})
	.UseIterator()
	;
});



var host = builder.Build();


await host.StartAsync();

var client = host.Services.GetRequiredService<IClusterClient>();

await SetupStates("ariorsen", 34);
var gg1 = await SetupStates("ieniennn", 34);
await SetupStates("oenyiu", 34);
await SetupStates("ieioinokioeh", 34);
await SetupStates("ak,riorsen", 34);
var gg2 = await SetupStates(",", 34);
await SetupStates("nioenuill", 34);

var factory = host.Services.GetRequiredService<IIteratorFactory>();
var ite = factory.CreateIterator<IDeviceGrain>("Device");

await foreach (var device in ite)
{
	Console.WriteLine(device);
}

var user = client.GetGrain<IUserGrain>("Orleans");

var d1 = await user.GetDevices();
await user.AddDevice(gg1.GetGrainId());
await user.AddDevice(gg2.GetGrainId());

var d2 = await user.GetDevices();

foreach (var item in d2)
{
	var g = client.GetGrain<IDeviceGrain>(item);
	Console.Write(await g.SayHi());
}

await user.HiAll();


await Task.Delay(1000);
await Task.Delay(1000);



async Task<IDeviceGrain> SetupStates(string id, int count)
{
	var dev1 = client!.GetGrain<IDeviceGrain>(id);
	for (var i = 0; i < count; i++)
	{
		await dev1.SayHi();
	}
	return dev1;
}