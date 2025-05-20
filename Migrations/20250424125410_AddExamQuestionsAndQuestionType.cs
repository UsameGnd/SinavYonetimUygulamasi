using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinavYonetimUygulamasi.Migrations
{
    /// <inheritdoc />
    public partial class AddExamQuestionsAndQuestionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "question_type",
                table: "QuestionBank",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "ExamQuestions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    question_order = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ExamQuestions__3213E83F21D8E48F", x => x.id);
                    table.ForeignKey(
                        name: "fk_exam_questions_exam",
                        column: x => x.exam_id,
                        principalTable: "Exams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exam_questions_question",
                        column: x => x.question_id,
                        principalTable: "QuestionBank",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_exam_id",
                table: "ExamQuestions",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_question_id",
                table: "ExamQuestions",
                column: "question_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamQuestions");

            migrationBuilder.DropColumn(
                name: "question_type",
                table: "QuestionBank");
        }
    }
}
