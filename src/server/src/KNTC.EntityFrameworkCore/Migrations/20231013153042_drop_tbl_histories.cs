using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class drop_tbl_histories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Histories",
                schema: "KNTC");

            migrationBuilder.DropSequence(
                name: "Sequence-History");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Sequence-History",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Histories",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ghi_chu = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    id_ho_so = table.Column<Guid>(type: "uuid", nullable: false),
                    loai_vu_viec = table.Column<int>(type: "integer", nullable: false),
                    nguoi_thuc_hien = table.Column<Guid>(type: "uuid", nullable: false),
                    thao_tac = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

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
    }
}