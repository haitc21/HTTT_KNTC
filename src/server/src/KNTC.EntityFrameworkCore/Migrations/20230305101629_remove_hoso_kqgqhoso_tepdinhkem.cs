using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class remove_hoso_kqgqhoso_tepdinhkem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kqgq_ho_so",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "tep_dinh_kem_ho_so",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "ho_so",
                schema: "KNTC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "KNTC");

            migrationBuilder.CreateTable(
                name: "ho_so",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cccd_cmnd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dia_chi_lien_he = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    dia_chi_thua_dat = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    dia_chi_thuong_tru = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    dien_thoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dien_tich = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    du_lieu_hinh_hoc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    du_lieu_toa_do = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    huyen_thua_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ket_qua = table.Column<short>(type: "smallint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    linh_vuc = table.Column<short>(type: "smallint", nullable: false),
                    loai_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    loai_vu_viec = table.Column<short>(type: "smallint", nullable: false),
                    ma_ho_so = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ma_quan_huyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ma_tinh_tp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ma_xa_phuong_tt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ngay_cap_cccd_cmnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_hen_tra_kq = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_tiep_nhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nguoi_de_nghi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    noi_cap_cccd_cmnd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    noi_dung_vu_viec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    so_lan_tra_kq = table.Column<short>(type: "smallint", nullable: false),
                    so_thua = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tieu_de = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tinh_thua_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    to_ban_do = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    xa_thua_dat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ho_so", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kqgq_ho_so",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_ho_so = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ghi_chu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ket_qua = table.Column<short>(type: "smallint", nullable: false),
                    lan_gq = table.Column<short>(type: "smallint", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ngay_tra_kq = table.Column<DateTime>(type: "datetime2", nullable: false),
                    so_qd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tham_quyen = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ngay_khieu_nai = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kqgq_ho_so", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kqgq_ho_so_ho_so_id_ho_so",
                        column: x => x.id_ho_so,
                        principalSchema: "KNTC",
                        principalTable: "ho_so",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tep_dinh_kem_ho_so",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_ho_so = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    content_length = table.Column<long>(type: "bigint", nullable: false),
                    content_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    file_name = table.Column<string>(type: "nvarchar(258)", maxLength: 258, nullable: false),
                    hinh_thuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ngay_nhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    noi_dung_chinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ten_tai_lieu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    thoi_gian_ban_hanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    thu_tu_but_luc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tep_dinh_kem_ho_so", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tep_dinh_kem_ho_so_ho_so_id_ho_so",
                        column: x => x.id_ho_so,
                        principalSchema: "KNTC",
                        principalTable: "ho_so",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ho_so_ma_ho_so",
                schema: "KNTC",
                table: "ho_so",
                column: "ma_ho_so");

            migrationBuilder.CreateIndex(
                name: "IX_kqgq_ho_so_id_ho_so",
                schema: "KNTC",
                table: "kqgq_ho_so",
                column: "id_ho_so");

            migrationBuilder.CreateIndex(
                name: "IX_tep_dinh_kem_ho_so_id_ho_so",
                schema: "KNTC",
                table: "tep_dinh_kem_ho_so",
                column: "id_ho_so");
        }
    }
}
