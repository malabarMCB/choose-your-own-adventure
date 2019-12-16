﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(QuestionsDbContext))]
    [Migration("20191216181249_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.Entities.QuestionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NegativeAnswerQuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("PositiveAnswerQuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NegativeAnswerQuestionId");

                    b.HasIndex("PositiveAnswerQuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("DataAccess.Entities.QuestionEntity", b =>
                {
                    b.HasOne("DataAccess.Entities.QuestionEntity", "NegativeAnswerQuestion")
                        .WithMany("InverseNavigationNegativeAnswerQuestion")
                        .HasForeignKey("NegativeAnswerQuestionId");

                    b.HasOne("DataAccess.Entities.QuestionEntity", "PositiveAnswerQuestion")
                        .WithMany("InverseNavigationPositiveAnswerQuestion")
                        .HasForeignKey("PositiveAnswerQuestionId");
                });
#pragma warning restore 612, 618
        }
    }
}
