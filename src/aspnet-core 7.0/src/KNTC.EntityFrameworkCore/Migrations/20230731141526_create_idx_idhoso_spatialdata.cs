using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class createidxidhosospatialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdHoSo",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                newName: "id_ho_so");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "id_ho_so",
            //    schema: "SPATIAL_DATA",
            //    table: "SpatialDatas",
            //    type: "uuid",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_SpatialDatas_id_ho_so",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                column: "id_ho_so");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SpatialDatas_id_ho_so",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.RenameColumn(
                name: "id_ho_so",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                newName: "IdHoSo");

            //migrationBuilder.AlterColumn<int>(
            //    name: "IdHoSo",
            //    schema: "SPATIAL_DATA",
            //    table: "SpatialDatas",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(Guid),
            //    oldType: "uuid");
        }
    }
}
