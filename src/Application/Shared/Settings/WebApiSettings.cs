using System.ComponentModel.DataAnnotations;

namespace ObakiSite.Application.Shared.Settings
{
    public sealed class WebApiSettings
    {
        [Required]
        public required string Type { get; set; }

        [Required]
        public required string ProjectId { get; set; }

        [Required]
        public required string PrivateKeyId { get; set; }

        [Required]
        public required string PrivateKey { get; set; }

        [Required]
        public required string ClientEmail { get; set; }

        [Required]
        public required string ClientId { get; set; }

        [Required]
        public required string AuthUri { get; set; }

        [Required]
        public required string TokenUri { get; set; }

        [Required]
        public required string AuthProvider { get; set; }

        [Required]
        public required string ClientCertUrl { get; set; }

        [Required]
        public required string AnimelistClientId { get; set; }

        [Required]
        public required string AppPassword { get; set; }

        [Required]
        public required string CosmosEndPoint { get; set; }

        [Required]
        public required string CosmosAccessKey { get; set; }

        [Required]
        public required string TokenKey { get; set; }

    }
}
