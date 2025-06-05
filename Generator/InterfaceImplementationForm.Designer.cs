namespace Generator;

public partial class InterfaceImplementationForm : Form
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtPath;
    private TextBox txtEntityName;
    private TextBox txtModuleName;
    private Button btnGenerate;
    private Button btnReset;
    private Button btnBrowse;
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
        btnGenerate = new Button();
        btnReset = new Button();
        btnBrowse = new Button();
        toolTip = new ToolTip(components);
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
        toolTip.SetToolTip(txtPath, "Enter the path where the generated files will be saved.");
        txtPath.Enter += RemovePlaceholder;
        txtPath.Leave += AddPlaceholder;
        // 
        // txtEntityName
        // 
        txtEntityName.ForeColor = Color.Gray;
        txtEntityName.Location = new Point(12, 50);
        txtEntityName.Name = "txtEntityName";
        txtEntityName.Size = new Size(681, 23);
        txtEntityName.TabIndex = 1;
        txtEntityName.Text = "Enter Entity Name";
        toolTip.SetToolTip(txtEntityName, "Enter the name of the Entity class.");
        txtEntityName.Enter += RemovePlaceholder;
        txtEntityName.Leave += AddPlaceholder;
        // 
        // txtModuleName
        // 
        txtModuleName.ForeColor = Color.Gray;
        txtModuleName.Location = new Point(12, 88);
        txtModuleName.Name = "txtModuleName";
        txtModuleName.Size = new Size(681, 23);
        txtModuleName.TabIndex = 2;
        txtModuleName.Text = "Enter Module Short Name";
        toolTip.SetToolTip(txtModuleName, "Enter the short name of the module (e.g., CRM, HR).");
        txtModuleName.Enter += RemovePlaceholder;
        txtModuleName.Leave += AddPlaceholder;
        // 
        // btnGenerate
        // 
        btnGenerate.Location = new Point(593, 126);
        btnGenerate.Name = "btnGenerate";
        btnGenerate.Size = new Size(100, 30);
        btnGenerate.TabIndex = 3;
        btnGenerate.Text = "Generate";
        toolTip.SetToolTip(btnGenerate, "Click to generate the interface, class, and AutoMapper profile.");
        btnGenerate.UseVisualStyleBackColor = true;
        btnGenerate.Click += btnGenerate_Click;
        // 
        // btnReset
        // 
        btnReset.Location = new Point(487, 126);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(100, 30);
        btnReset.TabIndex = 4;
        btnReset.Text = "Reset";
        toolTip.SetToolTip(btnReset, "Click to reset all input fields.");
        btnReset.UseVisualStyleBackColor = true;
        btnReset.Click += btnReset_Click;
        // 
        // btnBrowse
        // 
        btnBrowse.Location = new Point(618, 12);
        btnBrowse.Name = "btnBrowse";
        btnBrowse.Size = new Size(75, 23);
        btnBrowse.TabIndex = 5;
        btnBrowse.Text = "Browse";
        toolTip.SetToolTip(btnBrowse, "Click to select a folder for the project path.");
        btnBrowse.UseVisualStyleBackColor = true;
        btnBrowse.Click += btnBrowse_Click;
        // 
        // InterfaceImplementationForm
        // 
        ClientSize = new Size(708, 179);
        Controls.Add(txtPath);
        Controls.Add(txtEntityName);
        Controls.Add(txtModuleName);
        Controls.Add(btnGenerate);
        Controls.Add(btnReset);
        Controls.Add(btnBrowse);
        Name = "InterfaceImplementationForm";
        Text = "Interface/Implementation Generator";
        Load += InterfaceImplementationForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }
}