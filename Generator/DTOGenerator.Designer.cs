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
        // DTOGenerator
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
        Name = "DTOGenerator";
        Text = "DTO Generator";
        ResumeLayout(false);
        PerformLayout();
    }
}
