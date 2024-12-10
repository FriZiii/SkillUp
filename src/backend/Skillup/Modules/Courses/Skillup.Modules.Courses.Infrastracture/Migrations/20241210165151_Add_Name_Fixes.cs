using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Add_Name_Fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerExercise_AssignmentAssets_AssignmentId",
                schema: "courses",
                table: "QuestionAnswerExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizAnswer_QuizQuestionExercise_QuestionId",
                schema: "courses",
                table: "QuizAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionExercise_AssignmentAssets_AssignmentId",
                schema: "courses",
                table: "QuizQuestionExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestionExercise",
                schema: "courses",
                table: "QuizQuestionExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizAnswer",
                schema: "courses",
                table: "QuizAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswerExercise",
                schema: "courses",
                table: "QuestionAnswerExercise");

            migrationBuilder.RenameTable(
                name: "QuizQuestionExercise",
                schema: "courses",
                newName: "QuizQuestionExercises",
                newSchema: "courses");

            migrationBuilder.RenameTable(
                name: "QuizAnswer",
                schema: "courses",
                newName: "QuizAnswers",
                newSchema: "courses");

            migrationBuilder.RenameTable(
                name: "QuestionAnswerExercise",
                schema: "courses",
                newName: "QuestionAnswerExercises",
                newSchema: "courses");

            migrationBuilder.RenameIndex(
                name: "IX_QuizQuestionExercise_AssignmentId",
                schema: "courses",
                table: "QuizQuestionExercises",
                newName: "IX_QuizQuestionExercises_AssignmentId");

            migrationBuilder.RenameColumn(
                name: "isCorrectAnswer",
                schema: "courses",
                table: "QuizAnswers",
                newName: "IsCorrectAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_QuizAnswer_QuestionId",
                schema: "courses",
                table: "QuizAnswers",
                newName: "IX_QuizAnswers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerExercise_AssignmentId",
                schema: "courses",
                table: "QuestionAnswerExercises",
                newName: "IX_QuestionAnswerExercises_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestionExercises",
                schema: "courses",
                table: "QuizQuestionExercises",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizAnswers",
                schema: "courses",
                table: "QuizAnswers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswerExercises",
                schema: "courses",
                table: "QuestionAnswerExercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerExercises_AssignmentAssets_AssignmentId",
                schema: "courses",
                table: "QuestionAnswerExercises",
                column: "AssignmentId",
                principalSchema: "courses",
                principalTable: "AssignmentAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAnswers_QuizQuestionExercises_QuestionId",
                schema: "courses",
                table: "QuizAnswers",
                column: "QuestionId",
                principalSchema: "courses",
                principalTable: "QuizQuestionExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionExercises_AssignmentAssets_AssignmentId",
                schema: "courses",
                table: "QuizQuestionExercises",
                column: "AssignmentId",
                principalSchema: "courses",
                principalTable: "AssignmentAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerExercises_AssignmentAssets_AssignmentId",
                schema: "courses",
                table: "QuestionAnswerExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizAnswers_QuizQuestionExercises_QuestionId",
                schema: "courses",
                table: "QuizAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionExercises_AssignmentAssets_AssignmentId",
                schema: "courses",
                table: "QuizQuestionExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestionExercises",
                schema: "courses",
                table: "QuizQuestionExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizAnswers",
                schema: "courses",
                table: "QuizAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswerExercises",
                schema: "courses",
                table: "QuestionAnswerExercises");

            migrationBuilder.RenameTable(
                name: "QuizQuestionExercises",
                schema: "courses",
                newName: "QuizQuestionExercise",
                newSchema: "courses");

            migrationBuilder.RenameTable(
                name: "QuizAnswers",
                schema: "courses",
                newName: "QuizAnswer",
                newSchema: "courses");

            migrationBuilder.RenameTable(
                name: "QuestionAnswerExercises",
                schema: "courses",
                newName: "QuestionAnswerExercise",
                newSchema: "courses");

            migrationBuilder.RenameIndex(
                name: "IX_QuizQuestionExercises_AssignmentId",
                schema: "courses",
                table: "QuizQuestionExercise",
                newName: "IX_QuizQuestionExercise_AssignmentId");

            migrationBuilder.RenameColumn(
                name: "IsCorrectAnswer",
                schema: "courses",
                table: "QuizAnswer",
                newName: "isCorrectAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_QuizAnswers_QuestionId",
                schema: "courses",
                table: "QuizAnswer",
                newName: "IX_QuizAnswer_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerExercises_AssignmentId",
                schema: "courses",
                table: "QuestionAnswerExercise",
                newName: "IX_QuestionAnswerExercise_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestionExercise",
                schema: "courses",
                table: "QuizQuestionExercise",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizAnswer",
                schema: "courses",
                table: "QuizAnswer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswerExercise",
                schema: "courses",
                table: "QuestionAnswerExercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerExercise_AssignmentAssets_AssignmentId",
                schema: "courses",
                table: "QuestionAnswerExercise",
                column: "AssignmentId",
                principalSchema: "courses",
                principalTable: "AssignmentAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAnswer_QuizQuestionExercise_QuestionId",
                schema: "courses",
                table: "QuizAnswer",
                column: "QuestionId",
                principalSchema: "courses",
                principalTable: "QuizQuestionExercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionExercise_AssignmentAssets_AssignmentId",
                schema: "courses",
                table: "QuizQuestionExercise",
                column: "AssignmentId",
                principalSchema: "courses",
                principalTable: "AssignmentAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
