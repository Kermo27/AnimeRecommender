using AnimeRecommender.View;

namespace AnimeRecommender
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}
	}
}
