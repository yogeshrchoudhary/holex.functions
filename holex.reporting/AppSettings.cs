namespace holex.reporting;

public class AppSettings
{
    public const string SectionName = nameof(AppSettings);

    public required string ServiceBusConnectionString { get; init; }
    public required string ServiceBusTopicReport { get; init; }
    public required string ServiceBusTopicSubscriptionDaily { get; init; }
}