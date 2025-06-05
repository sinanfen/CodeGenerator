namespace Generator;

public partial class InterfaceImplementationForm : Form
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtPath;
    private TextBox txtModuleName;
    private TextBox txtEntityName;
    private TextBox txtNamespace;
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
        txtModuleName = new TextBox();
        btnGenerate = new Button();
        btnReset = new Button();
        btnBrowse = new Button();
        toolTip = new ToolTip(components);
        txtEntityName = new TextBox();
        txtNamespace = new TextBox();
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
        // btnGenerate
        // 
        btnGenerate.Location = new Point(593, 70);
        btnGenerate.Name = "btnGenerate";
        btnGenerate.Size = new Size(100, 30);
        btnGenerate.TabIndex = 6;
        btnGenerate.Text = "Generate";
        toolTip.SetToolTip(btnGenerate, "Click to generate the interface, class, and AutoMapper profile.");
        btnGenerate.UseVisualStyleBackColor = true;
        btnGenerate.Click += btnGenerate_Click;
        // 
        // btnReset
        // 
        btnReset.Location = new Point(487, 70);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(100, 30);
        btnReset.TabIndex = 5;
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
        btnBrowse.TabIndex = 1;
        btnBrowse.Text = "Browse";
        toolTip.SetToolTip(btnBrowse, "Click to select a folder for the project path.");
        btnBrowse.UseVisualStyleBackColor = true;
        btnBrowse.Click += btnBrowse_Click;
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
        // InterfaceImplementationForm
        // 
        ClientSize = new Size(708, 112);
        Controls.Add(txtNamespace);
        Controls.Add(txtEntityName);
        Controls.Add(txtPath);
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