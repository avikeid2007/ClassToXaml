# C# Class to XAML Converter

This application helps you convert C# classes to XAML for use in various UI frameworks including UWP, WinUI, WPF, and Xamarin/Maui. The tool generates XAML code from a given C# class, allowing easy integration of the class properties into XAML-based user interfaces.

## Features

- Convert C# class properties to XAML.
- Support for UWP/Uno-Platform/WinUI, WPF, and Xamarin/Maui.
- Option to use Grid layout for XAML elements.
- Two-way data binding.
- Customizable settings for text blocks, text boxes, and other UI components.

## Screenshot

![C# Class to XAML Converter](path-to-screenshot.png)

## Usage

1. **Input C# Class**: Enter your C# class in the text box provided.
    ```csharp
    public class Name
    {
        public string maiden { get; set; }
        public string suffix { get; set; }
        public string givenName { get; set; }
        public string middleName { get; set; }
        public string surname { get; set; }
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
<Grid>
    <Entry Placeholder="maiden" Text="{Binding maiden, Mode=TwoWay}" Grid.Column="0" Grid.Row="0" />
    <Entry Placeholder="suffix" Text="{Binding suffix, Mode=TwoWay}" Grid.Column="1" Grid.Row="0" />
    <Entry Placeholder="givenName" Text="{Binding givenName, Mode=TwoWay}" Grid.Column="0" Grid.Row="1" />
    <Entry Placeholder="middleName" Text="{Binding middleName, Mode=TwoWay}" Grid.Column="1" Grid.Row="1" />
    <Entry Placeholder="surname" Text="{Binding surname, Mode=TwoWay}" Grid.Column="0" Grid.Row="2" />
    <Grid.RowDefinitions>
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
