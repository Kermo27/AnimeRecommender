using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnimeRecommender.Services
{
    public class AuthenticatorService : IAuthenticatorService
    {
        private const string clientId = "17000";
        private readonly string clientSecret = "mKmdmR3bKJPDa1DcKP92iTwk086Om0zlU38FMVE4";
        private const string redirectUri = "myapp://"; // This matches the redirect URI in your AniList API client settings
        private readonly string authUrl = $"https://anilist.co/api/v2/oauth/authorize?client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=code";

        public async Task<string> ExchangeCodeForTokenAsync(string code)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("code", code)
            });

                var response = await client.PostAsync("https://anilist.co/api/v2/oauth/token", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return await ExtractAccessTokenFromResponse(responseString);
                }
                else
                {
                    throw new HttpRequestException($"Failed to exchange code for token. Status code: {response.StatusCode}");
                }
            }
        }

        public async Task<string> ExtractAccessTokenFromResponse(string response)
        {
            using (JsonDocument doc = JsonDocument.Parse(response))
            {
                var root = doc.RootElement;

                if (root.TryGetProperty("access_token", out var tokenElement))
                {
                    string accessToken = tokenElement.GetString();
                    await SaveAccessTokenAsync(accessToken);
                    return accessToken;
                }
                else
                {
                    throw new InvalidOperationException("Access token not found in the response.");
                }
            }
        }

        public async Task<string> GetAccessCodeAsync()
        {
            try
            {
                WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri(authUrl),
                    new Uri("myapp://"));

                string accessToken = authResult?.AccessToken;

                if (authResult.Properties.TryGetValue("code", out var authorizationCode))
                {
                    return authorizationCode;
                }
                else
                {
                    throw new InvalidOperationException("Authorization code not found in the response.");
                }
            }
            catch (TaskCanceledException e)
            {
                // Authenticaion was stopped by the user
                throw new OperationCanceledException("Authentication was canceled by the user", e);
            }
        }

        public async Task SaveAccessTokenAsync(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException("Access token is null or empty.", nameof(accessToken));
            }
            await SecureStorage.SetAsync("access_token", accessToken);
        }
    }
}
