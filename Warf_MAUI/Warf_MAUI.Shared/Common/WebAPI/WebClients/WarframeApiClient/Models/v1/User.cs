using Newtonsoft.Json;

namespace Warf_MAUI.Shared.Common.WebAPI.WebClients.WarframeApiClient.Models.v1
{
    public class UserPayload
    {
        [JsonProperty("user")]
        public User User { get; set; } = new User();
    }

    public class User
    {
        [JsonProperty("jwt_token")]
        public string JwtToken { get; set; } = string.Empty;
        [JsonProperty("written_reviews")]
        public int WrittenReviews { get; set; }

        [JsonProperty("avatar")]
        public string? Avatar { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; } = string.Empty;

        [JsonProperty("has_mail")]
        public bool HasMail { get; set; }

        [JsonProperty("background")]
        public string? Background { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("crossplay")]
        public bool Crossplay { get; set; }

        [JsonProperty("verification")]
        public bool Verification { get; set; }

        [JsonProperty("ingame_name")]
        public string InGameName { get; set; } = string.Empty;

        [JsonProperty("banned")]
        public bool Banned { get; set; }

        [JsonProperty("linked_accounts")]
        public LinkedAccounts LinkedAccounts { get; set; } = new LinkedAccounts();

        [JsonProperty("anonymous")]
        public bool Anonymous { get; set; }

        [JsonProperty("check_code")]
        public string CheckCode { get; set; } = string.Empty;

        [JsonProperty("reputation")]
        public int Reputation { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; } = string.Empty;

        [JsonProperty("locale")]
        public string Locale { get; set; } = string.Empty;

        [JsonProperty("unread_messages")]
        public int UnreadMessages { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; } = string.Empty;
    }

    public class LinkedAccounts
    {
        [JsonProperty("steam_profile")]
        public bool SteamProfile { get; set; }

        [JsonProperty("patreon_profile")]
        public bool PatreonProfile { get; set; }

        [JsonProperty("xbox_profile")]
        public bool XboxProfile { get; set; }

        [JsonProperty("discord_profile")]
        public bool DiscordProfile { get; set; }

        [JsonProperty("github_profile")]
        public bool GithubProfile { get; set; }
    }
}
