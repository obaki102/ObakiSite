
using ObakiSite.Application.Domain.Entities;

namespace ObakiSite.Application.Shared.DTO
{
    public class ApplicationUserDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Provider { get; set; } = "Google";
        public string ProfilePictureDataUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string Role { get; set; } = "User";

        public static implicit operator ApplicationUser(ApplicationUserDTO userDTO)
        {
            return new ApplicationUser
                (
                 userDTO.Id,
                 userDTO.DisplayName,
                 userDTO.Email
                 userDTO.Provider,
                 userDTO.ProfilePictureDataUrl,
                 userDTO.IsActive,
                 userDTO.RefreshToken,
                 userDTO.AccessToken,
                 userDTO.RefreshTokenExpiryTime,
                 userDTO.Role
                );
        }

        public static explicit operator ApplicationUserDTO(ApplicationUser user)
        {
            return new ApplicationUserDTO
            {
                Id = user.Id,
                Role = user.Role,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Provider = user.Provider,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                AccessToken = user.AccessToken,
                IsActive = user.IsActive,
                RefreshToken = user.RefreshToken,
                ProfilePictureDataUrl = user.ProfilePictureDataUrl,
            };
        }
    }
}
