using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class intitdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "KNTC");

            migrationBuilder.CreateTable(
                name: "AppSysConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSysConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Dob = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserInfos_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DocumentTypeCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DocumentTypeName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LandTypes",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LandTypeCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LandTypeName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpatialData",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OBJECTID = table.Column<double>(type: "double precision", nullable: false),
                    TenToChuc = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Quyen = table.Column<float>(type: "real", nullable: true),
                    SotoBD = table.Column<string>(name: "So_to_BD", type: "character varying(255)", maxLength: 255, nullable: true),
                    GeoJson = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpatialData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitTypes",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitTypeCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UnitTypeName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComplainId = table.Column<Guid>(type: "uuid", nullable: true),
                    DenounceId = table.Column<Guid>(type: "uuid", nullable: true),
                    giaidoan = table.Column<int>(name: "giai_doan", type: "integer", nullable: false),
                    tentailieu = table.Column<string>(name: "ten_tai_lieu", type: "character varying(250)", maxLength: 250, nullable: false),
                    hinhthuc = table.Column<int>(name: "hinh_thuc", type: "integer", maxLength: 50, nullable: false),
                    thoigianbanhanh = table.Column<DateTime>(name: "thoi_gian_ban_hanh", type: "timestamp without time zone", nullable: false),
                    ngaynhan = table.Column<DateTime>(name: "ngay_nhan", type: "timestamp without time zone", nullable: false),
                    thutubutluc = table.Column<string>(name: "thu_tu_but_luc", type: "character varying(50)", maxLength: 50, nullable: false),
                    noidungchinh = table.Column<string>(name: "noi_dung_chinh", type: "text", nullable: false),
                    filename = table.Column<string>(name: "file_name", type: "character varying(258)", maxLength: 258, nullable: false),
                    contenttype = table.Column<string>(name: "content_type", type: "character varying(100)", maxLength: 100, nullable: false),
                    contentlength = table.Column<long>(name: "content_length", type: "bigint", nullable: false),
                    loaivuviec = table.Column<int>(name: "loai_vu_viec", type: "integer", nullable: false),
                    congkhai = table.Column<bool>(name: "cong_khai", type: "boolean", nullable: false, defaultValue: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileAttachments_DocumentTypes_hinh_thuc",
                        column: x => x.hinhthuc,
                        principalSchema: "KNTC",
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Complains",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    mahoso = table.Column<string>(name: "ma_ho_so", type: "character varying(50)", maxLength: 50, nullable: false),
                    linhvuc = table.Column<int>(name: "linh_vuc", type: "integer", nullable: false),
                    tieude = table.Column<string>(name: "tieu_de", type: "character varying(100)", maxLength: 100, nullable: false),
                    nguoinopdon = table.Column<string>(name: "nguoi_nop_don", type: "character varying(100)", maxLength: 100, nullable: false),
                    cccdcmnd = table.Column<string>(name: "cccd_cmnd", type: "character varying(50)", maxLength: 50, nullable: false),
                    ngaysinh = table.Column<DateTime>(name: "ngay_sinh", type: "timestamp without time zone", nullable: false),
                    dienthoai = table.Column<string>(name: "dien_thoai", type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    diachithuongtru = table.Column<string>(name: "dia_chi_thuong_tru", type: "character varying(250)", maxLength: 250, nullable: false),
                    diachilienhe = table.Column<string>(name: "dia_chi_lien_he", type: "character varying(250)", maxLength: 250, nullable: false),
                    matinhtp = table.Column<int>(name: "ma_tinh_tp", type: "integer", nullable: false),
                    maquanhuyen = table.Column<int>(name: "ma_quan_huyen", type: "integer", nullable: false),
                    maxaphuongtt = table.Column<int>(name: "ma_xa_phuong_tt", type: "integer", nullable: false),
                    thoigiantiepnhan = table.Column<DateTime>(name: "thoi_gian_tiep_nhan", type: "timestamp without time zone", nullable: false),
                    thoigianhentrakq = table.Column<DateTime>(name: "thoi_gian_hen_tra_kq", type: "timestamp without time zone", nullable: false),
                    noidungvuviec = table.Column<string>(name: "noi_dung_vu_viec", type: "text", nullable: false),
                    bophandangxl = table.Column<string>(name: "bo_phan_dang_xl", type: "character varying(250)", maxLength: 250, nullable: false),
                    sothua = table.Column<string>(name: "so_thua", type: "character varying(50)", maxLength: 50, nullable: false),
                    tobando = table.Column<string>(name: "to_ban_do", type: "character varying(255)", maxLength: 255, nullable: false),
                    dientich = table.Column<decimal>(name: "dien_tich", type: "numeric", nullable: false),
                    loaidat = table.Column<int>(name: "loai_dat", type: "integer", nullable: false),
                    diachithuadat = table.Column<string>(name: "dia_chi_thua_dat", type: "character varying(250)", maxLength: 250, nullable: false),
                    tinhthuadat = table.Column<int>(name: "tinh_thua_dat", type: "integer", nullable: false),
                    huyenthuadat = table.Column<int>(name: "huyen_thua_dat", type: "integer", nullable: false),
                    xathuadat = table.Column<int>(name: "xa_thua_dat", type: "integer", nullable: false),
                    dulieutoado = table.Column<string>(name: "du_lieu_toa_do", type: "character varying(128)", maxLength: 128, nullable: false),
                    dulieuhinhhoc = table.Column<string>(name: "du_lieu_hinh_hoc", type: "TEXT", nullable: false),
                    ghichu = table.Column<string>(name: "ghi_chu", type: "character varying(250)", maxLength: 250, nullable: false),
                    loaikhieunai1 = table.Column<int>(name: "loai_khieu_nai_1", type: "integer", nullable: true),
                    ngaykhieunai1 = table.Column<DateTime>(name: "ngay_khieu_nai_1", type: "timestamp without time zone", nullable: true),
                    ngaytrakq1 = table.Column<DateTime>(name: "ngay_tra_kq_1", type: "timestamp without time zone", nullable: true),
                    thamquyen1 = table.Column<string>(name: "tham_quyen_1", type: "character varying(250)", maxLength: 250, nullable: false),
                    soqd1 = table.Column<string>(name: "so_qd_1", type: "character varying(100)", maxLength: 100, nullable: false),
                    ketqua1 = table.Column<int>(name: "ket_qua_1", type: "integer", nullable: true),
                    loaikhieunai2 = table.Column<int>(name: "loai_khieu_nai_2", type: "integer", nullable: true),
                    ngaykhieunai2 = table.Column<DateTime>(name: "ngay_khieu_nai_2", type: "timestamp without time zone", nullable: true),
                    ngaytrakq2 = table.Column<DateTime>(name: "ngay_tra_kq_2", type: "timestamp without time zone", nullable: true),
                    thamquyen2 = table.Column<string>(name: "tham_quyen_2", type: "character varying(250)", maxLength: 250, nullable: false),
                    soqd2 = table.Column<string>(name: "so_qd_2", type: "character varying(100)", maxLength: 100, nullable: false),
                    ketqua2 = table.Column<int>(name: "ket_qua_2", type: "integer", nullable: true),
                    ketqua = table.Column<int>(name: "ket_qua", type: "integer", nullable: true),
                    congkhai = table.Column<bool>(name: "cong_khai", type: "boolean", nullable: false, defaultValue: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complains_LandTypes_loai_dat",
                        column: x => x.loaidat,
                        principalSchema: "KNTC",
                        principalTable: "LandTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Denounces",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    mahoso = table.Column<string>(name: "ma_ho_so", type: "character varying(50)", maxLength: 50, nullable: false),
                    linhvuc = table.Column<int>(name: "linh_vuc", type: "integer", nullable: false),
                    tieude = table.Column<string>(name: "tieu_de", type: "character varying(100)", maxLength: 100, nullable: false),
                    nguoinopdon = table.Column<string>(name: "nguoi_nop_don", type: "character varying(100)", maxLength: 100, nullable: false),
                    cccdcmnd = table.Column<string>(name: "cccd_cmnd", type: "character varying(50)", maxLength: 50, nullable: false),
                    ngaysinh = table.Column<DateTime>(name: "ngay_sinh", type: "timestamp without time zone", nullable: false),
                    dienthoai = table.Column<string>(name: "dien_thoai", type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    diachithuongtru = table.Column<string>(name: "dia_chi_thuong_tru", type: "character varying(250)", maxLength: 250, nullable: false),
                    diachilienhe = table.Column<string>(name: "dia_chi_lien_he", type: "character varying(250)", maxLength: 250, nullable: false),
                    matinhtp = table.Column<int>(name: "ma_tinh_tp", type: "integer", nullable: false),
                    maquanhuyen = table.Column<int>(name: "ma_quan_huyen", type: "integer", nullable: false),
                    maxaphuongtt = table.Column<int>(name: "ma_xa_phuong_tt", type: "integer", nullable: false),
                    thoigiantiepnhan = table.Column<DateTime>(name: "thoi_gian_tiep_nhan", type: "timestamp without time zone", nullable: false),
                    thoigianhentrakq = table.Column<DateTime>(name: "thoi_gian_hen_tra_kq", type: "timestamp without time zone", nullable: false),
                    noidungvuviec = table.Column<string>(name: "noi_dung_vu_viec", type: "text", nullable: false),
                    nguoibitocao = table.Column<string>(name: "nguoi_bi_to_cao", type: "character varying(100)", maxLength: 100, nullable: false),
                    bophandangxl = table.Column<string>(name: "bo_phan_dang_xl", type: "character varying(250)", maxLength: 250, nullable: false),
                    sothua = table.Column<string>(name: "so_thua", type: "character varying(50)", maxLength: 50, nullable: false),
                    tobando = table.Column<string>(name: "to_ban_do", type: "character varying(255)", maxLength: 255, nullable: false),
                    dientich = table.Column<decimal>(name: "dien_tich", type: "numeric", nullable: false),
                    loaidat = table.Column<int>(name: "loai_dat", type: "integer", nullable: false),
                    diachithuadat = table.Column<string>(name: "dia_chi_thua_dat", type: "character varying(250)", maxLength: 250, nullable: false),
                    tinhthuadat = table.Column<int>(name: "tinh_thua_dat", type: "integer", nullable: false),
                    huyenthuadat = table.Column<int>(name: "huyen_thua_dat", type: "integer", nullable: false),
                    xathuadat = table.Column<int>(name: "xa_thua_dat", type: "integer", nullable: false),
                    dulieutoado = table.Column<string>(name: "du_lieu_toa_do", type: "character varying(128)", maxLength: 128, nullable: false),
                    dulieuhinhhoc = table.Column<string>(name: "du_lieu_hinh_hoc", type: "TEXT", nullable: false),
                    ghichu = table.Column<string>(name: "ghi_chu", type: "character varying(250)", maxLength: 250, nullable: false),
                    ngayGQTC = table.Column<DateTime>(name: "ngay_GQTC", type: "timestamp without time zone", nullable: true),
                    nguoiGQTC = table.Column<string>(name: "nguoi_GQTC", type: "character varying(100)", maxLength: 100, nullable: false),
                    quyetdinhthulyGQTC = table.Column<string>(name: "quyet_dinh_thu_ly_GQTC", type: "character varying(100)", maxLength: 100, nullable: false),
                    ngayQDGQTC = table.Column<DateTime>(name: "ngay_QDGQTC", type: "timestamp without time zone", nullable: true),
                    quyetdinhdinhchiGQTC = table.Column<string>(name: "quyet_dinh_dinh_chi_GQTC", type: "character varying(100)", maxLength: 100, nullable: false),
                    giahanGQTC1 = table.Column<DateTime>(name: "gia_han_GQTC_1", type: "timestamp without time zone", nullable: true),
                    giahanGQTC2 = table.Column<DateTime>(name: "gia_han_GQTC_2", type: "timestamp without time zone", nullable: true),
                    soVBKLNDTC = table.Column<string>(name: "so_VB_KL_NDTC", type: "character varying(100)", maxLength: 100, nullable: false),
                    ngaynhanTBKQXLKLTC = table.Column<DateTime>(name: "ngay_nhan_TB_KQXLKLTC", type: "timestamp without time zone", nullable: true),
                    ketqua = table.Column<int>(name: "ket_qua", type: "integer", nullable: true),
                    congkhai = table.Column<bool>(name: "cong_khai", type: "boolean", nullable: false, defaultValue: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denounces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Denounces_LandTypes_loai_dat",
                        column: x => x.loaidat,
                        principalSchema: "KNTC",
                        principalTable: "LandTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UnitName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UnitTypeId = table.Column<int>(type: "integer", nullable: false),
                    ConfigId = table.Column<int>(type: "integer", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_UnitTypes_UnitTypeId",
                        column: x => x.UnitTypeId,
                        principalSchema: "KNTC",
                        principalTable: "UnitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSysConfigs_Name",
                table: "AppSysConfigs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserInfos_UserId",
                table: "AppUserInfos",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complains_linh_vuc",
                schema: "KNTC",
                table: "Complains",
                column: "linh_vuc");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_loai_dat",
                schema: "KNTC",
                table: "Complains",
                column: "loai_dat");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_ma_ho_so",
                schema: "KNTC",
                table: "Complains",
                column: "ma_ho_so");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_thoi_gian_tiep_nhan_ma_ho_so",
                schema: "KNTC",
                table: "Complains",
                columns: new[] { "thoi_gian_tiep_nhan", "ma_ho_so" });

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_linh_vuc",
                schema: "KNTC",
                table: "Denounces",
                column: "linh_vuc");

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_loai_dat",
                schema: "KNTC",
                table: "Denounces",
                column: "loai_dat");

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_ma_ho_so",
                schema: "KNTC",
                table: "Denounces",
                column: "ma_ho_so");

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_thoi_gian_tiep_nhan_ma_ho_so",
                schema: "KNTC",
                table: "Denounces",
                columns: new[] { "thoi_gian_tiep_nhan", "ma_ho_so" });

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_ComplainId",
                schema: "KNTC",
                table: "FileAttachments",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_DenounceId",
                schema: "KNTC",
                table: "FileAttachments",
                column: "DenounceId");

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_hinh_thuc",
                schema: "KNTC",
                table: "FileAttachments",
                column: "hinh_thuc");

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_loai_vu_viec_ComplainId",
                schema: "KNTC",
                table: "FileAttachments",
                columns: new[] { "loai_vu_viec", "ComplainId" });

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_loai_vu_viec_DenounceId",
                schema: "KNTC",
                table: "FileAttachments",
                columns: new[] { "loai_vu_viec", "DenounceId" });

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitTypeId",
                schema: "KNTC",
                table: "Units",
                column: "UnitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitTypeId_ParentId",
                schema: "KNTC",
                table: "Units",
                columns: new[] { "UnitTypeId", "ParentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSysConfigs");

            migrationBuilder.DropTable(
                name: "AppUserInfos");

            migrationBuilder.DropTable(
                name: "Complains",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "Denounces",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "FileAttachments",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "SpatialData",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "LandTypes",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "DocumentTypes",
                schema: "KNTC");

            migrationBuilder.DropTable(
                name: "UnitTypes",
                schema: "KNTC");
        }
    }
}
