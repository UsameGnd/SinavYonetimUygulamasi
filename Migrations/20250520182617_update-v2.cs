using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinavYonetimUygulamasi.Migrations
{
    /// <inheritdoc />
    public partial class updatev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_exam_questions_exam",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "fk_exam_questions_question",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "fk_exams_course",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "fk_exams_user",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "fk_question_bank_course",
                table: "QuestionBank");

            migrationBuilder.DropForeignKey(
                name: "fk_question_bank_subtopic",
                table: "QuestionBank");

            migrationBuilder.DropForeignKey(
                name: "fk_question_bank_topic",
                table: "QuestionBank");

            migrationBuilder.DropForeignKey(
                name: "fk_question_bank_user",
                table: "QuestionBank");

            migrationBuilder.DropForeignKey(
                name: "fk_question_option_question",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "fk_subtopics_topic",
                table: "Subtopics");

            migrationBuilder.DropForeignKey(
                name: "fk_topics_course",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Users__3213E83F36AA3206",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "UQ__Users__AB6E6164FC554CB2",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "UQ__Users__F3DBC572324A194F",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Topics__3213E83F2BD14843",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Subtopic__3213E83FFB3DB425",
                table: "Subtopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK__QuestionOptions__3213E83F5E39254E",
                table: "QuestionOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Exams__3213E83F21D8E48D",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_created_by",
                table: "Exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK__ExamQuestions__3213E83F21D8E48F",
                table: "ExamQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Courses__3213E83F8EA4DF79",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "UQ__Courses__72E12F1B18807178",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Question__3213E83F5E39254E",
                table: "QuestionBank");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "difficulty",
                table: "QuestionBank");

            migrationBuilder.RenameTable(
                name: "Subtopics",
                newName: "SubTopics");

            migrationBuilder.RenameTable(
                name: "QuestionBank",
                newName: "QuestionBanks");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Topics",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Topics",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "course_id",
                table: "Topics",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_course_id",
                table: "Topics",
                newName: "IX_Topics_CourseId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "SubTopics",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SubTopics",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "topic_id",
                table: "SubTopics",
                newName: "TopicId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "SubTopics",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Subtopics_topic_id",
                table: "SubTopics",
                newName: "IX_SubTopics_TopicId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "QuestionOptions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "question_id",
                table: "QuestionOptions",
                newName: "QuestionId");

            migrationBuilder.RenameColumn(
                name: "option_text",
                table: "QuestionOptions",
                newName: "OptionText");

            migrationBuilder.RenameColumn(
                name: "is_correct",
                table: "QuestionOptions",
                newName: "IsCorrect");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "QuestionOptions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "order_index",
                table: "QuestionOptions",
                newName: "OptionOrder");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionOptions_question_id",
                table: "QuestionOptions",
                newName: "IX_QuestionOptions_QuestionId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Exams",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Exams",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Exams",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "Exams",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Exams",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "course_id",
                table: "Exams",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_course_id",
                table: "Exams",
                newName: "IX_Exams_CourseId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ExamQuestions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "question_order",
                table: "ExamQuestions",
                newName: "QuestionOrder");

            migrationBuilder.RenameColumn(
                name: "question_id",
                table: "ExamQuestions",
                newName: "QuestionId");

            migrationBuilder.RenameColumn(
                name: "exam_id",
                table: "ExamQuestions",
                newName: "ExamId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ExamQuestions",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_ExamQuestions_question_id",
                table: "ExamQuestions",
                newName: "IX_ExamQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamQuestions_exam_id",
                table: "ExamQuestions",
                newName: "IX_ExamQuestions_ExamId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Courses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Courses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Courses",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "QuestionBanks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "topic_id",
                table: "QuestionBanks",
                newName: "TopicId");

            migrationBuilder.RenameColumn(
                name: "subtopic_id",
                table: "QuestionBanks",
                newName: "SubTopicId");

            migrationBuilder.RenameColumn(
                name: "question_type",
                table: "QuestionBanks",
                newName: "QuestionType");

            migrationBuilder.RenameColumn(
                name: "question_text",
                table: "QuestionBanks",
                newName: "QuestionText");

            migrationBuilder.RenameColumn(
                name: "image_path",
                table: "QuestionBanks",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "QuestionBanks",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "course_id",
                table: "QuestionBanks",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "QuestionBanks",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionBank_topic_id",
                table: "QuestionBanks",
                newName: "IX_QuestionBanks_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionBank_subtopic_id",
                table: "QuestionBanks",
                newName: "IX_QuestionBanks_SubTopicId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionBank_created_by",
                table: "QuestionBanks",
                newName: "IX_QuestionBanks_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionBank_course_id",
                table: "QuestionBanks",
                newName: "IX_QuestionBanks_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topics",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Topics",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SubTopics",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SubTopics",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SubTopics",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OptionText",
                table: "QuestionOptions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "QuestionOptions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Exams",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Exams",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Exams",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<bool>(
                name: "AllowReview",
                table: "Exams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByNavigationId",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamDuration",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "Exams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PassingScore",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RandomizeQuestions",
                table: "Exams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowResults",
                table: "Exams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamQuestions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Courses",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionType",
                table: "QuestionBanks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "QuestionText",
                table: "QuestionBanks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "QuestionBanks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "QuestionBanks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "QuestionBanks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DifficultyLevel",
                table: "QuestionBanks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "QuestionBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionA",
                table: "QuestionBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionB",
                table: "QuestionBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionC",
                table: "QuestionBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionD",
                table: "QuestionBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionE",
                table: "QuestionBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "QuestionBanks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubTopics",
                table: "SubTopics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionOptions",
                table: "QuestionOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exams",
                table: "Exams",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamQuestions",
                table: "ExamQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionBanks",
                table: "QuestionBanks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CreatedByNavigationId",
                table: "Exams",
                column: "CreatedByNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Exams_ExamId",
                table: "ExamQuestions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_QuestionBanks_QuestionId",
                table: "ExamQuestions",
                column: "QuestionId",
                principalTable: "QuestionBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Courses_CourseId",
                table: "Exams",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Users_CreatedByNavigationId",
                table: "Exams",
                column: "CreatedByNavigationId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBanks_Courses_CourseId",
                table: "QuestionBanks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBanks_SubTopics_SubTopicId",
                table: "QuestionBanks",
                column: "SubTopicId",
                principalTable: "SubTopics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBanks_Topics_TopicId",
                table: "QuestionBanks",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBanks_Users_CreatedById",
                table: "QuestionBanks",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_QuestionBanks_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId",
                principalTable: "QuestionBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTopics_Topics_TopicId",
                table: "SubTopics",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Exams_ExamId",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_QuestionBanks_QuestionId",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Courses_CourseId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Users_CreatedByNavigationId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBanks_Courses_CourseId",
                table: "QuestionBanks");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBanks_SubTopics_SubTopicId",
                table: "QuestionBanks");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBanks_Topics_TopicId",
                table: "QuestionBanks");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBanks_Users_CreatedById",
                table: "QuestionBanks");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_QuestionBanks_QuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTopics_Topics_TopicId",
                table: "SubTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubTopics",
                table: "SubTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionOptions",
                table: "QuestionOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exams",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_CreatedByNavigationId",
                table: "Exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamQuestions",
                table: "ExamQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionBanks",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SubTopics");

            migrationBuilder.DropColumn(
                name: "AllowReview",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CreatedByNavigationId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "ExamDuration",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "PassingScore",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "RandomizeQuestions",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "ShowResults",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "DifficultyLevel",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "OptionA",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "OptionB",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "OptionC",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "OptionD",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "OptionE",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "QuestionBanks");

            migrationBuilder.RenameTable(
                name: "SubTopics",
                newName: "Subtopics");

            migrationBuilder.RenameTable(
                name: "QuestionBanks",
                newName: "QuestionBank");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Topics",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Topics",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Topics",
                newName: "course_id");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_CourseId",
                table: "Topics",
                newName: "IX_Topics_course_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Subtopics",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Subtopics",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TopicId",
                table: "Subtopics",
                newName: "topic_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Subtopics",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_SubTopics_TopicId",
                table: "Subtopics",
                newName: "IX_Subtopics_topic_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "QuestionOptions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "QuestionOptions",
                newName: "question_id");

            migrationBuilder.RenameColumn(
                name: "OptionText",
                table: "QuestionOptions",
                newName: "option_text");

            migrationBuilder.RenameColumn(
                name: "IsCorrect",
                table: "QuestionOptions",
                newName: "is_correct");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "QuestionOptions",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "OptionOrder",
                table: "QuestionOptions",
                newName: "order_index");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                newName: "IX_QuestionOptions_question_id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Exams",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Exams",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Exams",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Exams",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Exams",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Exams",
                newName: "course_id");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_CourseId",
                table: "Exams",
                newName: "IX_Exams_course_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ExamQuestions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "QuestionOrder",
                table: "ExamQuestions",
                newName: "question_order");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "ExamQuestions",
                newName: "question_id");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "ExamQuestions",
                newName: "exam_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ExamQuestions",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_ExamQuestions_QuestionId",
                table: "ExamQuestions",
                newName: "IX_ExamQuestions_question_id");

            migrationBuilder.RenameIndex(
                name: "IX_ExamQuestions_ExamId",
                table: "ExamQuestions",
                newName: "IX_ExamQuestions_exam_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Courses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Courses",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "QuestionBank",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TopicId",
                table: "QuestionBank",
                newName: "topic_id");

            migrationBuilder.RenameColumn(
                name: "SubTopicId",
                table: "QuestionBank",
                newName: "subtopic_id");

            migrationBuilder.RenameColumn(
                name: "QuestionType",
                table: "QuestionBank",
                newName: "question_type");

            migrationBuilder.RenameColumn(
                name: "QuestionText",
                table: "QuestionBank",
                newName: "question_text");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "QuestionBank",
                newName: "image_path");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "QuestionBank",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "QuestionBank",
                newName: "course_id");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "QuestionBank",
                newName: "created_by");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionBanks_TopicId",
                table: "QuestionBank",
                newName: "IX_QuestionBank_topic_id");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionBanks_SubTopicId",
                table: "QuestionBank",
                newName: "IX_QuestionBank_subtopic_id");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionBanks_CreatedById",
                table: "QuestionBank",
                newName: "IX_QuestionBank_created_by");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionBanks_CourseId",
                table: "QuestionBank",
                newName: "IX_QuestionBank_course_id");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "Users",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "Users",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Users",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Users",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Topics",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Topics",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Subtopics",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Subtopics",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "option_text",
                table: "QuestionOptions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "QuestionOptions",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Exams",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Exams",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Exams",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "ExamQuestions",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Courses",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Courses",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "question_type",
                table: "QuestionBank",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "question_text",
                table: "QuestionBank",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "image_path",
                table: "QuestionBank",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "QuestionBank",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "difficulty",
                table: "QuestionBank",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Users__3213E83F36AA3206",
                table: "Users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Topics__3213E83F2BD14843",
                table: "Topics",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Subtopic__3213E83FFB3DB425",
                table: "Subtopics",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__QuestionOptions__3213E83F5E39254E",
                table: "QuestionOptions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Exams__3213E83F21D8E48D",
                table: "Exams",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__ExamQuestions__3213E83F21D8E48F",
                table: "ExamQuestions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Courses__3213E83F8EA4DF79",
                table: "Courses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Question__3213E83F5E39254E",
                table: "QuestionBank",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__AB6E6164FC554CB2",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__F3DBC572324A194F",
                table: "Users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_created_by",
                table: "Exams",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "UQ__Courses__72E12F1B18807178",
                table: "Courses",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_exam_questions_exam",
                table: "ExamQuestions",
                column: "exam_id",
                principalTable: "Exams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_exam_questions_question",
                table: "ExamQuestions",
                column: "question_id",
                principalTable: "QuestionBank",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_exams_course",
                table: "Exams",
                column: "course_id",
                principalTable: "Courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_exams_user",
                table: "Exams",
                column: "created_by",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_question_bank_course",
                table: "QuestionBank",
                column: "course_id",
                principalTable: "Courses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_question_bank_subtopic",
                table: "QuestionBank",
                column: "subtopic_id",
                principalTable: "Subtopics",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_question_bank_topic",
                table: "QuestionBank",
                column: "topic_id",
                principalTable: "Topics",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_question_bank_user",
                table: "QuestionBank",
                column: "created_by",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_question_option_question",
                table: "QuestionOptions",
                column: "question_id",
                principalTable: "QuestionBank",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_subtopics_topic",
                table: "Subtopics",
                column: "topic_id",
                principalTable: "Topics",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_topics_course",
                table: "Topics",
                column: "course_id",
                principalTable: "Courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
