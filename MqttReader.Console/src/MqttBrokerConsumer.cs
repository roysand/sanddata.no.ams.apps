using DataLayer.Application.Interface;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Interface;
using Microsoft.Extensions.Hosting;
using Application.Interface;

namespace MqttReader.Console;

public class MqttBrokerConsumer : BackgroundService
{
    private readonly IMqttManagedClient _mqttManagedClient;
    private readonly IDetailRepository<Detail> _detailRepository;
    private readonly IConfig _config;

    public MqttBrokerConsumer(IMqttManagedClient mqttManagedClient
        ,IDetailRepository<Detail> detailRepository
        ,IConfig config)
    {
        _mqttManagedClient = mqttManagedClient;
        _detailRepository = detailRepository;
        _config = config;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _mqttManagedClient.SubscribeAsync(_config.MqttConfig.MQTTTopic());
    }
}