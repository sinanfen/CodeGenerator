namespace Generator
{
    public partial class GetDbContextName : Form
    {
        public EventHandler DbContextNameSaved;
        public EventHandler DbContextNameCancelled;

        public GetDbContextName()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DbContextNameCancelled?.Invoke(this, EventArgs.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DbContextNameSaved?.Invoke(this, new DbContextNameEventArgs(txtDbContextName.Text));
        }
    }
}
