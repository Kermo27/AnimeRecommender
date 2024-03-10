using AnimeRecommender.Models;
using AnimeRecommender.ViewModels;

namespace AnimeRecommender.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override void OnAppearing()
	{
        base.OnAppearing();
        ((DetailsViewModel)BindingContext).Initialize();
    }
}