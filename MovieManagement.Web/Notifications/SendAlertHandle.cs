using MediatR;
using Microsoft.Extensions.Logging;

namespace MovieManagement.Web.Notifications;

public sealed class SendAlertHandle(ILogger<SendAlertHandle> logger) : INotificationHandler<MovieCreatedNotification>
{
    public Task Handle(MovieCreatedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("handling notification for movie creation with id : {id}. send alert.", notification.Id);
        return Task.CompletedTask;
    }
}
