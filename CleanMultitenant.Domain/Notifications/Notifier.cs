namespace CleanMultitenant.Domain.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public async Task<List<Notification>> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(string message)
        {
            _notifications.Add(new Notification(message));
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
