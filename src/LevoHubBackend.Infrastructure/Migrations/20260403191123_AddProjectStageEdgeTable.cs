using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LevoHubBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectStageEdgeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectStageEdges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    FromProjectStageId = table.Column<int>(type: "integer", nullable: false),
                    ToProjectStageId = table.Column<int>(type: "integer", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: true),
                    LagDays = table.Column<int>(type: "integer", nullable: false),
                    EdgeType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStageEdges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectStageEdges_ProjectStages_FromProjectStageId",
                        column: x => x.FromProjectStageId,
                        principalTable: "ProjectStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectStageEdges_ProjectStages_ToProjectStageId",
                        column: x => x.ToProjectStageId,
                        principalTable: "ProjectStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectStageEdges_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStageEdges_FromProjectStageId",
                table: "ProjectStageEdges",
                column: "FromProjectStageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStageEdges_ProjectId",
                table: "ProjectStageEdges",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStageEdges_ToProjectStageId",
                table: "ProjectStageEdges",
                column: "ToProjectStageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectStageEdges");
        }
    }
}
