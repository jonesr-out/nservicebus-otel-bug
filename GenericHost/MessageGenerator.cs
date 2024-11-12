using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NServiceBus;

class MessageGenerator : BackgroundService
{
    private readonly IMessageSession messageSession;

    public MessageGenerator(IMessageSession messageSession)
    {
        this.messageSession = messageSession;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            var number = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                var sendOptions = new SendOptions();
                sendOptions.DelayDeliveryWith(TimeSpan.FromMilliseconds(100));
                //sendOptions.StartNewTraceOnReceive();
                sendOptions.RouteToThisEndpoint();

                await messageSession.Send(new MyMessage { Number = number++ }, sendOptions, cancellationToken);

                await Task.Delay(1000, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            // graceful shutdown
        }
    }
}