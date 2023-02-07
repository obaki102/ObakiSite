using System.ComponentModel.DataAnnotations;

namespace ObakiSite.Application.Shared.Settings
{
    public sealed class GoogleAuth2Config
    {
        [Required]
        public required string AccessToken { get; set; }

        [Required]
        public required string ClientId { get; set; }

        [Required]
        public required string Scope { get; set; }

        [Required]
        public required string DiscoveryDocs { get; set; }
    }
}
