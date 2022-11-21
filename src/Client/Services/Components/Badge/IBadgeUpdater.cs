namespace ObakiSite.Client.Services.Components.Badge
{
    public interface IBadgeUpdater
    {
        public event EventHandler<int>? BadgeChangedHandler;
        public  void IncrementBadge();
        public void DecrementBadge();
        public void ResetBadge();

    }
}
