// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using Microsoft.UI.Xaml.Markup;

namespace ClassToXaml.Presentation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PreviewPage : Page
    {
        public PreviewViewModel VM { get { return (PreviewViewModel)DataContext; } }
        public PreviewPage()
        {
            this.InitializeComponent();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadXamlString();
        }

        private void LoadXamlString()
        {
            if (!string.IsNullOrWhiteSpace(VM.Parameter))
            {
                var xamlContent = (UIElement)XamlReader.Load(VM.Parameter);
                MainContainer.Children.Clear();
                MainContainer.Children.Add(xamlContent);
            }
        }
    }
}
