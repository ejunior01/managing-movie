using MediatR;

namespace MovieManagement.Web.Notifications;

public record MovieCreatedNotification(Guid Id) : INotification;
