using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSharp.Migrations
{
    /// <inheritdoc />
    public partial class ExerciseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Exercise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TrainerId",
                table: "Exercise",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Trainer_TrainerId",
                table: "Exercise",
                column: "TrainerId",
                principalTable: "Trainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Trainer_TrainerId",
                table: "Exercise");

            migrationBuilder.DropTable(
                name: "Trainer");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_TrainerId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Exercise");
        }
    }
}
