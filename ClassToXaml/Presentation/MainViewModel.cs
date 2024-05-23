using System.Text;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Windows.ApplicationModel.DataTransfer;

namespace ClassToXaml.Presentation;

public partial class MainViewModel : ObservableObject
{
    private INavigator _navigator;


    [ObservableProperty]
    private string? name;

    public MainViewModel(INavigator navigator)
    {
        _navigator = navigator;
        NoOfColumn = 2;
        IsUseGrid = true;
        IsUseTextBlock = false;
        IsUseTwoWayBinding = true;
        IsUseXBind = false;
        forUWP = true;
    }
    [ObservableProperty]
    private string? inputText;
    [ObservableProperty]
    private bool isUseGrid;
    [ObservableProperty]
    private bool isUseTextBlock;
    [ObservableProperty]
    private bool isUseTwoWayBinding;
    [ObservableProperty]
    private bool isUseXBind;
    [ObservableProperty]
    private bool forUWP;
    [ObservableProperty]
    private bool forWPF;
    [ObservableProperty]
    private bool forMAUI;
    [ObservableProperty]
    private int noOfColumn;
    [ObservableProperty]
    private string margin;
    [ObservableProperty]
    private string? outputText;
    [ObservableProperty]
    private string? styleForTextBox;
    [ObservableProperty]
    private string? styleForTextBlock;
    [ObservableProperty]
    private string? styleForCheckBox;
    [ObservableProperty]
    private string? styleForDateTime;
    [ObservableProperty]
    private string? styleForCombo;
    public string placeHolder => @"public class Name
{
    public string maiden { get; set; }
    public string suffix { get; set; }
    public string givenName { get; set; }
    public string middleName { get; set; }
    public string surname { get; set; }

}";
    public RelayCommand GenerateCommand => new RelayCommand(OnGenerateCommandExecuted);
    public RelayCommand CopyCommand => new RelayCommand(OnCopyCommandExecuted);
    public RelayCommand ClearCommand => new RelayCommand(OnClearCommandExecuted);
    static IEnumerable<Property> GetPublicProperty(string code)
    {
        try
        {
            List<Property> properties = new List<Property>();
            // Parse the code into a syntax tree
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            // Find the first member declaration
            MemberDeclarationSyntax member = root.Members.FirstOrDefault();
            if (member is ClassDeclarationSyntax cl)
            {
                foreach (var classMember in cl.Members)
                {
                    if (classMember is PropertyDeclarationSyntax propDecl)
                    {
                        // Check for public access modifier
                        bool isPublic = propDecl.Modifiers.Any(SyntaxKind.PublicKeyword);
                        if (isPublic)
                        {
                            // Check if it has get and set accessors
                            bool hasGetAccessor = propDecl.AccessorList.Accessors.Any(a => a.Kind() == SyntaxKind.GetAccessorDeclaration);
                            bool hasSetAccessor = propDecl.AccessorList.Accessors.Any(a => a.Kind() == SyntaxKind.SetAccessorDeclaration);
                            if (hasGetAccessor && hasSetAccessor)
                            {
                                // Return true and the type of the property
                                string propertyType = propDecl.Type.ToString();
                                string propertyName = propDecl.Identifier.Text;
                                properties.Add(new Property(propertyName, propertyType));
                            }
                        }
                        //Console.WriteLine($"Method: {methodDecl.Identifier}");
                    }
                }
            }
            return properties;
        }
        catch (Exception)
        {
            // If there's an exception in parsing, the code is not a valid property
            return null;
        }
    }
    private void OnGenerateCommandExecuted()
    {
        GenerateXamlForClass();
    }
    private void OnCopyCommandExecuted()
    {
        if (string.IsNullOrWhiteSpace(OutputText))
        {
            return;
        }
        var dataPackage = new DataPackage();
        dataPackage.SetText(OutputText);
        Clipboard.SetContent(dataPackage);
    }
    private void OnClearCommandExecuted()
    {
        InputText = string.Empty;
        OutputText = string.Empty;
    }
    public void GenerateXamlForClass()
    {
        OutputText = GenerateXamlForClass(InputText, NoOfColumn);
    }
    private static string ExtractClassName(string classText)
    {
        // Regular expression to extract class name
        string classNamePattern = @"public\s+class\s+(\w+)";
        var classNameMatch = Regex.Match(classText, classNamePattern);

        if (classNameMatch.Success)
        {
            return classNameMatch.Groups[1].Value;
        }

        return string.Empty;
    }
    private static List<string> ExtractClassDefinitions(string fileContent)
    {
        List<string> classDefinitions = new List<string>();
        string classPattern = @"public\s+class\s+\w+\s*{";
        MatchCollection matches = Regex.Matches(fileContent, classPattern);
        foreach (Match match in matches)
        {
            int startIndex = match.Index;
            int openBraces = 0;
            int endIndex = startIndex;
            for (int i = startIndex; i < fileContent.Length; i++)
            {
                if (fileContent[i] == '{')
                {
                    openBraces++;
                }
                else if (fileContent[i] == '}')
                {
                    openBraces--;
                    if (openBraces == 0)
                    {
                        endIndex = i;
                        break;
                    }
                }
            }

            string classDefinition = fileContent.Substring(startIndex, endIndex - startIndex + 1);
            classDefinitions.Add(classDefinition);
        }

        return classDefinitions;
    }
    public string GenerateXamlForClass(string classText, int columnCount)
    {
        if (classText == null)
        {
            return string.Empty;
        }
        StringBuilder xamlText = new();
        string classPattern = @"public\s+class\s+\w+\s*{";
        var classMatches = Regex.Matches(classText, classPattern);
        if (classMatches.Count > 1)
        {
            List<string> classDefinitions = ExtractClassDefinitions(classText);
            foreach (string text in classDefinitions)
            {
                xamlText.AppendLine($"  <!--///////////////XAML for class {ExtractClassName(text)}/////////////-->");
                xamlText.AppendLine(GenerateXaml(text, columnCount));
            }
        }
        else if (classMatches.Count == 1)
        {
            return GenerateXaml(classText, columnCount);
        }
        return xamlText.ToString();
    }

    private string GenerateXaml(string classText, int columnCount)
    {
        var properties = GetPublicProperty(classText);
        if (properties == null || !properties.Any())
        {
            return string.Empty;
        }
        StringBuilder xamlBuilder = new();
        xamlBuilder.AppendLine(IsUseGrid ? "<Grid>" : $"<{GetControl("StackPanel")}>");
        int row = 0;
        foreach (var property in properties)
        {
            GenerateControlText(columnCount, xamlBuilder, row, property);
            row++;
        }
        if (IsUseGrid)
        {
            xamlBuilder.AppendLine("    <Grid.RowDefinitions>");
            // Set up grid rows
            for (int i = 0; i < row / columnCount + 1; i++)
            {
                xamlBuilder.AppendLine($"        <RowDefinition Height=\"Auto\"/>");
            }
            xamlBuilder.AppendLine("    </Grid.RowDefinitions>");
            // Set up grid columns
            xamlBuilder.AppendLine("    <Grid.ColumnDefinitions>");
            for (int i = 0; i < columnCount; i++)
            {
                xamlBuilder.AppendLine("        <ColumnDefinition Width=\"*\"/>");
            }
            xamlBuilder.AppendLine("    </Grid.ColumnDefinitions>");
        }
        xamlBuilder.AppendLine(IsUseGrid ? "</Grid>" : $"</{GetControl("StackPanel")}>");
        return xamlBuilder.ToString();
    }

    private string GetMode()
    {
        if (IsUseTwoWayBinding)
        {
            return "Mode=TwoWay";
        }
        return "Mode=OneWay";
    }
    private string GetBinding()
    {
        if (IsUseXBind)
        {
            return "x:Bind";
        }
        return "Binding";
    }
    private string GetRowAndColumn(int row, int columnCount)
    {
        if (IsUseGrid)
        {
            return $"Grid.Column=\"{row % columnCount}\" Grid.Row=\"{row / columnCount}\"";
        }
        return string.Empty;
    }
    private string GetPlaceholderText(string property)
    {
        if (ForUWP)
        {
            return $"PlaceholderText=\"{property}\"";
        }
        else if (ForMAUI)
        {
            return $"Placeholder=\"{property}\"";
        }
        else
        {
            return string.Empty;
        }
    }
    private string GetStyle(string style)
    {
        if (string.IsNullOrWhiteSpace(style))
        {
            return string.Empty;
        }
        return $"Style=\"{{StaticResource {style}}}\"";

    }
    private string AddMargin()
    {
        if (string.IsNullOrWhiteSpace(Margin))
        {
            return string.Empty;
        }
        return $"Margin=\"{Margin}\"";
    }
    private Dictionary<string, string> typeMap = new(StringComparer.OrdinalIgnoreCase)
       {
           { "TextBlock", "Label" },
           { "TextBox", "Entry" },
           { "ComboBox", "Picker" },
           { "StackPanel", "StackLayout" }
       };
    private string GetControl(string type)
    {
        if (ForMAUI && typeMap.ContainsKey(type))
        {
            return typeMap[type];
        }
        return type;

    }
    private void GenerateControlText(int columnCount, StringBuilder xamlBuilder, int row, Property property)
    {
        switch (property.Type.ToLower())
        {
            case "string":
            case "int":
            case "double":
            case "float":
            case "decimal":
                if (IsUseTextBlock)
                    xamlBuilder.AppendLine($"    <{GetControl("TextBlock")} Text=\"{{{GetBinding()} {property.Name}}}\"  Grid.Column=\"{row % columnCount}\" Grid.Row=\"{row / columnCount}\" {GetStyle(StyleForTextBlock)} {AddMargin()} />");
                else
                    xamlBuilder.AppendLine($"    <{GetControl("TextBox")} {GetPlaceholderText(property.Name)}  Text=\"{{{GetBinding()} {property.Name}, {GetMode()}}}\" {GetRowAndColumn(row, columnCount)}  {GetStyle(StyleForTextBox)} {AddMargin()} />");
                break;
            case "datetime":
            case "datetimeoffset":
                xamlBuilder.AppendLine($"    <DatePicker SelectedDate=\"{{{GetBinding()} {property.Name}, {GetMode()}}}\" {GetRowAndColumn(row, columnCount)} {GetStyle(StyleForDateTime)} {AddMargin()} />");
                break;
            case "bool":
                xamlBuilder.AppendLine($"    <CheckBox  Content=\"{property.Name}\" IsChecked=\"{{{GetBinding()} {property.Name},  {GetMode()} }}\" {GetRowAndColumn(row, columnCount)} {GetStyle(StyleForCheckBox)} {AddMargin()} />");
                break;
            case "guid":
                xamlBuilder.AppendLine($"    <{GetControl("ComboBox")} {GetPlaceholderText(property.Name)} SelectedValue=\"{{{GetBinding()} {property.Name}, {GetMode()} }}\" {GetRowAndColumn(row, columnCount)} {GetStyle(StyleForCombo)} {AddMargin()} />");
                break;
            default:
                // For other types, just display the type name
                xamlBuilder.AppendLine($"    <{GetControl("TextBlock")} Text=\"{{{GetBinding()} {property.Name}}}\"  Grid.Column=\"{row % columnCount}\" Grid.Row=\"{row / columnCount}\"  {GetStyle(StyleForTextBlock)} {AddMargin()} />");
                break;
        }
    }

    public ICommand GoToSecond => new AsyncRelayCommand(async () => await _navigator.NavigateViewModelAsync<SecondViewModel>(this));
    public ICommand ExampleCommand => new RelayCommand(() => InputText = placeHolder);

}
