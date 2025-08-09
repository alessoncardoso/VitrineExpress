using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitrineExpress.Migrations
{
    /// <inheritdoc />
    public partial class UnificaClienteELojistaNoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrinhos_Clientes_ClienteId",
                table: "Carrinhos");

            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Clientes_ClienteId",
                table: "Enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Lojas_Lojistas_LojistaId",
                table: "Lojas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Lojistas");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Pedidos",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                newName: "IX_Pedidos_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "LojistaId",
                table: "Lojas",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Lojas_LojistaId",
                table: "Lojas",
                newName: "IX_Lojas_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Enderecos",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_ClienteId",
                table: "Enderecos",
                newName: "IX_Enderecos_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Carrinhos",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Carrinhos_ClienteId",
                table: "Carrinhos",
                newName: "IX_Carrinhos_UsuarioId");

            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "Usuarios",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Usuarios",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoUsuario",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "ValorMinimoPedido",
                table: "Lojas",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "TaxaEntrega",
                table: "Lojas",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinhos_Usuarios_UsuarioId",
                table: "Carrinhos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Usuarios_UsuarioId",
                table: "Enderecos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lojas_Usuarios_UsuarioId",
                table: "Lojas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrinhos_Usuarios_UsuarioId",
                table: "Carrinhos");

            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Usuarios_UsuarioId",
                table: "Enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Lojas_Usuarios_UsuarioId",
                table: "Lojas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TipoUsuario",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Pedidos",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos",
                newName: "IX_Pedidos_ClienteId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Lojas",
                newName: "LojistaId");

            migrationBuilder.RenameIndex(
                name: "IX_Lojas_UsuarioId",
                table: "Lojas",
                newName: "IX_Lojas_LojistaId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Enderecos",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_UsuarioId",
                table: "Enderecos",
                newName: "IX_Enderecos_ClienteId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Carrinhos",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Carrinhos_UsuarioId",
                table: "Carrinhos",
                newName: "IX_Carrinhos_ClienteId");

            migrationBuilder.AlterColumn<double>(
                name: "ValorMinimoPedido",
                table: "Lojas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TaxaEntrega",
                table: "Lojas",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lojistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lojistas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lojistas_UsuarioId",
                table: "Lojistas",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinhos_Clientes_ClienteId",
                table: "Carrinhos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Clientes_ClienteId",
                table: "Enderecos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lojas_Lojistas_LojistaId",
                table: "Lojas",
                column: "LojistaId",
                principalTable: "Lojistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
