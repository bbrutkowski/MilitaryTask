namespace MilitaryTask.Model.Auth
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string Scope { get; set; }

        [Newtonsoft.Json.JsonProperty("access_token")]
        public string AccessTokenJson { set => AccessToken = value; }
        [Newtonsoft.Json.JsonProperty("token_type")]
        public string TokenTypeJson { set => TokenType = value; }
        [Newtonsoft.Json.JsonProperty("expires_in")]
        public int ExpiresInJson { set => ExpiresIn = value; }
        [Newtonsoft.Json.JsonProperty("scope")]
        public string ScopeJson { set => Scope = value; }
    }
}
