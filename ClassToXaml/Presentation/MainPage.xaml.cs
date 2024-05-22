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
}
