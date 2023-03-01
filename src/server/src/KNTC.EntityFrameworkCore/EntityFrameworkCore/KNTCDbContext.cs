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
using KNTC.HoSos;
using Newtonsoft.Json.Linq;
using System.Reflection.Emit;

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
    public DbSet<HoSo> HoSo { get; set; }
    public DbSet<KQGQHoSo> KQGQHoSos { get; set; }
    public DbSet<TepDinhKemHoSo> TepDinhKemHoSos { get; set; }


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

        builder.Entity<HoSo>(b =>
        {
            b.ToTable("ho_so", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasIndex(x => x.MaHoSo);
            b.Property(x => x.MaHoSo).IsRequired().HasColumnName("ma_ho_so").HasMaxLength(HoSoConsts.MaxCodeLength);
            b.Property(x => x.TieuDe).IsRequired().HasColumnName("tieu_de").HasMaxLength(HoSoConsts.MaxTieuDeLength);
            b.Property(x => x.NguoiDeNghi).IsRequired().HasColumnName("nguoi_de_nghi");
            b.Property(x => x.CccdCmnd).IsRequired().HasColumnName("cccd_cmnd").HasMaxLength(HoSoConsts.MaxCccdCmndLength);
            b.Property(x => x.NgayCapCccdCmnd).IsRequired().HasColumnName("ngay_cap_cccd_cmnd");
            b.Property(x => x.NoiCapCccdCmnd).IsRequired().HasColumnName("noi_cap_cccd_cmnd").HasMaxLength(HoSoConsts.MaxNoiCapCccdCmnd);
            b.Property(x => x.NgaySinh).IsRequired().HasColumnName("ngay_sinh");
            b.Property(x => x.DienThaoi).IsRequired().HasColumnName("dien_thoai").HasMaxLength(HoSoConsts.MaxSDTLength);
            b.Property(x => x.Email).IsRequired().HasColumnName("email").HasMaxLength(HoSoConsts.MaxEmailLength);
            b.Property(x => x.DiaChioThuongTru).IsRequired().HasColumnName("dia_chi_thuong_tru").HasMaxLength(HoSoConsts.MaxDiaChiLength);
            b.Property(x => x.DiaChiLienHe).IsRequired().HasColumnName("dia_chi_lien_he").HasMaxLength(HoSoConsts.MaxDiaChiLength);
            b.Property(x => x.MaTinhTP).IsRequired().HasColumnName("ma_tinh_tp").HasMaxLength(HoSoConsts.MaxMaDiaDanhLength);
            b.Property(x => x.MaQuanHuyen).IsRequired().HasColumnName("ma_quan_huyen").HasMaxLength(HoSoConsts.MaxMaDiaDanhLength);
            b.Property(x => x.MaXaPhuongTT).IsRequired().HasColumnName("ma_xa_phuong_tt").HasMaxLength(HoSoConsts.MaxMaDiaDanhLength);
           
            b.Property(x => x.LoaiVuViec).IsRequired().HasColumnName("loai_vu_viec");
            b.Property(x => x.LinhVuc).IsRequired().HasColumnName("linh_vuc");
            b.Property(x => x.NgayTiepNhan).HasColumnName("ngay_tiep_nhan");
            b.Property(x => x.NgayHenTraKQ).HasColumnName("ngay_hen_tra_kq");
            b.Property(x => x.NoiDungVuViec).IsRequired().HasColumnName("noi_dung_vu_viec");
            b.Property(x => x.SoThua).HasColumnName("so_thua").HasMaxLength(HoSoConsts.MáoThuaLength);
            b.Property(x => x.ToBanDo).HasColumnName("to_ban_do").HasMaxLength(HoSoConsts.MaxToBanDoLength);
            b.Property(x => x.DienTich).HasColumnName("dien_tich").HasMaxLength(HoSoConsts.MaxDienTichLength);
            b.Property(x => x.LoaiDat).HasColumnName("loai_dat").HasMaxLength(HoSoConsts.MaxLoaiDatLength);
            b.Property(x => x.DiaChiThuaDat).IsRequired().HasColumnName("dia_chi_thua_dat").HasMaxLength(HoSoConsts.MaxDiaChiLength);
            b.Property(x => x.TinhThuaDat).IsRequired().HasColumnName("tinh_thua_dat").HasMaxLength(HoSoConsts.MaxMaDiaDanhLength);
            b.Property(x => x.HuyenThuaDat).IsRequired().HasColumnName("huyen_thua_dat").HasMaxLength(HoSoConsts.MaxMaDiaDanhLength);
            b.Property(x => x.XaThuaDat).IsRequired().HasColumnName("xa_thua_dat").HasMaxLength(HoSoConsts.MaxMaDiaDanhLength);

            b.Property(x => x.DuLieuToaDo).HasColumnName("du_lieu_toa_do").HasMaxLength(HoSoConsts.MaxToaDoLength);
            b.Property(x => x.DuLieuHinhHoc).HasColumnName("du_lieu_hinh_hoc").HasMaxLength(HoSoConsts.MaxHinhHocLength);

            b.Property(x => x.SoLanTraKQ).HasColumnName("so_lan_tra_kq");
            b.Property(x => x.KetQua).HasColumnName("ket_qua");

            b.HasMany(h => h.KQGQHoSos)
             .WithOne(k => k.HoSo)
             .HasForeignKey(k => k.IdHoSo)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(h => h.TepDinhKemHoSos)
             .WithOne(k => k.HoSo)
             .HasForeignKey(k => k.IdHoSo)
             .OnDelete(DeleteBehavior.Cascade);

        });

        builder.Entity<KQGQHoSo>(b =>
        {
            b.ToTable("kqgq_ho_so", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.IdHoSo).IsRequired().HasColumnName("id_ho_so");
            b.Property(x => x.LanGQ).IsRequired().HasColumnName("lan_gq");
            b.Property(x => x.NgayTraKQ).IsRequired().HasColumnName("ngay_tra_kq");
            b.Property(x => x.ThamQuyen).IsRequired().HasColumnName("tham_quyen").HasMaxLength(HoSoConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQD).IsRequired().HasColumnName("so_qd").HasMaxLength(HoSoConsts.MaxSoQDLength);
            b.Property(x => x.GhiChu).IsRequired().HasColumnName("ghi_chu").HasMaxLength(HoSoConsts.MaxGhiChuLength);
            b.Property(x => x.KetQua).HasColumnName("ket_qua");
        });

        builder.Entity<TepDinhKemHoSo>(b =>
        {
            b.ToTable("tep_dinh_kem_ho_so", KNTCConsts.KNTCDbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.IdHoSo).IsRequired().HasColumnName("id_ho_so");
            b.Property(x => x.TenTaiLieu).IsRequired().HasColumnName("ten_tai_lieu").HasMaxLength(HoSoConsts.MaxTenTaiLieuLength);
            b.Property(x => x.HinhThuc).IsRequired().HasColumnName("hinh_thuc").HasMaxLength(HoSoConsts.MaxHinhThuc);
            b.Property(x => x.ThoiGianBanHanh).HasColumnName("thoi_gian_ban_hanh");
            b.Property(x => x.NgayNhan).HasColumnName("ngay_nhan");
            b.Property(x => x.ThuTuButLuc).HasColumnName("thu_tu_but_luc");
            b.Property(x => x.NoiDungChinh).HasColumnName("noi_dung_chinh");
            b.Property(x => x.ContentType).IsRequired().HasColumnName("content_type").HasMaxLength(HoSoConsts.MaxContenTypeLength);
            b.Property(x => x.ContentLength).IsRequired().HasColumnName("content_length");
        });
    }
}