namespace ClassToXaml.Presentation;

public partial record SecondViewModel()
{
}
public partial class PreviewViewModel : ObservableObject
{
    public PreviewViewModel(string parameter)
    {
        Parameter = parameter;
    }

    public string Parameter { get; }
}
