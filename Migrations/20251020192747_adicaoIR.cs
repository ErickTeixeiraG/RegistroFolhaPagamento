using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIFolha.Migrations
{
    /// <inheritdoc />
    public partial class adicaoIR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ValorHora",
                table: "FolhaPagamentos",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "SalarioBruto",
                table: "FolhaPagamentos",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<double>(
                name: "HorasTrabalhadas",
                table: "FolhaPagamentos",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<double>(
                name: "IR",
                table: "FolhaPagamentos",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IR",
                table: "FolhaPagamentos");

            migrationBuilder.AlterColumn<int>(
                name: "ValorHora",
                table: "FolhaPagamentos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "SalarioBruto",
                table: "FolhaPagamentos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "HorasTrabalhadas",
                table: "FolhaPagamentos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
