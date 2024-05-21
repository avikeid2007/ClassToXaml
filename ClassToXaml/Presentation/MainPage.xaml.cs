namespace ClassToXaml.Presentation;

public sealed partial class MainPage : Page
{
    public MainViewModel VM { get { return (MainViewModel)DataContext; } }
    public MainPage()
    {
        this.InitializeComponent();
    }
}
