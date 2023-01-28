
using ObakiSite.Application.Domain.Primitives;

namespace ObakiSite.Application.Domain.Entities
{
    public class ApplicationUser : Entity
    {
        public ApplicationUser(Guid id, string displayname, string provider, string profilePictureDataUrl,
            bool isActive, string refreshToken, string accessToken, DateTime refreshTokenExpiryTime, string role) : base(id)
        {
            DisplayName = displayname;
            Provider = provider;
            ProfilePictureDataUrl = profilePictureDataUrl;
            IsActive = isActive;
            RefreshToken = refreshToken;
            AccessToken = accessToken;
            Role = role;
        }
        public string DisplayName { get; set; }
        public string Provider { get; set; } = "Google";
        public string ProfilePictureDataUrl { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string Role { get; set; } = "User";

    }
}
