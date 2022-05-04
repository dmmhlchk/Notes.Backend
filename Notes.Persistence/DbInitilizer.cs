namespace Notes.Persistence
{
    public class DbInitilizer
    {
        public static void Initialize(NotesDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
