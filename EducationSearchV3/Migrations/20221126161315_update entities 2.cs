using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationSearchV3.Migrations
{
    /// <inheritdoc />
    public partial class updateentities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighSchools_Countries_CountryId",
                table: "HighSchools");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Countries_CountryId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_EducationPrograms_EducationProgramId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_CountryId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_EducationProgramId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "EducationProgramId",
                table: "Languages");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "HighSchools",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CountryLanguage",
                columns: table => new
                {
                    CountriesId = table.Column<int>(type: "int", nullable: false),
                    LanguagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLanguage", x => new { x.CountriesId, x.LanguagesId });
                    table.ForeignKey(
                        name: "FK_CountryLanguage_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryLanguage_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationProgramLanguage",
                columns: table => new
                {
                    EducationProgramsId = table.Column<int>(type: "int", nullable: false),
                    LanguagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationProgramLanguage", x => new { x.EducationProgramsId, x.LanguagesId });
                    table.ForeignKey(
                        name: "FK_EducationProgramLanguage_EducationPrograms_EducationProgramsId",
                        column: x => x.EducationProgramsId,
                        principalTable: "EducationPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationProgramLanguage_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryLanguage_LanguagesId",
                table: "CountryLanguage",
                column: "LanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationProgramLanguage_LanguagesId",
                table: "EducationProgramLanguage",
                column: "LanguagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighSchools_Countries_CountryId",
                table: "HighSchools",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighSchools_Countries_CountryId",
                table: "HighSchools");

            migrationBuilder.DropTable(
                name: "CountryLanguage");

            migrationBuilder.DropTable(
                name: "EducationProgramLanguage");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Languages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EducationProgramId",
                table: "Languages",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "HighSchools",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_CountryId",
                table: "Languages",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_EducationProgramId",
                table: "Languages",
                column: "EducationProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighSchools_Countries_CountryId",
                table: "HighSchools",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Countries_CountryId",
                table: "Languages",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_EducationPrograms_EducationProgramId",
                table: "Languages",
                column: "EducationProgramId",
                principalTable: "EducationPrograms",
                principalColumn: "Id");
        }
    }
}
