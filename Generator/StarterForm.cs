namespace Generator;

public partial class StarterForm : Form
{
    public StarterForm()
    {
        InitializeComponent();
    }

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

    private void btnMerge_Click(object sender, EventArgs e)
    {
        MergedGenerator mergedGenerator = new MergedGenerator();
        mergedGenerator.Show();
    }
}
