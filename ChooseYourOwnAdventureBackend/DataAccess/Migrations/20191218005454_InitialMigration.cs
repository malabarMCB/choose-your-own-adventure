using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(maxLength: 256, nullable: true),
                    PositiveAnswerQuestionId = table.Column<int>(nullable: true),
                    NegativeAnswerQuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Questions_NegativeAnswerQuestionId",
                        column: x => x.NegativeAnswerQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Questions_PositiveAnswerQuestionId",
                        column: x => x.PositiveAnswerQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            if (migrationBuilder.IsSqlServer())
            {
                var getQuestionsTreeProc = @"create proc GetQuestionsTree
	                    @FirstQuestionId int
                    as
                    with recursiveQuestions (Id, [Text], [PositiveAnswerQuestionId], [NegativeAnswerQuestionId])
                    as
                    (
	                    select * from Questions where Id = @FirstQuestionId
	                    union all
	                    select q.* from Questions q
	                    join recursiveQuestions r on r.PositiveAnswerQuestionId = q.id
	                    where r.PositiveAnswerQuestionId is not null
	                    union all
	                    select q.* from Questions q
	                    join recursiveQuestions r on r.NegativeAnswerQuestionId = q.id
	                    where r.NegativeAnswerQuestionId is not null
                    )
                    select * from recursiveQuestions";

                migrationBuilder.Sql(getQuestionsTreeProc);
            }

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "NegativeAnswerQuestionId", "PositiveAnswerQuestionId", "Text" },
                values: new object[,]
                {
                    { 3, null, null, "Maybe you want an apple?" },
                    { 7, null, null, "Do jumping jacks first." },
                    { 8, null, null, "What are you waiting for? Grab it now." },
                    { 9, null, null, "Wait `till you find a sinful, unforgettable doughnut." },
                    { 10, null, null, "Get it." },
                    { 11, null, null, "Why not to take a cake?" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "NegativeAnswerQuestionId", "PositiveAnswerQuestionId", "Text" },
                values: new object[] { 5, 9, 8, "Is it a good doughnut?" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "NegativeAnswerQuestionId", "PositiveAnswerQuestionId", "Text" },
                values: new object[] { 6, 11, 10, "Are you really sure?" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "NegativeAnswerQuestionId", "PositiveAnswerQuestionId", "Text" },
                values: new object[] { 4, 7, 6, "Are you sure?" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "NegativeAnswerQuestionId", "PositiveAnswerQuestionId", "Text" },
                values: new object[] { 2, 5, 4, "Do I deserve it?" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "NegativeAnswerQuestionId", "PositiveAnswerQuestionId", "Text" },
                values: new object[] { 1, 3, 2, "Do I want a doughnut?" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_NegativeAnswerQuestionId",
                table: "Questions",
                column: "NegativeAnswerQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_PositiveAnswerQuestionId",
                table: "Questions",
                column: "PositiveAnswerQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            if (migrationBuilder.IsSqlServer())
            {
                var dropGetQuestionsTreeProc = "drop proc GetQuestionsTree";
                migrationBuilder.Sql(dropGetQuestionsTreeProc);
            }
        }
    }
}
