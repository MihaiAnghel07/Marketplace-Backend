using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class TwuelvethCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Car_CarId",
                table: "CarFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Feature_FeatureId",
                table: "CarFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarFeatures",
                table: "CarFeatures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarFeatures",
                table: "CarFeatures",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarFeatures_CarId",
                table: "CarFeatures",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Car_CarId",
                table: "CarFeatures",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Feature_FeatureId",
                table: "CarFeatures",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Car_CarId",
                table: "CarFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Feature_FeatureId",
                table: "CarFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarFeatures",
                table: "CarFeatures");

            migrationBuilder.DropIndex(
                name: "IX_CarFeatures_CarId",
                table: "CarFeatures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarFeatures",
                table: "CarFeatures",
                columns: new[] { "CarId", "FeatureId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Car_CarId",
                table: "CarFeatures",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Feature_FeatureId",
                table: "CarFeatures",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id");
        }
    }
}
