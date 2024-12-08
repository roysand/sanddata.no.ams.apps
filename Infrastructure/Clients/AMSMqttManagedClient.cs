using Application.Interface;
using DataLayer.Application.Interface;
using DataLayer.Domain.Common.Enum;
using DataLayer.Domain.Entities;
using DataLayer.Domain.Models.AmsLeser;
using DataLayer.Infrastructure.Clients;
using DataLayer.Infrastructure.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Clients;

internal class AMSMqttManagedClient : MqttManagedClient, IMqttManagedClient
{
    private readonly IConfig _config;
    private readonly IDetailRepository<Detail> _detailRepository;
    private readonly ILogger<AMSMqttManagedClient> _logger;
    private readonly IConfiguration _configuration;
    private int _counter = 0;

    public AMSMqttManagedClient(IConfig config
        , IDetailRepository<Detail> detailRepository, ILogger<AMSMqttManagedClient> logger
        , IConfiguration configuration) : base(config, logger, configuration)
    {
        _config = config;
        _detailRepository = detailRepository;
        _logger = logger;
        _configuration = configuration;
        _counter = 0;
    }

    public override Task Save(AMSReaderData data)
    {
        if (data.Data == null)
        {
            return Task.CompletedTask;
        }

        var location = _config.ApplicationSettingsConfig.Location();
        _counter++;

        var detail = new Detail()
        {
            MeasurementId = Guid.NewGuid(),
            TimeStamp = data.TimeStamp,
            ObisCode = "1-0:1.7.0.255",
            Name = "Active power",
            ValueStr = "Active power",
            ObisCodeId = ObisCodeId.PowerUsed,
            Unit = "kW",
            Location = _config.ApplicationSettingsConfig.Location(),
            ValueNum = (decimal)data.Data.P / 1000
        };

        Console.WriteLine(
            $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.zzz")}:{JsonConvert.SerializeObject(detail)}");

        // _detailRepository.Add(detail);

        if (_counter > _config.MqttConfig.MQTTDelayCountBeforeSaveToDb())
        {
            _counter = 0;
            // _detailRepository.SaveChangesAsync(new CancellationToken());
        }
        return Task.CompletedTask;
    }
}