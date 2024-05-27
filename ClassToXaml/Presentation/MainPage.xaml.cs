namespace ClassToXaml.Presentation;

public sealed partial class MainPage : Page
{
    public MainViewModel VM { get { return (MainViewModel)DataContext; } }
    public MainPage()
    {
        this.InitializeComponent();
        Loaded += MainPage_Loaded;
    }

    private void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        DarkModeToggle.IsOn = this.GetThemeService().IsDark;
    }

    private void OnDarkModeToggleToggled(object sender, RoutedEventArgs e)
    {
        var ser = this.GetThemeService();
        if (ser.IsDark)
        {
            ser.SetThemeAsync(AppTheme.Light);
        }
        else
        {
            ser.SetThemeAsync(AppTheme.Dark);
        }
    }

    private async void SymbolIcon_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9PM6HNH3LNG1"));
    }

    private async void Path_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?ProductId=9PM6HNH3LNG1"));
    }

    private async void SymbolIcon_Tapped_1(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        await Windows.System.Launcher.LaunchUriAsync(new Uri("http://classtoxaml.com"));

    }

    private async void Path_Tapped_1(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/avikeid2007/ClassToXaml"));
    }
}
