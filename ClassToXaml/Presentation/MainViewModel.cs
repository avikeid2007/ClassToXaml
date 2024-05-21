using System.Reflection;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassToXaml.Presentation;

public partial class MainViewModel : ObservableObject
{
    private INavigator _navigator;


    [ObservableProperty]
    private string? name;

    public MainViewModel(INavigator navigator)
    {
        _navigator = navigator;
        // Title = "Main";
        GoToSecond = new AsyncRelayCommand(GoToSecondView);
        NoOfColumn = 1;
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
        throw new NotImplementedException();
    }
    private void OnClearCommandExecuted()
    {
        throw new NotImplementedException();
    }
    //public async Task<Type> GenerateDynamicType(string classText)
    //{
    //    try
    //    {
    //        //classText = classText.Replace(" DateTime ", " System.DateTime ");
    //        //classText = classText.Replace(" DateTimeOffset ", " System.DateTimeOffset ");
    //        ////  var usings = new List<string> { "System", "System.Linq", "System.Collections.Generic" };

    //        ////  SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(classText);
    //        //SyntaxTree tree = CSharpSyntaxTree.ParseText(classText);
    //        //CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

    //        //// Find the first member declaration
    //        //MemberDeclarationSyntax member = root.Members.FirstOrDefault();
    //        var item = GetPublicProperty(classText);
    //        return null;
    //        //    List<MetadataReference> references = new List<MetadataReference>();
    //        //    if (OperatingSystem.IsBrowser())
    //        //    {
    //        //        var meta = await GetAssemblyMetadataReference(typeof(object).Assembly);
    //        //        references.Add(meta);
    //        //    }
    //        //    else
    //        //    {
    //        //        var location = typeof(object).Assembly.Location;

    //        //        references.Add(MetadataReference.CreateFromFile(location));
    //        //        references.Add(MetadataReference.CreateFromFile(Assembly.Load("System.Collections.Generic").Location)); ;
    //        //    }
    //        //    CSharpCompilation compilation = CSharpCompilation.Create($"ClassToXaml.Presentation.{Guid.NewGuid()}")
    //        //.WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, concurrentBuild: false))
    //        //.AddReferences(references)
    //        //.AddSyntaxTrees(syntaxTree);
    //        //    // Emit assembly to memory stream
    //        //    using (var ms = new System.IO.MemoryStream())
    //        //    {
    //        //        EmitResult emitResult = compilation.Emit(ms);
    //        //        if (!emitResult.Success)
    //        //        {
    //        //            var failures = emitResult.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);
    //        //            throw new InvalidOperationException($"Failed to generate dynamic type.\n{string.Join('\n', failures.Select(x => x.GetMessage()))}");
    //        //        }
    //        //        Assembly assembly = Assembly.Load(ms.ToArray());
    //        //        return assembly.GetTypes().First();
    //        //    }
    //    }
    //    catch (Exception ex)
    //    {
    //        await _navigator.ShowMessageDialogAsync(this, title: "Fail to convert", content: ex.Message);
    //        return null;
    //    }
    //}
    private async Task<MetadataReference?> GetAssemblyMetadataReference(Assembly assembly)
    {
        MetadataReference? ret = null;
        // var assemblyName = assembly.GetName().Name;
        var assemblyUrl = $"{assembly.GetName().Name}.dll";
        try
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5001/");
            var tmp = await client.GetAsync(assemblyUrl);
            if (tmp.IsSuccessStatusCode)
            {
                var bytes = await tmp.Content.ReadAsByteArrayAsync();
                string filePath = "Avi." + assemblyUrl;
                // Write the byte array to the file
                File.WriteAllBytes(filePath, bytes);
                var path = Path.GetFullPath(filePath);
                ret = MetadataReference.CreateFromFile(path);
            }
        }
        catch (Exception ex)
        {

        }
        return ret;
    }

    public void GenerateXamlForClass()
    {
        OutputText = GenerateXamlForClass(InputText, NoOfColumn);
    }
    public string GenerateXamlForClass(string classText, int columnCount)
    {
        if (classText == null)
        {
            return string.Empty;
        }
        var properties = GetPublicProperty(classText);
        if (properties == null || !properties.Any())
        {
            return string.Empty;
        }
        StringBuilder xamlBuilder = new();
        xamlBuilder.AppendLine(IsUseGrid ? "<Grid>" : "<StackPanel>");
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
        xamlBuilder.AppendLine(IsUseGrid ? "</Grid>" : "</StackPanel>");
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
        return forUWP || ForMAUI ? $"PlaceholderText=\"{property}\"" : string.Empty;
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
                    xamlBuilder.AppendLine($"    <TextBlock Text=\"{{{GetBinding()} {property.Name}}}\"  Grid.Column=\"{row % columnCount}\" Grid.Row=\"{row / columnCount}\" {GetStyle(StyleForTextBlock)} {AddMargin()} />");
                else
                    xamlBuilder.AppendLine($"    <TextBox {GetPlaceholderText(property.Name)}  Text=\"{{{GetBinding()} {property.Name}, {GetMode()}}}\" {GetRowAndColumn(row, columnCount)}  {GetStyle(StyleForTextBox)} {AddMargin()} />");
                break;
            case "datetime":
            case "dateitmeOffset":
                xamlBuilder.AppendLine($"    <DatePicker SelectedDate=\"{{{GetBinding()} {property.Name}, {GetMode()}}}\" {GetRowAndColumn(row, columnCount)} {GetStyle(StyleForDateTime)} {AddMargin()} />");
                break;
            case "bool":
                xamlBuilder.AppendLine($"    <CheckBox  Content=\"{property.Name}\" IsChecked=\"{{{GetBinding()} {property.Name},  {GetMode()} }}\" {GetRowAndColumn(row, columnCount)} {GetStyle(StyleForCheckBox)} {AddMargin()} />");
                break;
            case "guid":
                xamlBuilder.AppendLine($"    <ComboBox {GetPlaceholderText(property.Name)} SelectedValue=\"{{{GetBinding()} {property.Name}, {GetMode()} }}\" {GetRowAndColumn(row, columnCount)} {GetStyle(StyleForCombo)} {AddMargin()} />");
                break;
            default:
                // For other types, just display the type name
                xamlBuilder.AppendLine($"    <TextBlock Text=\"{{{GetBinding()} {property.Name}}}\"  Grid.Column=\"{row % columnCount}\" Grid.Row=\"{row / columnCount}\"  {GetStyle(StyleForTextBlock)} {AddMargin()} />");
                break;
        }
    }

    public ICommand GoToSecond { get; }

    private async Task GoToSecondView()
    {
        await _navigator.NavigateViewModelAsync<SecondViewModel>(this, data: new Entity(Name!));
    }

}
