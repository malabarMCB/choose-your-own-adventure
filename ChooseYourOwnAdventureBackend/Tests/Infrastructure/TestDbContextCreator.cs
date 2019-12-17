using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure
{
    public static class TestDbContextCreator
    {
        public static QuestionsDbContext Create(string dbName)
        {
            var options = new DbContextOptionsBuilder<QuestionsDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new QuestionsDbContext(options);
        }
    }
}