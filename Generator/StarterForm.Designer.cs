using Generator;

namespace Generator;

public partial class StarterForm : Form
{
    private System.ComponentModel.IContainer components = null;
    private Button btnDtoGenerator;
    private Button btnInterfaceImplementationGenerator;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void btnDtoGenerator_Click(object sender, EventArgs e)
    {
        DTOGenerator dtoGeneratorForm = new DTOGenerator();
        dtoGeneratorForm.Show();
    }

    private void btnInterfaceImplementationGenerator_Click(object sender, EventArgs e)
    {
        InterfaceImplementationForm interfaceForm = new InterfaceImplementationForm();
        interfaceForm.Show();
    }

    private void InitializeComponent()
    {
        btnDtoGenerator = new Button();
        btnInterfaceImplementationGenerator = new Button();
        SuspendLayout();
        // 
        // btnDtoGenerator
        // 
        btnDtoGenerator.Location = new Point(35, 28);
        btnDtoGenerator.Name = "btnDtoGenerator";
        btnDtoGenerator.Size = new Size(200, 100);
        btnDtoGenerator.TabIndex = 0;
        btnDtoGenerator.Text = "DTO Generator";
        btnDtoGenerator.UseVisualStyleBackColor = true;
        btnDtoGenerator.Click += btnDtoGenerator_Click;
        // 
        // btnInterfaceImplementationGenerator
        // 
        btnInterfaceImplementationGenerator.Location = new Point(289, 28);
        btnInterfaceImplementationGenerator.Name = "btnInterfaceImplementationGenerator";
        btnInterfaceImplementationGenerator.Size = new Size(200, 100);
        btnInterfaceImplementationGenerator.TabIndex = 1;
        btnInterfaceImplementationGenerator.Text = "Interface/Implementation Generator";
        btnInterfaceImplementationGenerator.UseVisualStyleBackColor = true;
        btnInterfaceImplementationGenerator.Click += btnInterfaceImplementationGenerator_Click;
        // 
        // StarterForm
        // 
        ClientSize = new Size(526, 163);
        Controls.Add(btnDtoGenerator);
        Controls.Add(btnInterfaceImplementationGenerator);
        Name = "StarterForm";
        Text = "Main Menu";
        ResumeLayout(false);
    }
}

