using KNTC.Configs;
using KNTC.Complains;
using KNTC.Denounces;
using KNTC.DocumentTypes;
using KNTC.FileAttachments;
using KNTC.LandTypes;
using KNTC.Units;
using KNTC.CategoryUnitTypes;
using KNTC.Users;
using KNTC.SpatialDatas;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
//using Volo.Abp.TenantManagement;
//using Volo.Abp.TenantManagement.EntityFrameworkCore;


namespace KNTC.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
//[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class KNTCDbContext :
    AbpDbContext<KNTCDbContext>,
    IIdentityDbContext
    //ITenantManagementDbContext
{

    #region Entities from the modules
    public DbSet<IdentityUser> Users { get; set; }

    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    //public DbSet<Tenant> Tenants { get; set; }

    //public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion Entities from the modules

    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<Complain> Complains { get; set; }
    public DbSet<Denounce> Denounces { get; set; }
    public DbSet<FileAttachment> FileAttachments { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<LandType> LandTypes { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<UnitType> UnitTypes { get; set; }
    public DbSet<Config> Configs { get; set; }
    public DbSet<SpatialData> SpatialDatas { get; set; }

    public KNTCDbContext(DbContextOptions<KNTCDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        //builder.ConfigureTenantManagement();

        builder.Entity<UserInfo>(b =>
        {
            b.ToTable(KNTCConsts.DbTablePrefix + "UserInfos");
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<UserInfo>(x => x.UserId)
                .IsRequired();
        });

        builder.Entity<Complain>(b =>
        {
            b.ToTable("Complains", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasIndex(x => x.MaHoSo);
            b.Property(x => x.MaHoSo).IsRequired().HasColumnName("ma_ho_so").HasMaxLength(KNTCValidatorConsts.MaxMaHoSoLength);
            b.HasIndex(x => x.LinhVuc);
            b.Property(x => x.LinhVuc).IsRequired().HasColumnName("linh_vuc");
            b.Property(x => x.TieuDe).IsRequired().HasColumnName("tieu_de").HasMaxLength(KNTCValidatorConsts.MaxTieuDeLength);
            b.Property(x => x.NguoiNopDon).IsRequired().HasColumnName("nguoi_nop_don").HasMaxLength(KNTCValidatorConsts.MaxTenNguoiLength);
            b.Property(x => x.CccdCmnd).IsRequired().HasColumnName("cccd_cmnd").HasMaxLength(KNTCValidatorConsts.MaxCccdCmndLength);
            //b.Property(x => x.NgayCapCccdCmnd).IsRequired().HasColumnName("ngay_cap_cccd_cmnd");
            //b.Property(x => x.NoiCapCccdCmnd).IsRequired().HasColumnName("noi_cap_cccd_cmnd").HasMaxLength(KNTCValidatorConsts.MaxNoiCapCccdCmnd);
            b.Property(x => x.NgaySinh).IsRequired().HasColumnName("ngay_sinh");
            b.Property(x => x.DienThoai).IsRequired().HasColumnName("dien_thoai").HasMaxLength(KNTCValidatorConsts.MaxSDTLength);
            b.Property(x => x.Email).HasColumnName("email").HasMaxLength(KNTCValidatorConsts.MaxEmailLength);
            b.Property(x => x.DiaChiThuongTru).IsRequired().HasColumnName("dia_chi_thuong_tru").HasMaxLength(KNTCValidatorConsts.MaxDiaChiLength);
            b.Property(x => x.DiaChiLienHe).IsRequired().HasColumnName("dia_chi_lien_he").HasMaxLength(KNTCValidatorConsts.MaxDiaChiLength);
            b.Property(x => x.MaTinhTP).IsRequired().HasColumnName("ma_tinh_tp");
            b.Property(x => x.MaQuanHuyen).IsRequired().HasColumnName("ma_quan_huyen");
            b.Property(x => x.MaXaPhuongTT).IsRequired().HasColumnName("ma_xa_phuong_tt");
            b.Property(x => x.ThoiGianTiepNhan).IsRequired().HasColumnName("thoi_gian_tiep_nhan");
            b.Property(x => x.ThoiGianHenTraKQ).IsRequired().HasColumnName("thoi_gian_hen_tra_kq");
            b.Property(x => x.NoiDungVuViec).IsRequired().HasColumnName("noi_dung_vu_viec");
            b.Property(x => x.BoPhanDangXL).IsRequired().HasColumnName("bo_phan_dang_xl").HasMaxLength(KNTCValidatorConsts.MaxBoPhanXLLength);

            b.Property(x => x.SoThua).IsRequired().HasColumnName("so_thua").HasMaxLength(KNTCValidatorConsts.MaxSoThuaLength);
            b.Property(x => x.ToBanDo).IsRequired().HasColumnName("to_ban_do").HasMaxLength(KNTCValidatorConsts.MaxToBanDoLength);
            b.Property(x => x.DienTich).IsRequired().HasColumnName("dien_tich");
            b.Property(x => x.LoaiDat).IsRequired().HasColumnName("loai_dat");
            b.Property(x => x.DiaChiThuaDat).IsRequired().HasColumnName("dia_chi_thua_dat").HasMaxLength(KNTCValidatorConsts.MaxDiaChiLength);
            b.Property(x => x.tinhThuaDat).IsRequired().IsRequired().HasColumnName("tinh_thua_dat");
            b.Property(x => x.huyenThuaDat).IsRequired().HasColumnName("huyen_thua_dat");
            b.Property(x => x.xaThuaDat).IsRequired().HasColumnName("xa_thua_dat");

            b.Property(x => x.loaiKhieuNai1).HasColumnName("loai_khieu_nai_1");
            b.Property(x => x.NgayKhieuNai1).HasColumnName("ngay_khieu_nai_1");
            b.Property(x => x.NgayTraKQ1).HasColumnName("ngay_tra_kq_1");
            b.Property(x => x.ThamQuyen1).HasColumnName("tham_quyen_1").HasMaxLength(KNTCValidatorConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQD1).HasColumnName("so_qd_1").HasMaxLength(KNTCValidatorConsts.MaxSoQDLength);
            b.Property(x => x.KetQua1).HasColumnName("ket_qua_1");

            b.Property(x => x.loaiKhieuNai2).HasColumnName("loai_khieu_nai_2");
            b.Property(x => x.NgayKhieuNai2).HasColumnName("ngay_khieu_nai_2");
            b.Property(x => x.NgayTraKQ2).HasColumnName("ngay_tra_kq_2");
            b.Property(x => x.ThamQuyen2).HasColumnName("tham_quyen_2").HasMaxLength(KNTCValidatorConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQD2).HasColumnName("so_qd_2").HasMaxLength(KNTCValidatorConsts.MaxSoQDLength);
            b.Property(x => x.KetQua2).HasColumnName("ket_qua_2");

            b.Property(x => x.DuLieuToaDo).HasColumnName("du_lieu_toa_do").HasMaxLength(KNTCValidatorConsts.MaxToaDoLength);
            b.Property(x => x.DuLieuHinhHoc).HasColumnName("du_lieu_hinh_hoc").HasMaxLength(KNTCValidatorConsts.MaxHinhHocLength);

            b.Property(x => x.GhiChu).HasColumnName("ghi_chu").HasMaxLength(KNTCValidatorConsts.MaxGhiChuLength);
            b.Property(x => x.KetQua).HasColumnName("ket_qua");
            b.Property(x => x.CongKhai).IsRequired().HasColumnName("cong_khai").HasDefaultValue(false);
        });

        builder.Entity<Denounce>(b =>
        {
            b.ToTable("Denounces", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasIndex(x => x.MaHoSo);
            b.Property(x => x.MaHoSo).IsRequired().HasColumnName("ma_ho_so").HasMaxLength(KNTCValidatorConsts.MaxMaHoSoLength);
            b.HasIndex(x => x.LinhVuc);
            b.Property(x => x.LinhVuc).IsRequired().HasColumnName("linh_vuc");
            b.Property(x => x.TieuDe).IsRequired().HasColumnName("tieu_de").HasMaxLength(KNTCValidatorConsts.MaxTieuDeLength);
            b.Property(x => x.NguoiNopDon).IsRequired().HasColumnName("nguoi_nop_don").HasMaxLength(KNTCValidatorConsts.MaxTenNguoiLength);
            b.Property(x => x.CccdCmnd).IsRequired().HasColumnName("cccd_cmnd").HasMaxLength(KNTCValidatorConsts.MaxCccdCmndLength);
            //b.Property(x => x.NgayCapCccdCmnd).IsRequired().HasColumnName("ngay_cap_cccd_cmnd");
            //b.Property(x => x.NoiCapCccdCmnd).IsRequired().HasColumnName("noi_cap_cccd_cmnd").HasMaxLength(KNTCValidatorConsts.MaxNoiCapCccdCmnd);
            b.Property(x => x.NgaySinh).IsRequired().HasColumnName("ngay_sinh");
            b.Property(x => x.DienThoai).IsRequired().HasColumnName("dien_thoai").HasMaxLength(KNTCValidatorConsts.MaxSDTLength);
            b.Property(x => x.Email).HasColumnName("email").HasMaxLength(KNTCValidatorConsts.MaxEmailLength);
            b.Property(x => x.DiaChiThuongTru).IsRequired().HasColumnName("dia_chi_thuong_tru").HasMaxLength(KNTCValidatorConsts.MaxDiaChiLength);
            b.Property(x => x.DiaChiLienHe).IsRequired().HasColumnName("dia_chi_lien_he").HasMaxLength(KNTCValidatorConsts.MaxDiaChiLength);
            b.Property(x => x.MaTinhTP).IsRequired().HasColumnName("ma_tinh_tp");
            b.Property(x => x.MaQuanHuyen).IsRequired().HasColumnName("ma_quan_huyen");
            b.Property(x => x.MaXaPhuongTT).IsRequired().HasColumnName("ma_xa_phuong_tt");
            b.Property(x => x.ThoiGianTiepNhan).IsRequired().HasColumnName("thoi_gian_tiep_nhan");
            b.Property(x => x.ThoiGianHenTraKQ).IsRequired().HasColumnName("thoi_gian_hen_tra_kq");
            b.Property(x => x.NoiDungVuViec).IsRequired().HasColumnName("noi_dung_vu_viec");
            b.Property(x => x.BoPhanDangXL).IsRequired().HasColumnName("bo_phan_dang_xl").HasMaxLength(KNTCValidatorConsts.MaxBoPhanXLLength);

            b.Property(x => x.SoThua).IsRequired().HasColumnName("so_thua").HasMaxLength(KNTCValidatorConsts.MaxSoThuaLength);
            b.Property(x => x.ToBanDo).IsRequired().HasColumnName("to_ban_do").HasMaxLength(KNTCValidatorConsts.MaxToBanDoLength);
            b.Property(x => x.DienTich).IsRequired().HasColumnName("dien_tich");
            b.Property(x => x.LoaiDat).IsRequired().HasColumnName("loai_dat");
            b.Property(x => x.DiaChiThuaDat).IsRequired().HasColumnName("dia_chi_thua_dat").HasMaxLength(KNTCValidatorConsts.MaxDiaChiLength);
            b.Property(x => x.TinhThuaDat).IsRequired().IsRequired().HasColumnName("tinh_thua_dat");
            b.Property(x => x.HuyenThuaDat).IsRequired().HasColumnName("huyen_thua_dat");
            b.Property(x => x.XaThuaDat).IsRequired().HasColumnName("xa_thua_dat");

            b.Property(x => x.DuLieuToaDo).HasColumnName("du_lieu_toa_do").HasMaxLength(KNTCValidatorConsts.MaxToaDoLength);
            b.Property(x => x.DuLieuHinhHoc).HasColumnName("du_lieu_hinh_hoc").HasMaxLength(KNTCValidatorConsts.MaxHinhHocLength);

            b.Property(x => x.GhiChu).HasColumnName("ghi_chu").HasMaxLength(KNTCValidatorConsts.MaxGhiChuLength);
            b.Property(x => x.KetQua).HasColumnName("ket_qua");

            b.Property(x => x.NguoiBiToCao).IsRequired().HasColumnName("nguoi_bi_to_cao").HasMaxLength(KNTCValidatorConsts.MaxTenNguoiLength);

            b.Property(x => x.NgayGQTC).HasColumnName("ngay_GQTC");
            b.Property(x => x.NguoiGQTC).HasColumnName("nguoi_GQTC").HasMaxLength(KNTCValidatorConsts.MaxTenNguoiLength);
            b.Property(x => x.QuyerDinhThuLyGQTC).HasColumnName("quyet_dinh_thu_ly_GQTC").HasMaxLength(KNTCValidatorConsts.MaxSoQDLength);
            b.Property(x => x.NgayQDGQTC).HasColumnName("ngay_QDGQTC");
            b.Property(x => x.QuyetDinhDinhChiGQTC).HasColumnName("quyet_dinh_dinh_chi_GQTC").HasMaxLength(KNTCValidatorConsts.MaxSoQDLength);
            b.Property(x => x.GiaHanGQTC1).HasColumnName("gia_han_GQTC_1");
            b.Property(x => x.GiaHanGQTC2).HasColumnName("gia_han_GQTC_2");
            b.Property(x => x.SoVBKLNDTC).HasColumnName("so_VB_KL_NDTC").HasMaxLength(KNTCValidatorConsts.MaxSoQDLength);
            b.Property(x => x.NgayNhanTBKQXLKLTC).HasColumnName("ngay_nhan_TB_KQXLKLTC");

            b.Property(x => x.CongKhai).IsRequired().HasColumnName("cong_khai").HasDefaultValue(false);
        });

        builder.Entity<FileAttachment>(b =>
        {
            b.ToTable("FileAttachments", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasIndex(x => x.ComplainId);
            b.HasIndex(x => x.DenounceId);
            b.Property(x => x.GiaiDoan).HasColumnName("giai_doan");
            b.Property(x => x.TenTaiLieu).IsRequired().HasColumnName("ten_tai_lieu").HasMaxLength(KNTCValidatorConsts.MaxTenTaiLieuLength);
            b.Property(x => x.HinhThuc).IsRequired().HasColumnName("hinh_thuc").HasMaxLength(KNTCValidatorConsts.MaxHinhThucLength);
            b.Property(x => x.ThoiGianBanHanh).IsRequired().HasColumnName("thoi_gian_ban_hanh");
            b.Property(x => x.NgayNhan).IsRequired().HasColumnName("ngay_nhan");
            b.Property(x => x.ThuTuButLuc).IsRequired().HasColumnName("thu_tu_but_luc").HasMaxLength(KNTCValidatorConsts.MaxThuTuButLucLength);
            b.Property(x => x.NoiDungChinh).HasColumnName("noi_dung_chinh");
            b.Property(x => x.FileName).IsRequired().HasColumnName("file_name").HasMaxLength(KNTCValidatorConsts.MaxFileNameLength);
            b.Property(x => x.ContentType).IsRequired().HasColumnName("content_type").HasMaxLength(KNTCValidatorConsts.MaxContenTypeLength);
            b.Property(x => x.ContentLength).IsRequired().HasColumnName("content_length");
            b.Property(x => x.LoaiVuViec).IsRequired().HasColumnName("loai_vu_viec");
            b.Property(x => x.CongKhai).IsRequired().HasColumnName("cong_khai").HasDefaultValue(false);
            b.HasIndex(x => new { x.LoaiVuViec, x.ComplainId });
            b.HasIndex(x => new { x.LoaiVuViec, x.DenounceId });
        });

        builder.Entity<DocumentType>(b =>
        {
            b.ToTable("DocumentTypes", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(x => x.DocumentTypeCode).IsRequired().HasMaxLength(KNTCValidatorConsts.MaxCodeLength);
            b.Property(x => x.DocumentTypeName).IsRequired().HasMaxLength(KNTCValidatorConsts.MaxNameLength);
            b.Property(x => x.Description).HasMaxLength(KNTCValidatorConsts.MaxDescriptionLength);
            b.Property(x => x.Status).IsRequired().HasDefaultValue(Status.Active);

            b.HasMany(h => h.FileAttachments)
             .WithOne(k => k.DocumentType)
             .HasForeignKey(k => k.HinhThuc)
             .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<LandType>(b =>
        {
            b.ToTable("LandTypes", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(x => x.LandTypeCode).IsRequired().HasMaxLength(KNTCValidatorConsts.MaxCodeLength);
            b.Property(x => x.LandTypeName).IsRequired().HasMaxLength(KNTCValidatorConsts.MaxNameLength);
            b.Property(x => x.Description).HasMaxLength(KNTCValidatorConsts.MaxDescriptionLength);
            b.Property(x => x.Status).IsRequired().HasDefaultValue(Status.Active);

            b.HasMany(l => l.Complains)
             .WithOne(c => c.LandType)
             .HasForeignKey(c => c.LoaiDat)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(l => l.Denounces)
             .WithOne(c => c.LandType)
             .HasForeignKey(c => c.LoaiDat)
             .OnDelete(DeleteBehavior.Restrict);

        });

        builder.Entity<Unit>(b =>
        {
            b.ToTable("Units", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(x => x.UnitCode).IsRequired().HasMaxLength(KNTCValidatorConsts.MaxNameLength);
            b.Property(x => x.UnitName).IsRequired().HasMaxLength(KNTCValidatorConsts.MaxCodeLength);
            b.Property(x => x.ShortName).HasMaxLength(KNTCValidatorConsts.MaxCodeLength);
            b.Property(x => x.Description).HasMaxLength(KNTCValidatorConsts.MaxDescriptionLength);
            b.Property(x => x.Status).IsRequired().HasDefaultValue(Status.Active);

            b.HasIndex(x => x.UnitTypeId);
            b.HasIndex(u => new { u.UnitTypeId, u.ParentId });
        });

        builder.Entity<UnitType>(b =>
        {
            b.ToTable("UnitTypes", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(x => x.UnitTypeCode).IsRequired().HasMaxLength(KNTCValidatorConsts.MaxCodeLength);
            b.Property(x => x.UnitTypeName).IsRequired().HasMaxLength(KNTCValidatorConsts.MaxNameLength);
            b.Property(x => x.Description).HasMaxLength(KNTCValidatorConsts.MaxDescriptionLength);
            b.Property(x => x.Status).IsRequired().HasDefaultValue(Status.Active);

            b.HasMany(h => h.Units)
             .WithOne(k => k.UnitType)
             .HasForeignKey(k => k.UnitTypeId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Config>(b =>
        {
            b.ToTable("Configs", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(x => x.OrganizationCode).IsRequired().HasMaxLength(KNTCValidatorConsts.MaxCodeLength);
            b.Property(x => x.OrganizationName).IsRequired().HasMaxLength(KNTCValidatorConsts.MaxNameLength);
            b.Property(x => x.ToaDo);
            b.Property(x => x.Tel);
            b.Property(x => x.Address).HasMaxLength(KNTCValidatorConsts.MaxDescriptionLength);
            b.Property(x => x.Description).HasMaxLength(KNTCValidatorConsts.MaxDescriptionLength);
            b.Property(x => x.Status).IsRequired().HasDefaultValue(Status.Active);
        });

        builder.Entity<SpatialData>(b =>
        {
            b.ToTable("SpatialData", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(p => p.Id).HasColumnName("Id");
            //b.Property(p => p.Id).ValueGeneratedOnAdd();
            //b.Property(p => p.Geometry).HasColumnName("Geometry");
            b.Property(p => p.GeoJson).HasColumnName("GeoJson");
            b.Property(p => p.ObjectId).HasColumnName("OBJECTID");
            b.Property(p => p.SoToBD).HasColumnName("So_to_BD").HasMaxLength(KNTCValidatorConsts.MaxToBanDoLength);
            b.Property(p => p.TenToChuc).HasColumnName("TenToChuc").HasMaxLength(KNTCValidatorConsts.MaxTenToChucLength);
        });
    }
}