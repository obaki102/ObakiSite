using ObakiSite.Shared.DTO;

namespace ObakiSite.Client.Services.Settings
{
    public interface IMySettings
    {
       Task GetMyDefaultSettings();
        Preferences Data { get; }
    }
}
