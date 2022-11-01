namespace ObakiSite.Client.Services.Components.Badge
{
    public class BadgeUpdater : IBadgeUpdater
    {
        private int numOfBadges { get; set; } = 0;

        public event EventHandler<int>? BadgeChangedHandler;

        public void IncrementBadge()
        {
            numOfBadges++;
            BadgeChangedHandler?.Invoke(this, numOfBadges);
        }

        public void DecrementBadge()
        {
            numOfBadges--;
            BadgeChangedHandler?.Invoke(this, numOfBadges);
        }

        public void ResetBadge()
        {
            numOfBadges = 0;
            BadgeChangedHandler?.Invoke(this, numOfBadges);
        }
    }
}
