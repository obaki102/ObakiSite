
using ObakiSite.Application.Domain.Enums;
using ObakiSite.Application.Domain.Primitives;

namespace ObakiSite.Application.Domain.Entities
{
    public class ApplicationUser : Entity
    {
        public ApplicationUser(Guid id, string email, bool isActive, Provider provider, Role userRole,
            string displayName, string profilePictureUrl) : base(id)
        {

            Email = email;
            IsActive = isActive;
            Provider = provider;
            UserRole = userRole;
            DisplayName = displayName;
            ProfilePictureUrl = profilePictureUrl;
        }

        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Provider Provider { get; set; }
        public Role UserRole { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;

    }
}
