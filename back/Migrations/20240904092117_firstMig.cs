using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back.Migrations
{
    /// <inheritdoc />
    public partial class firstMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ReportData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    NIC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => new { x.NIC, x.Nationality });
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    No = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    AmountOfMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoneyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantNIC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicantNationality = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.No);
                    table.ForeignKey(
                        name: "FK_Applications_Applicants_ApplicantNIC_ApplicantNationality",
                        columns: x => new { x.ApplicantNIC, x.ApplicantNationality },
                        principalTable: "Applicants",
                        principalColumns: new[] { "NIC", "Nationality" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisaType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisaIssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisaValidityPeriod = table.Column<int>(type: "int", nullable: false),
                    DateLeaving = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantNIC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicantNationality = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_Applicants_ApplicantNIC_ApplicantNationality",
                        columns: x => new { x.ApplicantNIC, x.ApplicantNationality },
                        principalTable: "Applicants",
                        principalColumns: new[] { "NIC", "Nationality" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfExpire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicantNIC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicantNationality = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => new { x.Country, x.Id });
                    table.ForeignKey(
                        name: "FK_Passports_Applicants_ApplicantNIC_ApplicantNationality",
                        columns: x => new { x.ApplicantNIC, x.ApplicantNationality },
                        principalTable: "Applicants",
                        principalColumns: new[] { "NIC", "Nationality" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spouses",
                columns: table => new
                {
                    ApplicantNIC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicantNationality = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpouseNIC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spouses", x => new { x.ApplicantNIC, x.ApplicantNationality, x.SpouseNIC });
                    table.ForeignKey(
                        name: "FK_Spouses_Applicants_ApplicantNIC_ApplicantNationality",
                        columns: x => new { x.ApplicantNIC, x.ApplicantNationality },
                        principalTable: "Applicants",
                        principalColumns: new[] { "NIC", "Nationality" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicantNIC_ApplicantNationality",
                table: "Applications",
                columns: new[] { "ApplicantNIC", "ApplicantNationality" });

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ApplicantNIC_ApplicantNationality",
                table: "Histories",
                columns: new[] { "ApplicantNIC", "ApplicantNationality" });

            migrationBuilder.CreateIndex(
                name: "IX_Passports_ApplicantNIC_ApplicantNationality",
                table: "Passports",
                columns: new[] { "ApplicantNIC", "ApplicantNationality" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_ApplicantNIC_ApplicantNationality",
                table: "Spouses",
                columns: new[] { "ApplicantNIC", "ApplicantNationality" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Passports");

            migrationBuilder.DropTable(
                name: "Spouses");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ReportData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
