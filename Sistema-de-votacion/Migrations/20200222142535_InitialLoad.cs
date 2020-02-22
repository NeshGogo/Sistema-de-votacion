using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sistema_de_votacion.Migrations
{
    public partial class InitialLoad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citizen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DNI = table.Column<string>(maxLength: 13, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Election",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Election", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliticParty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    PartyLogoPath = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticParty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectionCitizen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ElectionId = table.Column<int>(nullable: false),
                    CitizenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionCitizen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectionCitizen_Citizen",
                        column: x => x.CitizenId,
                        principalTable: "Citizen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectionCitizen_Election",
                        column: x => x.ElectionId,
                        principalTable: "Election",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectionPoliticParty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ElectionId = table.Column<int>(nullable: false),
                    PoliticPartyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionPoliticParty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectionPoliticParty_Election",
                        column: x => x.ElectionId,
                        principalTable: "Election",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectionPoliticParty_PoliticParty",
                        column: x => x.PoliticPartyId,
                        principalTable: "PoliticParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    PoliticPartyId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    ProfilePhothoPath = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidate_PoliticParty",
                        column: x => x.PoliticPartyId,
                        principalTable: "PoliticParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidate_Position",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectionPosition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ElectionId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectionPosition_Election",
                        column: x => x.ElectionId,
                        principalTable: "Election",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectionPosition_Position",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectionCadidate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ElectionId = table.Column<int>(nullable: false),
                    CandidateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionCadidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectionCandidate_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectionCandidate_Election",
                        column: x => x.ElectionId,
                        principalTable: "Election",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_PoliticPartyId",
                table: "Candidate",
                column: "PoliticPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_PositionId",
                table: "Candidate",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCadidate_CandidateId",
                table: "ElectionCadidate",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCadidate_ElectionId",
                table: "ElectionCadidate",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCitizen_CitizenId",
                table: "ElectionCitizen",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCitizen_ElectionId",
                table: "ElectionCitizen",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionPoliticParty_ElectionId",
                table: "ElectionPoliticParty",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionPoliticParty_PoliticPartyId",
                table: "ElectionPoliticParty",
                column: "PoliticPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionPosition_ElectionId",
                table: "ElectionPosition",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionPosition_PositionId",
                table: "ElectionPosition",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectionCadidate");

            migrationBuilder.DropTable(
                name: "ElectionCitizen");

            migrationBuilder.DropTable(
                name: "ElectionPoliticParty");

            migrationBuilder.DropTable(
                name: "ElectionPosition");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Citizen");

            migrationBuilder.DropTable(
                name: "Election");

            migrationBuilder.DropTable(
                name: "PoliticParty");

            migrationBuilder.DropTable(
                name: "Position");
        }
    }
}
