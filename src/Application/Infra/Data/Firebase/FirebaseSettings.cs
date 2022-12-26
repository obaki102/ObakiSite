using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json.Serialization;


namespace ObakiSite.Application.Infra.Data.Firebase
{
    public class FirebaseSettings
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("project_id")]
        public string ProjectId { get; set; } = string.Empty;
        [JsonPropertyName("private_key_id")]
        public string PrivateKeyId { get; set; } = string.Empty;

        [JsonPropertyName("private_key")]
        public string PrivateKey { get; set; } = string.Empty;
        [JsonPropertyName("client_email")]
        public string ClientEmail { get; set; } = string.Empty;

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; } = string.Empty;

        [JsonPropertyName("auth_uri")]
        public string AuthUri { get; set; } = string.Empty;

        [JsonPropertyName("token_uri")]
        public string TokenUri { get; set; } = string.Empty;

        [JsonPropertyName("auth_provider_x509_cert_url")]
        public string AuthProvider { get; set; } = string.Empty;

        [JsonPropertyName("client_x509_cert_url")]
        public string ClientCertUrl { get; set; } = string.Empty;


        public static FirebaseSettings GetFireBaseSettings()
        {
            var newObject = new FirebaseSettings();
            var porerties = typeof(FirebaseSettings).GetProperties();

            foreach (PropertyInfo property in porerties)
            {
                property.SetValue(newObject, Environment.GetEnvironmentVariable($"Google{property.Name}"));    
            }

            return newObject;
        }

        public static FirebaseSettings GetFireBaseSettings(IConfiguration config)
        {
            if(config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            var newObject = new FirebaseSettings();
            var porerties = typeof(FirebaseSettings).GetProperties();

            foreach (PropertyInfo property in porerties)
            {
                property.SetValue(newObject, config.GetSection(property.Name).Value);
            }

            return newObject;
        }
    }

}
