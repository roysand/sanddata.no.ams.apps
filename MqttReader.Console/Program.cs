// See https://aka.ms/new-console-template for more information

using DataLayer.Infrastructure.Config;
using Infrastructure;
using MqttReader.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var configuration = builder.Configuration
    .AddEnvironmentVariables()
    .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>();

// var config = new Config(builder.Configuration);
// builder.Configuration.Bind(config);
// Add services to the container.
// builder.Services.AddSystemd();
builder.Logging.AddConsole();

builder.Services.AddInfrastructure();
builder.Services.AddHostedService<MqttBrokerConsumer>();

var app = builder.Build();
await app.RunAsync();