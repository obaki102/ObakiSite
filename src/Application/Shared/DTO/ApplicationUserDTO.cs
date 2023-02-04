
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Domain.Enums;

namespace ObakiSite.Application.Shared.DTO
{
    public class ApplicationUserDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Provider Provider { get; set; }
        public Role UserRole { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;

        public static implicit operator ApplicationUser(ApplicationUserDTO userDTO)
        {
            return new ApplicationUser
                (
                 userDTO.Id,
                 userDTO.Email,
                 userDTO.IsActive,
                 userDTO.Provider,
                 userDTO.UserRole,
                 userDTO.DisplayName,
                 userDTO.ProfilePictureUrl
                );
        }

        public static explicit operator ApplicationUserDTO(ApplicationUser user)
        {
            return new ApplicationUserDTO
            {
                Id = user.Id,
                UserRole = user.UserRole,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Provider = user.Provider,
                IsActive = user.IsActive,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
        }
    }
}
