using System.Text;

namespace Generator;

partial class DTOGenerator
{
    private TextBox txtNamespace;
    private TextBox txtEntityName;
    private TextBox txtModuleName;
    private System.ComponentModel.IContainer components = null;
    private TextBox txtPath;
    private TextBox txtProperties;
    private CheckBox chkDto;
    private CheckBox chkAddDto;
    private CheckBox chkUpdateDto;
    private Button btnGenerate;
    private Button btnBrowse;
    private Button btnReset;
    private FolderBrowserDialog folderBrowserDialog;
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
        txtProperties = new TextBox();
        chkDto = new CheckBox();
        chkAddDto = new CheckBox();
        chkUpdateDto = new CheckBox();
        btnGenerate = new Button();
        btnBrowse = new Button();
        btnReset = new Button();
        folderBrowserDialog = new FolderBrowserDialog();
        toolTip = new ToolTip(components);
        txtNamespace = new TextBox();
        txtEntityName = new TextBox();
        txtModuleName = new TextBox();
        SuspendLayout();
        // 
        // txtPath
        // 
        txtPath.ForeColor = Color.Gray;
        txtPath.Location = new Point(12, 12);
        txtPath.Name = "txtPath";
        txtPath.Size = new Size(600, 23);
        txtPath.TabIndex = 0;
        txtPath.Text = "Project Path";
        toolTip.SetToolTip(txtPath, "The path where the project files will be saved.");
        txtPath.Enter += RemovePlaceholder;
        txtPath.Leave += AddPlaceholder;
        // 
        // txtProperties
        // 
        txtProperties.ForeColor = Color.Gray;
        txtProperties.Location = new Point(12, 70);
        txtProperties.Multiline = true;
        txtProperties.Name = "txtProperties";
        txtProperties.Size = new Size(681, 212);
        txtProperties.TabIndex = 5;
        txtProperties.Text = "Entity Properties (one per line)";
        toolTip.SetToolTip(txtProperties, "Enter the entity properties (e.g., public string Name { get; set; }). One per line.");
        txtProperties.Enter += RemovePlaceholder;
        txtProperties.Leave += AddPlaceholder;
        // 
        // chkDto
        // 
        chkDto.AutoSize = true;
        chkDto.Location = new Point(12, 292);
        chkDto.Name = "chkDto";
        chkDto.Size = new Size(45, 19);
        chkDto.TabIndex = 6;
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
        chkAddDto.TabIndex = 7;
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
        chkUpdateDto.TabIndex = 8;
        chkUpdateDto.Text = "UpdateDto";
        toolTip.SetToolTip(chkUpdateDto, "Select this option to generate an UpdateDTO class for update operations.");
        chkUpdateDto.UseVisualStyleBackColor = true;
        // 
        // btnGenerate
        // 
        btnGenerate.Location = new Point(486, 288);
        btnGenerate.Name = "btnGenerate";
        btnGenerate.Size = new Size(207, 23);
        btnGenerate.TabIndex = 10;
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
        // txtNamespace
        // 
        txtNamespace.ForeColor = Color.Gray;
        txtNamespace.Location = new Point(12, 41);
        txtNamespace.Name = "txtNamespace";
        txtNamespace.Size = new Size(179, 23);
        txtNamespace.TabIndex = 2;
        txtNamespace.Text = "Enter Namespace";
        toolTip.SetToolTip(txtNamespace, "Enter the short name of the module (e.g., CRM, HR).");
        txtNamespace.Enter += RemovePlaceholder;
        txtNamespace.Leave += AddPlaceholder;
        // 
        // txtEntityName
        // 
        txtEntityName.ForeColor = Color.Gray;
        txtEntityName.Location = new Point(383, 41);
        txtEntityName.Name = "txtEntityName";
        txtEntityName.Size = new Size(310, 23);
        txtEntityName.TabIndex = 4;
        txtEntityName.Text = "Enter Entity Name";
        toolTip.SetToolTip(txtEntityName, "Enter the short name of the module (e.g., CRM, HR).");
        txtEntityName.Enter += RemovePlaceholder;
        txtEntityName.Leave += AddPlaceholder;
        // 
        // txtModuleName
        // 
        txtModuleName.ForeColor = Color.Gray;
        txtModuleName.Location = new Point(197, 41);
        txtModuleName.Name = "txtModuleName";
        txtModuleName.Size = new Size(180, 23);
        txtModuleName.TabIndex = 3;
        txtModuleName.Text = "Enter Module Short Name";
        toolTip.SetToolTip(txtModuleName, "Enter the short name of the module (e.g., CRM, HR).");
        txtModuleName.Enter += RemovePlaceholder;
        txtModuleName.Leave += AddPlaceholder;
        // 
        // DTOGenerator
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(706, 322);
        Controls.Add(txtNamespace);
        Controls.Add(txtEntityName);
        Controls.Add(txtModuleName);
        Controls.Add(btnReset);
        Controls.Add(btnGenerate);
        Controls.Add(chkUpdateDto);
        Controls.Add(chkAddDto);
        Controls.Add(chkDto);
        Controls.Add(txtProperties);
        Controls.Add(btnBrowse);
        Controls.Add(txtPath);
        Name = "DTOGenerator";
        Text = "DTO Generator";
        ResumeLayout(false);
        PerformLayout();
    }
}
