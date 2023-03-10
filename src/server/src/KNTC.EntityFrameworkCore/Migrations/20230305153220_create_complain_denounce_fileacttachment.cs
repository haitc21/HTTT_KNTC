using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace KNTC.Migrations
{
    public partial class create_complain_denounce_fileacttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "KNTC");

            migrationBuilder.CreateTable(
                name: "Complains",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma_ho_so = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    loai_vu_viec = table.Column<short>(type: "smallint", nullable: false),
                    tieu_de = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nguoi_de_nghi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cccd_cmnd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_cap_cccd_cmnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    noi_cap_cccd_cmnd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_sinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dien_thoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    dia_chi_thuong_tru = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    dia_chi_lien_he = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ma_tinh_tp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ma_quan_huyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ma_xa_phuong_tt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_tiep_nhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_hen_tra_kq = table.Column<DateTime>(type: "datetime2", nullable: false),
                    noi_dung_vu_viec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    so_thua = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    to_ban_do = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dien_tich = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    loai_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dia_chi_thua_dat = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    tinh_thua_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    huyen_thua_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    xa_thua_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_khieu_nai_I = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_tra_kq_I = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tham_quyen_I = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    so_qd_I = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ket_qua_I = table.Column<short>(type: "smallint", nullable: false),
                    ngay_khieu_nai_II = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_tra_kq_II = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tham_quyen_II = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    so_qd_II = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ket_qua_II = table.Column<short>(type: "smallint", nullable: false),
                    ket_qua = table.Column<short>(type: "smallint", nullable: false),
                    du_lieu_toa_do = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    du_lieu_hinh_hoc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ghi_chu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Denounces",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma_ho_so = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    loai_vu_viec = table.Column<short>(type: "smallint", nullable: false),
                    tieu_de = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nguoi_de_nghi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cccd_cmnd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_cap_cccd_cmnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    noi_cap_cccd_cmnd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_sinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dien_thoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    dia_chi_thuong_tru = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    dia_chi_lien_he = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ma_tinh_tp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ma_quan_huyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ma_xa_phuong_tt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_tiep_nhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_hen_tra_kq = table.Column<DateTime>(type: "datetime2", nullable: false),
                    noi_dung_vu_viec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    so_thua = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    to_ban_do = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dien_tich = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    loai_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dia_chi_thua_dat = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    tinh_thua_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    huyen_thua_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    xa_thua_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_khieu_nai_I = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_tra_kq_I = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tham_quyen_I = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    so_qd_I = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ket_qua_I = table.Column<short>(type: "smallint", nullable: false),
                    ngay_khieu_nai_II = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_tra_kq_II = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tham_quyen_II = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    so_qd_II = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ket_qua_II = table.Column<short>(type: "smallint", nullable: false),
                    ket_qua = table.Column<short>(type: "smallint", nullable: false),
                    du_lieu_toa_do = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    du_lieu_hinh_hoc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ghi_chu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denounces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_ho_so = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ten_tai_lieu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    hinh_thuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    thoi_gian_ban_hanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_nhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    thu_tu_but_luc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    noi_dung_chinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_name = table.Column<string>(type: "nvarchar(258)", maxLength: 258, nullable: false),
                    content_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    content_length = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileAttachments_Complains_id_ho_so",
                        column: x => x.id_ho_so,
                        principalSchema: "KNTC",
                        principalTable: "Complains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileAttachments_Denounces_id_ho_so",
                        column: x => x.id_ho_so,
                        principalSchema: "KNTC",
                        principalTable: "Denounces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complains_loai_vu_viec",
                schema: "KNTC",
                table: "Complains",
                column: "loai_vu_viec");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_ma_ho_so",
                schema: "KNTC",
                table: "Complains",
                column: "ma_ho_so");

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_loai_vu_viec",
                schema: "KNTC",
                table: "Denounces",
                column: "loai_vu_viec");

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_ma_ho_so",
                schema: "KNTC",
                table: "Denounces",
                column: "ma_ho_so");

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                column: "id_ho_so");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAttachments",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "Complains",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "Denounces",
                schema: "KNTC");
        }
    }
}
