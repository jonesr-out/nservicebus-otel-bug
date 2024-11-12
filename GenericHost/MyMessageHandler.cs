using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NServiceBus;

public class MyMessageHandler : IHandleMessages<MyMessage>
{
    private readonly ILogger<MyMessageHandler> logger;

    public MyMessageHandler(ILogger<MyMessageHandler> logger)
    {
        this.logger = logger;
    }

    public async Task Handle(MyMessage message, IMessageHandlerContext context)
    {
        var opid = Activity.Current?.Id;

        logger.LogInformation("Received message with Operation Id #{Id}", opid);

        Debug.Assert(opid != null);

        await Task.CompletedTask;
    }
}
