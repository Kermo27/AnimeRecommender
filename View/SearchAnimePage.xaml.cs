using AnimeRecommender.ViewModels;

namespace AnimeRecommender.View;

public partial class SearchAnimePage : ContentPage
{
	public SearchAnimePage(SearchAnimeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override void OnAppearing()
	{
        base.OnAppearing();
        ((SearchAnimeViewModel)BindingContext).Initialize();
    }
}