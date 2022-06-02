namespace Notes.Persistence
{
    // DbInitilizer - это класс, который отвечает за
    // такие взаимодействие с базой данных, как её создание или удаление
    public class DbInitilizer
    {
        public static void Initialize(NotesDbContext context)
        {
            context.Database.EnsureCreated(); //EnsureCreated - метод, который создаёт базу данных
        }
    }
}
