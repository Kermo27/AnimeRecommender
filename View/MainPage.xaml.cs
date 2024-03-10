using AnimeRecommender.Models;
using AnimeRecommender.ViewModels;

namespace AnimeRecommender.View;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((MainViewModel)BindingContext).Initialize();
    }
}