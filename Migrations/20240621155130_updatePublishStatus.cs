using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HE176084_MinhBT_A3.Migrations
{
    public partial class updatePublishStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PublishStatus",
                table: "Posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "PublishStatus",
                table: "Posts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
