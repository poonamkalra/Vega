using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Migrations
{
    public partial class FeatureModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

                migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature1')");
                migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature2')");
                migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature3')");
                migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature4')");
                migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature5')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");
        }
    }
}
