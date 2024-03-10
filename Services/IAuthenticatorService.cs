using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommender.Services
{
    interface IAuthenticatorService
    {
        Task<string> GetAccessCodeAsync();

        Task<string> ExchangeCodeForTokenAsync(string code);

        Task<string> ExtractAccessTokenFromResponse(string response);

        Task SaveAccessTokenAsync(string accessToken);
    }
}
