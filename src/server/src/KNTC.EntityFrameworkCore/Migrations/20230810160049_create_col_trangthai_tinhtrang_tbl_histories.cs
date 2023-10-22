using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class createcoltrangthaitinhtrangtblhistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tinh_trang",
                schema: "KNTC",
                table: "Denounces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "trang_thai",
                schema: "KNTC",
                table: "Denounces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "tinh_trang",
                schema: "KNTC",
                table: "Complains",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "trang_thai",
                schema: "KNTC",
                table: "Complains",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Histories",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idhoso = table.Column<Guid>(name: "id_ho_so", type: "uuid", nullable: false),
                    loaivuviec = table.Column<int>(name: "loai_vu_viec", type: "integer", nullable: false),
                    thaotac = table.Column<int>(name: "thao_tac", type: "integer", nullable: false),
                    nguoithuchien = table.Column<Guid>(name: "nguoi_thuc_hien", type: "uuid", nullable: false),
                    ghichu = table.Column<string>(name: "ghi_chu", type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_tinh_trang",
                schema: "KNTC",
                table: "Denounces",
                column: "tinh_trang");

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_trang_thai",
                schema: "KNTC",
                table: "Denounces",
                column: "trang_thai");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_tinh_trang",
                schema: "KNTC",
                table: "Complains",
                column: "tinh_trang");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_trang_thai",
                schema: "KNTC",
                table: "Complains",
                column: "trang_thai");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_id_ho_so",
                schema: "KNTC",
                table: "Histories",
                column: "id_ho_so");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_loai_vu_viec",
                schema: "KNTC",
                table: "Histories",
                column: "loai_vu_viec");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Histories",
                schema: "KNTC");

            migrationBuilder.DropIndex(
                name: "IX_Denounces_tinh_trang",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Denounces_trang_thai",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Complains_tinh_trang",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropIndex(
                name: "IX_Complains_trang_thai",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "tinh_trang",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "trang_thai",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "tinh_trang",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "trang_thai",
                schema: "KNTC",
                table: "Complains");
        }
    }
}