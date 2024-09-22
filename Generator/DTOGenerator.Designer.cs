using System.Text;

namespace Generator;

partial class DTOGenerator
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TextBox txtPath;
    private System.Windows.Forms.TextBox txtEntityName;
    private System.Windows.Forms.TextBox txtModuleName;
    private System.Windows.Forms.TextBox txtProperties;
    private System.Windows.Forms.CheckBox chkDto;
    private System.Windows.Forms.CheckBox chkAddDto;
    private System.Windows.Forms.CheckBox chkUpdateDto;
    private System.Windows.Forms.Button btnGenerate;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.Button btnReset;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    private ToolTip toolTip;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        txtPath = new TextBox();
        txtEntityName = new TextBox();
        txtModuleName = new TextBox();
        txtProperties = new TextBox();
        chkDto = new CheckBox();
        chkAddDto = new CheckBox();
        chkUpdateDto = new CheckBox();
        btnGenerate = new Button();
        btnBrowse = new Button();
        btnReset = new Button();
        folderBrowserDialog = new FolderBrowserDialog();
        toolTip = new ToolTip(components);
        SuspendLayout();
        // 
        // txtPath
        // 
        txtPath.Location = new Point(12, 12);
        txtPath.Name = "txtPath";
        txtPath.Size = new Size(600, 23);
        txtPath.TabIndex = 0;
        txtPath.Text = "Project Path";
        toolTip.SetToolTip(txtPath, "The path where the project files will be saved.");
        // 
        // txtEntityName
        // 
        txtEntityName.Location = new Point(12, 50);
        txtEntityName.Name = "txtEntityName";
        txtEntityName.Size = new Size(681, 23);
        txtEntityName.TabIndex = 2;
        txtEntityName.Text = "Entity Name";
        toolTip.SetToolTip(txtEntityName, "Enter the name of the entity (e.g., 'User').");
        // 
        // txtModuleName
        // 
        txtModuleName.Location = new Point(12, 88);
        txtModuleName.Name = "txtModuleName";
        txtModuleName.Size = new Size(681, 23);
        txtModuleName.TabIndex = 3;
        txtModuleName.Text = "Module Short Name";
        toolTip.SetToolTip(txtModuleName, "Enter the short name of the module (e.g., 'SYS').");
        // 
        // txtProperties
        // 
        txtProperties.Location = new Point(12, 126);
        txtProperties.Multiline = true;
        txtProperties.Name = "txtProperties";
        txtProperties.Size = new Size(681, 150);
        txtProperties.TabIndex = 4;
        txtProperties.Text = "Entity Properties (one per line)";
        toolTip.SetToolTip(txtProperties, "Enter the entity properties (e.g., public string Name { get; set; }). One per line.");
        // 
        // chkDto
        // 
        chkDto.AutoSize = true;
        chkDto.Location = new Point(12, 292);
        chkDto.Name = "chkDto";
        chkDto.Size = new Size(45, 19);
        chkDto.TabIndex = 5;
        chkDto.Text = "Dto";
        toolTip.SetToolTip(chkDto, "Select this option to generate a standard DTO class.");
        chkDto.UseVisualStyleBackColor = true;
        // 
        // chkAddDto
        // 
        chkAddDto.AutoSize = true;
        chkAddDto.Location = new Point(63, 292);
        chkAddDto.Name = "chkAddDto";
        chkAddDto.Size = new Size(67, 19);
        chkAddDto.TabIndex = 6;
        chkAddDto.Text = "AddDto";
        toolTip.SetToolTip(chkAddDto, "Select this option to generate an AddDTO class for insert operations.");
        chkAddDto.UseVisualStyleBackColor = true;
        // 
        // chkUpdateDto
        // 
        chkUpdateDto.AutoSize = true;
        chkUpdateDto.Location = new Point(136, 292);
        chkUpdateDto.Name = "chkUpdateDto";
        chkUpdateDto.Size = new Size(83, 19);
        chkUpdateDto.TabIndex = 7;
        chkUpdateDto.Text = "UpdateDto";
        toolTip.SetToolTip(chkUpdateDto, "Select this option to generate an UpdateDTO class for update operations.");
        chkUpdateDto.UseVisualStyleBackColor = true;
        // 
        // btnGenerate
        // 
        btnGenerate.Location = new Point(486, 288);
        btnGenerate.Name = "btnGenerate";
        btnGenerate.Size = new Size(207, 23);
        btnGenerate.TabIndex = 8;
        btnGenerate.Text = "Generate DTOs";
        toolTip.SetToolTip(btnGenerate, "Click to generate the selected DTO files.");
        btnGenerate.UseVisualStyleBackColor = true;
        btnGenerate.Click += btnGenerate_Click;
        // 
        // btnBrowse
        // 
        btnBrowse.Location = new Point(618, 12);
        btnBrowse.Name = "btnBrowse";
        btnBrowse.Size = new Size(75, 23);
        btnBrowse.TabIndex = 1;
        btnBrowse.Text = "Browse";
        btnBrowse.UseVisualStyleBackColor = true;
        btnBrowse.Click += btnBrowse_Click;
        // 
        // btnReset
        // 
        btnReset.Location = new Point(405, 288);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(75, 23);
        btnReset.TabIndex = 9;
        btnReset.Text = "Clear";
        btnReset.UseVisualStyleBackColor = true;
        btnReset.Click += btnReset_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(706, 322);
        Controls.Add(btnReset);
        Controls.Add(btnGenerate);
        Controls.Add(chkUpdateDto);
        Controls.Add(chkAddDto);
        Controls.Add(chkDto);
        Controls.Add(txtProperties);
        Controls.Add(txtModuleName);
        Controls.Add(txtEntityName);
        Controls.Add(btnBrowse);
        Controls.Add(txtPath);
        Name = "Form1";
        Text = "DTO Generator";
        ResumeLayout(false);
        PerformLayout();
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
        sb.AppendLine($"namespace MODISO.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;");
        sb.AppendLine($"public class {entityName}Dto");
        sb.AppendLine("{");
        sb.AppendLine("    public Guid Id { get; set; }");

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
        sb.AppendLine($"namespace MODISO.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;");
        sb.AppendLine($"public class {entityName}AddDto");
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
        sb.AppendLine($"namespace MODISO.DOMAIN.DTOs.{moduleName}.{entityName}Dtos;");
        sb.AppendLine($"public class {entityName}UpdateDto");
        sb.AppendLine("{");
        sb.AppendLine("    public Guid Id { get; set; }");

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
