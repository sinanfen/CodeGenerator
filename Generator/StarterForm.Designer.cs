namespace Generator;

public partial class StarterForm : Form
{
    private System.ComponentModel.IContainer components = null;
    private Button btnDtoGenerator;
    private Button btnInterfaceImplementationGenerator;

    private void InitializeComponent()
    {
        btnDtoGenerator = new Button();
        btnInterfaceImplementationGenerator = new Button();
        btnMerge = new Button();
        SuspendLayout();
        // 
        // btnDtoGenerator
        // 
        btnDtoGenerator.Location = new Point(12, 12);
        btnDtoGenerator.Name = "btnDtoGenerator";
        btnDtoGenerator.Size = new Size(200, 45);
        btnDtoGenerator.TabIndex = 0;
        btnDtoGenerator.Text = "DTO Generator";
        btnDtoGenerator.UseVisualStyleBackColor = true;
        btnDtoGenerator.Click += btnDtoGenerator_Click;
        // 
        // btnInterfaceImplementationGenerator
        // 
        btnInterfaceImplementationGenerator.Location = new Point(12, 63);
        btnInterfaceImplementationGenerator.Name = "btnInterfaceImplementationGenerator";
        btnInterfaceImplementationGenerator.Size = new Size(200, 45);
        btnInterfaceImplementationGenerator.TabIndex = 1;
        btnInterfaceImplementationGenerator.Text = "Interface/Implementation Generator";
        btnInterfaceImplementationGenerator.UseVisualStyleBackColor = true;
        btnInterfaceImplementationGenerator.Click += btnInterfaceImplementationGenerator_Click;
        // 
        // btnMerge
        // 
        btnMerge.Location = new Point(218, 12);
        btnMerge.Name = "btnMerge";
        btnMerge.Size = new Size(200, 96);
        btnMerge.TabIndex = 2;
        btnMerge.Text = "Merge";
        btnMerge.UseVisualStyleBackColor = true;
        btnMerge.Click += btnMerge_Click;
        // 
        // StarterForm
        // 
        ClientSize = new Size(431, 119);
        Controls.Add(btnMerge);
        Controls.Add(btnDtoGenerator);
        Controls.Add(btnInterfaceImplementationGenerator);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "StarterForm";
        Text = "Main Menu";
        ResumeLayout(false);
    }
    private Button btnMerge;
}

