using Microsoft.Extensions.Options;
using OptionsExample;

var builder = WebApplication.CreateBuilder(args);

// 1st way
// builder.Services.Configure<QueueOptions>(
//     builder.Configuration.GetSection(nameof(QueueOptions)));

// 2nd way
// Doesn't enable change tracking for Snapshot & Monitoring interfaces below
builder.Services.ConfigureOptions<QueueOptionsSetup>();

var app = builder.Build();

app.UseHttpsRedirection();


// Call GET https://localhost:5001/options in Postman

app.MapGet("options", (
    IOptions<QueueOptions> options, // singleton scoped to application run
    IOptionsSnapshot<QueueOptions> optionsSnapshot, // Request Scoped
    IOptionsMonitor<QueueOptions> optionsMonitor // Returns new value anytime it is updated
    ) =>
{
    var response = new
    {
        OptionsValue = options.Value.QueueName,
        OptionsSnapshotValue = optionsSnapshot.Value.QueueName,
        OptionsMonitorValue = optionsMonitor.CurrentValue.QueueName
    };

    return Results.Ok(response);
});

app.Run();