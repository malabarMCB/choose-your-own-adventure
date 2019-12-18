using System;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class QuestionsDbContext: DbContext 
    {
        public DbSet<QuestionEntity> Questions { get; set; }

        public QuestionsDbContext(DbContextOptions<QuestionsDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionEntity>()
                .HasOne(x => x.NegativeAnswerQuestion)
                .WithMany(x => x.InverseNavigationNegativeAnswerQuestion)
                .HasForeignKey(x => x.NegativeAnswerQuestionId);

            modelBuilder.Entity<QuestionEntity>()
                .HasOne(x => x.PositiveAnswerQuestion)
                .WithMany(x => x.InverseNavigationPositiveAnswerQuestion)
                .HasForeignKey(x => x.PositiveAnswerQuestionId);

            modelBuilder.Entity<QuestionEntity>().HasData(InitialDataProvider.QuestionEntityInitialData);
        }
    }
}
