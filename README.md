
![Class to XAML](https://github.com/avikeid2007/ClassToXaml/blob/2ead3d760d059fdb09f6065aa55a1cc99b836123/ClassToXaml/Assets/logo.png)
 <br/>
 <a href='//www.microsoft.com/store/apps/9PM6HNH3LNG1?cid=storebadge&ocid=badge'><img src='https://raw.githubusercontent.com/avikeid2007/WinDev-Utility/master/ScreenShots/store.png' alt='English badge' width="150" /></a>
  <br/>
 <a href='http://ClassToXaml.com'>Class to XAML- Web Version (WebAssembly) </a>
# C# Class to XAML Converter

The "C# Class to XAML Converter" is a comprehensive guide and toolkit designed to streamline the process of converting C# classes into XAML (Extensible Application Markup Language) for use in various frameworks, including UWP (Universal Windows Platform), WPF (Windows Presentation Foundation), .NET MAUI (Multi-platform App UI), Xamarin, WinUI, and the Uno Platform. This tool enables developers to effortlessly create and manage the user interface (UI) elements of their applications by bridging the gap between the back-end logic written in C# and the front-end design in XAML.

## Key Features:
1. **Automatic Conversion**: Automatically generate XAML code from C# classes, reducing manual coding and potential errors.
2. **Data Binding**: Simplify the implementation of data binding by providing pre-configured bindings that connect C# properties to XAML UI elements.
3. **Custom Templates**: Utilize and customize templates to ensure the generated XAML meets specific design requirements and coding standards.
4. **User-Friendly Interface**: Intuitive UI that allows developers to configure settings and preview the generated XAML before integration.
5. **Instant Preview**:  Get an instant Preview UI of the generated XAML code.
6. **Support for Complex Data Structures**: Handle complex data structures and nested properties with ease, providing a robust solution for real-world applications.
7. **Extensibility**: Easily extendable to support additional features and custom behaviors specific to your application's needs.

## Benefits:
- **Efficiency**: Speeds up the development process by reducing the time spent on writing and debugging XAML code.
- **Consistency**: Ensures a uniform look and feel across the application by using standardized templates and binding practices.
- **Maintainability**: Enhances maintainability by keeping the UI and business logic separate yet synchronized, facilitating easier updates and modifications.
- **Ease of Use**: Designed for both novice and experienced developers, making it accessible for a wide range of users.

## Use Cases:
- **Rapid Prototyping**: Quickly generate functional UI prototypes based on existing C# classes.
- **Enterprise Applications**: Suitable for large-scale enterprise applications where UI consistency and maintainability are critical.
- **Educational Tools**: Ideal for educational purposes, helping new developers understand the relationship between C# classes and XAML.

The "Class to XAML C# Application" toolkit is an indispensable resource for developers looking to enhance their productivity and create high-quality applications across multiple platforms with ease. Whether you are developing a new application from scratch or maintaining an existing one, this tool provides the functionality and flexibility needed to succeed in modern application development.


### Built With
- [Uno Platform](https://platform.uno/)
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [XAML](https://docs.microsoft.com/en-us/dotnet/desktop-wpf/xaml/overview)

## Screenshot

![Class to Xaml](https://github.com/avikeid2007/ClassToXaml/blob/4a2d5ac30067ef5e3b6d6c2e3fb36c325598d486/Screenshot/Xaml-to-Class.png)
![C# to Xaml](https://github.com/avikeid2007/ClassToXaml/blob/4a2d5ac30067ef5e3b6d6c2e3fb36c325598d486/Screenshot/4-json-to-xaml.png)
![Xaml converter from class](https://github.com/avikeid2007/ClassToXaml/blob/4a2d5ac30067ef5e3b6d6c2e3fb36c325598d486/Screenshot/3-Xaml-convert-from-class.png)
![Xaml preview](https://github.com/avikeid2007/ClassToXaml/blob/4a2d5ac30067ef5e3b6d6c2e3fb36c325598d486/Screenshot/2-Csharp-to-xaml-convertor.png)
![C# class to XAML](https://github.com/avikeid2007/ClassToXaml/blob/4a2d5ac30067ef5e3b6d6c2e3fb36c325598d486/Screenshot/1-Class-to-Xaml.png)
## Usage

1. **Input C# Class**: Enter your C# class in the text box provided.
    ```csharp
    public class Name
   {
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public Guid Id { get; set; }
    public bool IsMarried { get; set; }
   }
    ```
2. **Select Project Type**: Choose the target project type from UWP/Uno-Platform/WinUI, WPF, or Xamarin/Maui.

3. **Configure XAML Settings**:
   - Toggle `Use Grid Layout` to arrange elements in a grid.
   - Enable `Use Textblock instead of Textbox` if you prefer text blocks.
   - Enable `Use Two-way Binding` for binding data back to the class.
   - Optionally use `x:Bind` if required.

4. **Set Grid Layout Options**:
   - Specify the number of columns for the grid.
   - Adjust margin and styles for TextBox, TextBlock, CheckBox, DateTimePicker, and ComboBox.

5. **Generate XAML**: Click on `Generate XAML` to convert the C# class to XAML.

6. **Copy or Preview XAML**: Copy the generated XAML or preview it to integrate into your project.

## Example

Here's an example of the generated XAML for the provided class:
```xml
<Grid >
    <TextBox PlaceholderText="FirstName"  Text="{Binding FirstName, Mode=TwoWay}" Grid.Column="0" Grid.Row="0"    />
    <TextBox PlaceholderText="MiddleName"  Text="{Binding MiddleName, Mode=TwoWay}" Grid.Column="1" Grid.Row="0"    />
    <TextBox PlaceholderText="Surname"  Text="{Binding Surname, Mode=TwoWay}" Grid.Column="0" Grid.Row="1"    />
    <DatePicker SelectedDate="{Binding BirthDate, Mode=TwoWay}" Grid.Column="1" Grid.Row="1"   />
    <ComboBox PlaceholderText="Id" SelectedValue="{Binding Id, Mode=TwoWay }" Grid.Column="0" Grid.Row="2"   />
    <CheckBox  Content="IsMarried" IsChecked="{Binding IsMarried,  Mode=TwoWay }" Grid.Column="1" Grid.Row="2"   />
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>
</Grid>

 ```
## Roadmap

### Planned Features

- **Support for Additional UI Frameworks**: Expand support to include more UI frameworks.
- **Advanced XAML Customization**: Allow more detailed customization of generated XAML.
- **Improved UI**: Enhance the application's user interface for better usability.
- **Json Example Support**: Add support for generating XAML from JSON input.

### Future Enhancements

- **Code Refactoring**: Optimize codebase for better performance and maintainability.
- **Localization**: Add support for multiple languages.
- **Plugin Architecture**: Introduce a plugin system to extend functionality.

### Community Suggestions

We welcome your suggestions! Please open an issue to propose new features or improvements.

Stay tuned for updates as we continue to enhance the C# Class to XAML Converter!

## Contributing

We welcome contributions! To get started:

1. **Fork and Clone**: Fork the repository and clone it locally.
    ```bash
    git clone https://github.com/your-username/ClassToXAML.git
    ```

2. **Create a Branch**: Create a new branch for your changes.
    ```bash
    git checkout -b feature-or-bugfix-name
    ```

3. **Make Changes**: Make your changes, ensuring they adhere to the project's coding standards.

4. **Commit and Push**: Commit your changes and push to your fork.
    ```bash
    git add .
    git commit -m "Description of the feature or fix"
    git push origin feature-or-bugfix-name
    ```

5. **Submit a Pull Request**: Open a pull request against the main repository.

### Guidelines

- Provide a clear description in your pull request.

### Reporting Issues

For bugs or feature requests, please open an issue with details.

Thank you for contributing!
## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
