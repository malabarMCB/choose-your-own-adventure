using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Tests.Infrastructure
{
    public static class TestDbContextCreator
    {
        public static QuestionsDbContext CreateInMemory(string dbName)
        {
            var options = new DbContextOptionsBuilder<QuestionsDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new QuestionsDbContext(options);
        }

        public static QuestionsDbContext CreateSqlServer()
        {
            var configuration = ConfigurationProvider.Configuration;
            var options = new DbContextOptionsBuilder<QuestionsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("QuestionsDbConnection"))
                .Options;

            return new QuestionsDbContext(options);
        }
    }
}