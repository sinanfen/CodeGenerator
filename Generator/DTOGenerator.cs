using System.Text;

namespace Generator
{
    public partial class DTOGenerator : Form
    {
        public DTOGenerator()
        {
            InitializeComponent();
            InitializePlaceholders();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializePlaceholders()
        {
            SetPlaceholder(txtPath, "Project Path");
            SetPlaceholder(txtEntityName, "Entity Name");
            SetPlaceholder(txtModuleName, "Module Short Name");
            SetPlaceholder(txtProperties, "Entity Properties (one per line)");
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            // Event handlers for entering and leaving the text box
            textBox.Enter += (sender, e) => RemovePlaceholder(textBox, placeholder);
            textBox.Leave += (sender, e) => RestorePlaceholder(textBox, placeholder);
        }

        private void RemovePlaceholder(TextBox textBox, string placeholder)
        {
            if (textBox.Text == placeholder)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;  // Change text color to normal
            }
        }

        private void RestorePlaceholder(TextBox textBox, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholder;
                textBox.ForeColor = Color.Gray;  // Change text color to gray for placeholder
            }
        }


        // Define the btnBrowse_Click event handler
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // Create a FolderBrowserDialog to allow folder selection
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the selected path in the txtPath TextBox
                txtPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            SetPlaceholder(txtPath, "Project Path");
            SetPlaceholder(txtEntityName, "Entity Name");
            SetPlaceholder(txtModuleName, "Module Short Name");
            SetPlaceholder(txtProperties, "Entity Properties (one per line)");
            // Uncheck all checkboxes
            chkDto.Checked = false;
            chkAddDto.Checked = false;
            chkUpdateDto.Checked = false;
        }


        // Define the btnGenerate_Click event handler
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text;  // Get the path from the form
            string entityName = txtEntityName.Text;  // Entity name
            string moduleName = txtModuleName.Text.ToUpper();  // Module short name
            List<(string Type, string Name)> properties = GetEntityProperties(txtProperties.Text);  // Entity properties

            // Ensure that at least one DTO type is selected
            List<string> selectedOptions = new List<string>();
            if (chkDto.Checked) selectedOptions.Add("Dto");
            if (chkAddDto.Checked) selectedOptions.Add("AddDto");
            if (chkUpdateDto.Checked) selectedOptions.Add("UpdateDto");

            // If no DTOs are selected, show a message and return
            if (selectedOptions.Count == 0)
            {
                MessageBox.Show("Please select at least one DTO type to generate.");
                return;
            }

            // Path to save the DTO files
            path = Path.Combine(path, "DTOs", moduleName, $"{entityName}Dtos");

            // Create the directory if it doesn't exist
            Directory.CreateDirectory(path);

            // Generate the files based on the selected options
            if (selectedOptions.Contains("Dto"))
            {
                string dtoContent = GenerateDto(entityName, moduleName, properties);
                File.WriteAllText(Path.Combine(path, $"{entityName}Dto.cs"), dtoContent);
            }

            if (selectedOptions.Contains("AddDto"))
            {
                string addDtoContent = GenerateAddDto(entityName, moduleName, properties);
                File.WriteAllText(Path.Combine(path, $"{entityName}AddDto.cs"), addDtoContent);
            }

            if (selectedOptions.Contains("UpdateDto"))
            {
                string updateDtoContent = GenerateUpdateDto(entityName, moduleName, properties);
                File.WriteAllText(Path.Combine(path, $"{entityName}UpdateDto.cs"), updateDtoContent);
            }

            MessageBox.Show($"DTO files generated successfully in {path}");
        }

        // Method to parse the entity properties from the multiline textbox
        private List<(string Type, string Name)> GetEntityProperties(string propertiesInput)
        {
            List<(string Type, string Name)> properties = new List<(string Type, string Name)>();
            var lines = propertiesInput.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    // Remove 'public', '{ get; set; }', and any extra whitespace
                    string trimmedLine = line.Trim()
                                             .Replace("public", string.Empty)
                                             .Replace("{ get; set; }", string.Empty)
                                             .Trim();

                    // Split by spaces to get the type and the property name
                    var parts = trimmedLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length == 2)
                    {
                        string type = parts[0];
                        string name = parts[1];
                        properties.Add((type, name));
                    }
                }
            }

            return properties;
        }


        // Method to generate the content for Dto
        private string GenerateDto(string entityName, string moduleName, List<(string Type, string Name)> properties)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"namespace ERP.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;");
            sb.AppendLine($"public sealed class {entityName}Dto : BaseDto");
            sb.AppendLine("{");

            foreach (var prop in properties)
            {
                sb.AppendLine($"    public {prop.Type} {prop.Name} {{ get; set; }}");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        // Method to generate the content for AddDto
        private string GenerateAddDto(string entityName, string moduleName, List<(string Type, string Name)> properties)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"namespace ERP.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;");
            sb.AppendLine($"public sealed class {entityName}AddDto");
            sb.AppendLine("{");

            foreach (var prop in properties)
            {
                if (prop.Name != "Id")
                {
                    sb.AppendLine($"    public {prop.Type} {prop.Name} {{ get; set; }}");
                }
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        // Method to generate the content for UpdateDto
        private string GenerateUpdateDto(string entityName, string moduleName, List<(string Type, string Name)> properties)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"namespace ERP.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;");
            sb.AppendLine($"public sealed class {entityName}UpdateDto : BaseUpdateDto");
            sb.AppendLine("{");

            foreach (var prop in properties)
            {
                if (prop.Name != "Id")
                {
                    sb.AppendLine($"    public {prop.Type} {prop.Name} {{ get; set; }}");
                }
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
