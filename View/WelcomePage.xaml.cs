using AnimeRecommender.Services;
using CommunityToolkit.Maui.Alerts;

namespace AnimeRecommender.View;

public partial class WelcomePage : ContentPage
{
    private readonly IAuthenticatorService authenticator = new AuthenticatorService();

    public WelcomePage()
	{
		InitializeComponent();
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
        

        string code = await authenticator.GetAccessCodeAsync();

        string accessToken = await authenticator.ExchangeCodeForTokenAsync(code);

        if(!string.IsNullOrEmpty(accessToken))
        {
            AccessToken.Token = accessToken;
            await Toast.Make("You are now logged in!").Show();

            return;
        }

        await Toast.Make("Authorization failed").Show();
    }

	private void OnSearchAnimePageClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync(nameof(SearchAnimePage));
    }

    private void OnMainPageClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(MainPage));
    }
}