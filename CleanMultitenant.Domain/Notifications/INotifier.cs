namespace CleanMultitenant.Domain.Notifications
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(string message);
    }
}
