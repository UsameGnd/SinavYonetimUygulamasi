using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinavYonetimUygulamasi.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionOptionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_path",
                table: "QuestionBank",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    option_text = table.Column<string>(type: "text", nullable: false),
                    is_correct = table.Column<bool>(type: "bit", nullable: false),
                    order_index = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuestionOptions__3213E83F5E39254E", x => x.id);
                    table.ForeignKey(
                        name: "fk_question_option_question",
                        column: x => x.question_id,
                        principalTable: "QuestionBank",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_question_id",
                table: "QuestionOptions",
                column: "question_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropColumn(
                name: "image_path",
                table: "QuestionBank");
        }
    }
}
