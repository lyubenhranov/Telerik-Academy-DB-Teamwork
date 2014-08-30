namespace MusicFactory.Engine
{
    using MusicFactory.Models;

    class EntryPoint
    {
        static void Main()
        {
            var dbContext = new MusicFactoryDbContext();

            dbContext.Artists.Add(new Artist() { Name = "Pepi" });
            dbContext.SaveChanges();
        }
    }
}