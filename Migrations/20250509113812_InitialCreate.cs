using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinavYonetimUygulamasi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "course_id",
                table: "QuestionBank",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "topic_id",
                table: "QuestionBank",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_course_id",
                table: "QuestionBank",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_topic_id",
                table: "QuestionBank",
                column: "topic_id");

            migrationBuilder.AddForeignKey(
                name: "fk_question_bank_course",
                table: "QuestionBank",
                column: "course_id",
                principalTable: "Courses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_question_bank_topic",
                table: "QuestionBank",
                column: "topic_id",
                principalTable: "Topics",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_question_bank_course",
                table: "QuestionBank");

            migrationBuilder.DropForeignKey(
                name: "fk_question_bank_topic",
                table: "QuestionBank");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBank_course_id",
                table: "QuestionBank");

            migrationBuilder.DropIndex(
                name: "IX_QuestionBank_topic_id",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "topic_id",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");
        }
    }
}
