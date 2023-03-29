using Microsoft.Extensions.Options;

namespace OptionsExample;

// 2nd way to to this
public class QueueOptionsSetup : IConfigureOptions<QueueOptions>
{
    private readonly IConfiguration _configuration;

    public QueueOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(QueueOptions options)
    {
        _configuration.GetSection(nameof(QueueOptions))
            // Doesn't enable change tracking for Snapshot & Monitoring interfaces
            .Bind(options);
    }
}