using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace holex.reporting.functions;

public class DailyReport(ILoggerFactory loggerFactory, 
                            IOptions<AppSettings> appSettings)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<DailyReport>();

    [Function("DailyReport")]
    public void Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);
        _logger.LogInformation($"Service Bus ConnectionString = {appSettings.Value.ServiceBusConnectionString}, " +
                               $"Topic = {appSettings.Value.ServiceBusTopicReport}, " +
                               $"Subscription = {appSettings.Value.ServiceBusTopicSubscriptionDaily}");

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }

        var serviceBusMessage = new
        {
            ExpenseDate = DateTime.Now.ToUniversalTime(),
            Total = RandomExpenseTotal()
        };

        _logger.LogInformation($"Message = {JsonConvert.SerializeObject(serviceBusMessage)}");
    }

    private static decimal RandomExpenseTotal()
    {
        var randomFloat = new Random(DateTime.Now.Millisecond).NextSingle() * 100;
        return (decimal)Math.Round(randomFloat, 2, MidpointRounding.ToZero);
    }
}