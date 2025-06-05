namespace Generator
{
    public class DbContextNameEventArgs : EventArgs
    {
        public string DbContextName { get; }

        public DbContextNameEventArgs(string dbContextName)
        {
            DbContextName = dbContextName;
        }
    }
}
