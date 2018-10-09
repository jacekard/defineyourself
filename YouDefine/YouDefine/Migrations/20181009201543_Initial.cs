using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace YouDefine.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Information = table.Column<string>(nullable: true),
                    IsFixed = table.Column<bool>(nullable: false),
                    ReportDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ideas",
                columns: table => new
                {
                    IdeaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ideas", x => x.IdeaId);
                });

            migrationBuilder.CreateTable(
                name: "Definitions",
                columns: table => new
                {
                    DefinitionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IdeaId = table.Column<long>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definitions", x => x.DefinitionId);
                    table.ForeignKey(
                        name: "FK_Definitions_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "IdeaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Definitions_IdeaId",
                table: "Definitions",
                column: "IdeaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "Definitions");

            migrationBuilder.DropTable(
                name: "Ideas");
        }
    }
}
