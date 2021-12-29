using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeShop.Migrations
{
    public partial class Clients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "Bikes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_ClientId",
                table: "Bikes",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_Clients_ClientId",
                table: "Bikes",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_Clients_ClientId",
                table: "Bikes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Bikes_ClientId",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Bikes");
        }
    }
}
