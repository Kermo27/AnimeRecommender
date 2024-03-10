using Microsoft.Extensions.Logging;
using AnimeRecommender.View;
using CommunityToolkit.Maui;
using AnimeRecommender.ViewModels;
using AnimeRecommender.Services;
using AniListNet;

namespace AnimeRecommender
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif
			builder.Services.AddSingleton<WelcomePage>();
			builder.Services.AddSingleton<AnimeRecommendationsPage>();

			// Register the view models
			builder.Services.AddSingletonWithShellRoute<SearchAnimePage, SearchAnimeViewModel>(nameof(SearchAnimePage));
			builder.Services.AddSingletonWithShellRoute<MainPage, MainViewModel>(nameof(MainPage));
			builder.Services.AddSingletonWithShellRoute<DetailsPage, DetailsViewModel>(nameof(DetailsPage));

			// Register the services
			builder.Services.AddSingleton<IAniListClient, AniListClient>();
			builder.Services.AddSingleton<IAuthenticatorService, AuthenticatorService>();
			builder.Services.AddSingleton<AniClient>();



			return builder.Build();
		}
	}
}
