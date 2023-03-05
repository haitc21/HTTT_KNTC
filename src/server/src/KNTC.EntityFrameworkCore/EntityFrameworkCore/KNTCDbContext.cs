using Microsoft.EntityFrameworkCore;
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
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using KNTC.Users;
using Newtonsoft.Json.Linq;
using System.Reflection.Emit;
using KNTC.Complains;
using KNTC.FileAttachments;
using KNTC.Denounces;

namespace KNTC.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class KNTCDbContext :
    AbpDbContext<KNTCDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{

    #region Entities from the modules
    public DbSet<IdentityUser> Users { get; set; }

    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<Complain> Complains { get; set; }
    public DbSet<Denounce> Denounces { get; set; }
    public DbSet<FileAttachment> FileAttachments { get; set; }


    #endregion Entities from the modules

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
        builder.ConfigureTenantManagement();

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
            b.HasIndex(x => x.LoaiVuViec);
            b.Property(x => x.MaHoSo).IsRequired().HasColumnName("ma_ho_so").HasMaxLength(ComplainConsts.MaxMaHoSoLength);
            b.Property(x => x.LoaiVuViec).IsRequired().HasColumnName("loai_vu_viec");
            b.Property(x => x.TieuDe).IsRequired().HasColumnName("tieu_de").HasMaxLength(ComplainConsts.MaxTieuDeLength);
            b.Property(x => x.NguoiDeNghi).IsRequired().HasColumnName("nguoi_de_nghi").HasMaxLength(ComplainConsts.MaxNguoiDeNghiLength);
            b.Property(x => x.CccdCmnd).IsRequired().HasColumnName("cccd_cmnd").HasMaxLength(ComplainConsts.MaxCccdCmndLength);
            b.Property(x => x.NgayCapCccdCmnd).IsRequired().HasColumnName("ngay_cap_cccd_cmnd");
            b.Property(x => x.NoiCapCccdCmnd).IsRequired().HasColumnName("noi_cap_cccd_cmnd").HasMaxLength(ComplainConsts.MaxNoiCapCccdCmnd);
            b.Property(x => x.NgaySinh).IsRequired().HasColumnName("ngay_sinh");
            b.Property(x => x.DienThaoi).IsRequired().HasColumnName("dien_thoai").HasMaxLength(ComplainConsts.MaxSDTLength);
            b.Property(x => x.Email).HasColumnName("email").HasMaxLength(ComplainConsts.MaxEmailLength);
            b.Property(x => x.DiaChiThuongTru).IsRequired().HasColumnName("dia_chi_thuong_tru").HasMaxLength(ComplainConsts.MaxDiaChiLength);
            b.Property(x => x.DiaChiLienHe).IsRequired().HasColumnName("dia_chi_lien_he").HasMaxLength(ComplainConsts.MaxDiaChiLength);
            b.Property(x => x.MaTinhTP).IsRequired().HasColumnName("ma_tinh_tp").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);
            b.Property(x => x.MaQuanHuyen).IsRequired().HasColumnName("ma_quan_huyen").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);
            b.Property(x => x.MaXaPhuongTT).IsRequired().HasColumnName("ma_xa_phuong_tt").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);
            b.Property(x => x.NgayTiepNhan).IsRequired().HasColumnName("ngay_tiep_nhan");
            b.Property(x => x.NgayHenTraKQ).IsRequired().HasColumnName("ngay_hen_tra_kq");

            b.Property(x => x.NoiDungVuViec).IsRequired().HasColumnName("noi_dung_vu_viec");
            b.Property(x => x.SoThua).IsRequired().HasColumnName("so_thua").HasMaxLength(ComplainConsts.MaxSoThuaLength);
            b.Property(x => x.ToBanDo).IsRequired().HasColumnName("to_ban_do").HasMaxLength(ComplainConsts.MaxToBanDoLength);
            b.Property(x => x.DienTich).IsRequired().HasColumnName("dien_tich").HasMaxLength(ComplainConsts.MaxDienTichLength);
            b.Property(x => x.LoaiDat).IsRequired().HasColumnName("loai_dat").HasMaxLength(ComplainConsts.MaxLoaiDatLength);
            b.Property(x => x.DiaChiThuaDat).IsRequired().HasColumnName("dia_chi_thua_dat").HasMaxLength(ComplainConsts.MaxDiaChiLength);
            b.Property(x => x.TinhThuaDat).IsRequired().IsRequired().HasColumnName("tinh_thua_dat").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);
            b.Property(x => x.HuyenThuaDat).IsRequired().HasColumnName("huyen_thua_dat").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);
            b.Property(x => x.XaThuaDat).IsRequired().HasColumnName("xa_thua_dat").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);


            b.Property(x => x.ngayKhieuNaiI).HasColumnName("ngay_khieu_nai_I");
            b.Property(x => x.NgayTraKQI).HasColumnName("ngay_tra_kq_I");
            b.Property(x => x.ThamQuyenI).HasColumnName("tham_quyen_I").HasMaxLength(ComplainConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQDI).HasColumnName("so_qd_I").HasMaxLength(ComplainConsts.MaxSoQDLength);
            b.Property(x => x.KetQuaI).HasColumnName("ket_qua_I");

            b.Property(x => x.ngayKhieuNaiII).HasColumnName("ngay_khieu_nai_II");
            b.Property(x => x.NgayTraKQII).HasColumnName("ngay_tra_kq_II");
            b.Property(x => x.ThamQuyenII).HasColumnName("tham_quyen_II").HasMaxLength(ComplainConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQDII).HasColumnName("so_qd_II").HasMaxLength(ComplainConsts.MaxSoQDLength);
            b.Property(x => x.KetQuaII).HasColumnName("ket_qua_II");

            b.Property(x => x.DuLieuToaDo).HasColumnName("du_lieu_toa_do").HasMaxLength(ComplainConsts.MaxToaDoLength);
            b.Property(x => x.DuLieuHinhHoc).HasColumnName("du_lieu_hinh_hoc").HasMaxLength(ComplainConsts.MaxHinhHocLength);

            b.Property(x => x.GhiChu).HasColumnName("ghi_chu").HasMaxLength(ComplainConsts.MaxGhiChuLength);
            b.Property(x => x.KetQua).HasColumnName("ket_qua");


            b.HasMany(h => h.FileAttachments)
             .WithOne(k => k.Complain)
             .HasForeignKey(k => k.IdHoSo)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Denounce>(b =>
        {
            b.ToTable("Denounces", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasIndex(x => x.MaHoSo);
            b.HasIndex(x => x.LoaiVuViec);
            b.Property(x => x.MaHoSo).IsRequired().HasColumnName("ma_ho_so").HasMaxLength(DenounceConsts.MaxMaHoSoLength);
            b.Property(x => x.LoaiVuViec).IsRequired().HasColumnName("loai_vu_viec");
            b.Property(x => x.TieuDe).IsRequired().HasColumnName("tieu_de").HasMaxLength(DenounceConsts.MaxTieuDeLength);
            b.Property(x => x.NguoiDeNghi).IsRequired().HasColumnName("nguoi_de_nghi").HasMaxLength(DenounceConsts.MaxNguoiDeNghiLength);
            b.Property(x => x.CccdCmnd).IsRequired().HasColumnName("cccd_cmnd").HasMaxLength(DenounceConsts.MaxCccdCmndLength);
            b.Property(x => x.NgayCapCccdCmnd).IsRequired().HasColumnName("ngay_cap_cccd_cmnd");
            b.Property(x => x.NoiCapCccdCmnd).IsRequired().HasColumnName("noi_cap_cccd_cmnd").HasMaxLength(DenounceConsts.MaxNoiCapCccdCmnd);
            b.Property(x => x.NgaySinh).IsRequired().HasColumnName("ngay_sinh");
            b.Property(x => x.DienThaoi).IsRequired().HasColumnName("dien_thoai").HasMaxLength(DenounceConsts.MaxSDTLength);
            b.Property(x => x.Email).HasColumnName("email").HasMaxLength(DenounceConsts.MaxEmailLength);
            b.Property(x => x.DiaChiThuongTru).IsRequired().HasColumnName("dia_chi_thuong_tru").HasMaxLength(DenounceConsts.MaxDiaChiLength);
            b.Property(x => x.DiaChiLienHe).IsRequired().HasColumnName("dia_chi_lien_he").HasMaxLength(DenounceConsts.MaxDiaChiLength);
            b.Property(x => x.MaTinhTP).IsRequired().HasColumnName("ma_tinh_tp").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);
            b.Property(x => x.MaQuanHuyen).IsRequired().HasColumnName("ma_quan_huyen").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);
            b.Property(x => x.MaXaPhuongTT).IsRequired().HasColumnName("ma_xa_phuong_tt").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);
            b.Property(x => x.NgayTiepNhan).IsRequired().HasColumnName("ngay_tiep_nhan");
            b.Property(x => x.NgayHenTraKQ).IsRequired().HasColumnName("ngay_hen_tra_kq");

            b.Property(x => x.NoiDungVuViec).IsRequired().HasColumnName("noi_dung_vu_viec");
            b.Property(x => x.SoThua).IsRequired().HasColumnName("so_thua").HasMaxLength(DenounceConsts.MaxSoThuaLength);
            b.Property(x => x.ToBanDo).IsRequired().HasColumnName("to_ban_do").HasMaxLength(DenounceConsts.MaxToBanDoLength);
            b.Property(x => x.DienTich).IsRequired().HasColumnName("dien_tich").HasMaxLength(DenounceConsts.MaxDienTichLength);
            b.Property(x => x.LoaiDat).IsRequired().HasColumnName("loai_dat").HasMaxLength(DenounceConsts.MaxLoaiDatLength);
            b.Property(x => x.DiaChiThuaDat).IsRequired().HasColumnName("dia_chi_thua_dat").HasMaxLength(DenounceConsts.MaxDiaChiLength);
            b.Property(x => x.TinhThuaDat).IsRequired().IsRequired().HasColumnName("tinh_thua_dat").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);
            b.Property(x => x.HuyenThuaDat).IsRequired().HasColumnName("huyen_thua_dat").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);
            b.Property(x => x.XaThuaDat).IsRequired().HasColumnName("xa_thua_dat").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);


            b.Property(x => x.ngayKhieuNaiI).HasColumnName("ngay_khieu_nai_I");
            b.Property(x => x.NgayTraKQI).HasColumnName("ngay_tra_kq_I");
            b.Property(x => x.ThamQuyenI).HasColumnName("tham_quyen_I").HasMaxLength(DenounceConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQDI).HasColumnName("so_qd_I").HasMaxLength(DenounceConsts.MaxSoQDLength);
            b.Property(x => x.KetQuaI).HasColumnName("ket_qua_I");

            b.Property(x => x.ngayKhieuNaiII).HasColumnName("ngay_khieu_nai_II");
            b.Property(x => x.NgayTraKQII).HasColumnName("ngay_tra_kq_II");
            b.Property(x => x.ThamQuyenII).HasColumnName("tham_quyen_II").HasMaxLength(DenounceConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQDII).HasColumnName("so_qd_II").HasMaxLength(DenounceConsts.MaxSoQDLength);
            b.Property(x => x.KetQuaII).HasColumnName("ket_qua_II");

            b.Property(x => x.DuLieuToaDo).HasColumnName("du_lieu_toa_do").HasMaxLength(DenounceConsts.MaxToaDoLength);
            b.Property(x => x.DuLieuHinhHoc).HasColumnName("du_lieu_hinh_hoc").HasMaxLength(DenounceConsts.MaxHinhHocLength);

            b.Property(x => x.GhiChu).HasColumnName("ghi_chu").HasMaxLength(DenounceConsts.MaxGhiChuLength);
            b.Property(x => x.KetQua).HasColumnName("ket_qua");


            b.HasMany(h => h.FileAttachments)
             .WithOne(k => k.Denounce)
             .HasForeignKey(k => k.IdHoSo)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<FileAttachment>(b =>
        {
            b.ToTable("FileAttachments", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasIndex(x => x.IdHoSo);
            b.Property(x => x.IdHoSo).IsRequired().HasColumnName("id_ho_so");
            b.Property(x => x.TenTaiLieu).IsRequired().HasColumnName("ten_tai_lieu").HasMaxLength(FileAttachmentConsts.MaxTenTaiLieuLength);
            b.Property(x => x.HinhThuc).IsRequired().HasColumnName("hinh_thuc").HasMaxLength(FileAttachmentConsts.MaxHinhThucLength);
            b.Property(x => x.ThoiGianBanHanh).HasColumnName("thoi_gian_ban_hanh");
            b.Property(x => x.NgayNhan).HasColumnName("ngay_nhan");
            b.Property(x => x.ThuTuButLuc).IsRequired().HasColumnName("thu_tu_but_luc").HasMaxLength(FileAttachmentConsts.MaxThuTuButLucLength);
            b.Property(x => x.NoiDungChinh).HasColumnName("noi_dung_chinh");
            b.Property(x => x.FileName).IsRequired().HasColumnName("file_name").HasMaxLength(FileAttachmentConsts.MaxFileNameLength);
            b.Property(x => x.ContentType).IsRequired().HasColumnName("content_type").HasMaxLength(FileAttachmentConsts.MaxContenTypeLength);
            b.Property(x => x.ContentLength).IsRequired().HasColumnName("content_length");
        });
    }
}