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
            //b.Property(x => x.NgayCapCccdCmnd).IsRequired().HasColumnName("ngay_cap_cccd_cmnd");
            //b.Property(x => x.NoiCapCccdCmnd).IsRequired().HasColumnName("noi_cap_cccd_cmnd").HasMaxLength(ComplainConsts.MaxNoiCapCccdCmnd);
            b.Property(x => x.NgaySinh).IsRequired().HasColumnName("ngay_sinh");
            b.Property(x => x.DienThoai).IsRequired().HasColumnName("dien_thoai").HasMaxLength(ComplainConsts.MaxSDTLength);
            b.Property(x => x.Email).HasColumnName("email").HasMaxLength(ComplainConsts.MaxEmailLength);
            b.Property(x => x.DiaChiThuongTru).IsRequired().HasColumnName("dia_chi_thuong_tru").HasMaxLength(ComplainConsts.MaxDiaChiLength);
            b.Property(x => x.DiaChiLienHe).IsRequired().HasColumnName("dia_chi_lien_he").HasMaxLength(ComplainConsts.MaxDiaChiLength);
            b.Property(x => x.maTinhTP).IsRequired().HasColumnName("ma_tinh_tp").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);
            b.Property(x => x.maQuanHuyen).IsRequired().HasColumnName("ma_quan_huyen").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);
            b.Property(x => x.maXaPhuongTT).IsRequired().HasColumnName("ma_xa_phuong_tt").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);
            b.Property(x => x.ThoiGianTiepNhan).IsRequired().HasColumnName("thoi_gian_tiep_nhan");
            b.Property(x => x.ThoiGianHenTraKQ).IsRequired().HasColumnName("thoi_gian_hen_tra_kq");
            b.Property(x => x.NoiDungVuViec).IsRequired().HasColumnName("noi_dung_vu_viec");
            b.Property(x => x.boPhanDangXL).IsRequired().HasColumnName("bo_phan_dang_xl").HasMaxLength(ComplainConsts.MaxBoPhanXLLength);

            b.Property(x => x.SoThua).IsRequired().HasColumnName("so_thua").HasMaxLength(ComplainConsts.MaxSoThuaLength);
            b.Property(x => x.ToBanDo).IsRequired().HasColumnName("to_ban_do").HasMaxLength(ComplainConsts.MaxToBanDoLength);
            b.Property(x => x.DienTich).IsRequired().HasColumnName("dien_tich");
            b.Property(x => x.LoaiDat).IsRequired().HasColumnName("loai_dat").HasMaxLength(ComplainConsts.MaxLoaiDatLength);
            b.Property(x => x.DiaChiThuaDat).IsRequired().HasColumnName("dia_chi_thua_dat").HasMaxLength(ComplainConsts.MaxDiaChiLength);
            b.Property(x => x.tinhThuaDat).IsRequired().IsRequired().HasColumnName("tinh_thua_dat").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);
            b.Property(x => x.huyenThuaDat).IsRequired().HasColumnName("huyen_thua_dat").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);
            b.Property(x => x.xaThuaDat).IsRequired().HasColumnName("xa_thua_dat").HasMaxLength(ComplainConsts.MaxMaDiaDanhLength);

            b.Property(x => x.loaiKhieuNai1).HasColumnName("loai_khieu_nai_1");
            b.Property(x => x.ngayKhieuNai1).HasColumnName("ngay_khieu_nai_1");
            b.Property(x => x.NgayTraKQ1).HasColumnName("ngay_tra_kq_1");
            b.Property(x => x.ThamQuyen1).HasColumnName("tham_quyen_1").HasMaxLength(ComplainConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQD1).HasColumnName("so_qd_1").HasMaxLength(ComplainConsts.MaxSoQDLength);
            b.Property(x => x.KetQua1).HasColumnName("ket_qua_1");

            b.Property(x => x.loaiKhieuNai2).HasColumnName("loai_khieu_nai_2");
            b.Property(x => x.ngayKhieuNai2).HasColumnName("ngay_khieu_nai_2");
            b.Property(x => x.NgayTraKQ2).HasColumnName("ngay_tra_kq_2");
            b.Property(x => x.ThamQuyen2).HasColumnName("tham_quyen_2").HasMaxLength(ComplainConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQD2).HasColumnName("so_qd_2").HasMaxLength(ComplainConsts.MaxSoQDLength);
            b.Property(x => x.KetQua2).HasColumnName("ket_qua_2");

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
            //b.Property(x => x.NgayCapCccdCmnd).IsRequired().HasColumnName("ngay_cap_cccd_cmnd");
            //b.Property(x => x.NoiCapCccdCmnd).IsRequired().HasColumnName("noi_cap_cccd_cmnd").HasMaxLength(DenounceConsts.MaxNoiCapCccdCmnd);
            b.Property(x => x.NgaySinh).IsRequired().HasColumnName("ngay_sinh");
            b.Property(x => x.DienThoai).IsRequired().HasColumnName("dien_thoai").HasMaxLength(DenounceConsts.MaxSDTLength);
            b.Property(x => x.Email).HasColumnName("email").HasMaxLength(DenounceConsts.MaxEmailLength);
            b.Property(x => x.DiaChiThuongTru).IsRequired().HasColumnName("dia_chi_thuong_tru").HasMaxLength(DenounceConsts.MaxDiaChiLength);
            b.Property(x => x.DiaChiLienHe).IsRequired().HasColumnName("dia_chi_lien_he").HasMaxLength(DenounceConsts.MaxDiaChiLength);
            b.Property(x => x.maTinhTP).IsRequired().HasColumnName("ma_tinh_tp").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);
            b.Property(x => x.maQuanHuyen).IsRequired().HasColumnName("ma_quan_huyen").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);
            b.Property(x => x.maXaPhuongTT).IsRequired().HasColumnName("ma_xa_phuong_tt").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);
            b.Property(x => x.ThoiGianTiepNhan).IsRequired().HasColumnName("thoi_gian_tiep_nhan");
            b.Property(x => x.ThoiGianHenTraKQ).IsRequired().HasColumnName("thoi_gian_hen_tra_kq");
            b.Property(x => x.NoiDungVuViec).IsRequired().HasColumnName("noi_dung_vu_viec");
            b.Property(x => x.boPhanDangXL).IsRequired().HasColumnName("bo_phan_dang_xl").HasMaxLength(DenounceConsts.MaxBoPhanXLLength);

            b.Property(x => x.SoThua).IsRequired().HasColumnName("so_thua").HasMaxLength(DenounceConsts.MaxSoThuaLength);
            b.Property(x => x.ToBanDo).IsRequired().HasColumnName("to_ban_do").HasMaxLength(DenounceConsts.MaxToBanDoLength);
            b.Property(x => x.DienTich).IsRequired().HasColumnName("dien_tich");
            b.Property(x => x.LoaiDat).IsRequired().HasColumnName("loai_dat").HasMaxLength(DenounceConsts.MaxLoaiDatLength);
            b.Property(x => x.DiaChiThuaDat).IsRequired().HasColumnName("dia_chi_thua_dat").HasMaxLength(DenounceConsts.MaxDiaChiLength);
            b.Property(x => x.tinhThuaDat).IsRequired().IsRequired().HasColumnName("tinh_thua_dat").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);
            b.Property(x => x.huyenThuaDat).IsRequired().HasColumnName("huyen_thua_dat").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);
            b.Property(x => x.xaThuaDat).IsRequired().HasColumnName("xa_thua_dat").HasMaxLength(DenounceConsts.MaxMaDiaDanhLength);


            b.Property(x => x.ngayKhieuNai1).HasColumnName("ngay_khieu_nai_1");
            b.Property(x => x.NgayTraKQ1).HasColumnName("ngay_tra_kq_1");
            b.Property(x => x.ThamQuyen1).HasColumnName("tham_quyen_1").HasMaxLength(DenounceConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQD1).HasColumnName("so_qd_1").HasMaxLength(DenounceConsts.MaxSoQDLength);
            b.Property(x => x.KetQua1).HasColumnName("ket_qua_1");

            b.Property(x => x.ngayKhieuNai2).HasColumnName("ngay_khieu_nai_2");
            b.Property(x => x.NgayTraKQ2).HasColumnName("ngay_tra_kq_2");
            b.Property(x => x.ThamQuyen2).HasColumnName("tham_quyen_2").HasMaxLength(DenounceConsts.MaxThamQuyenLength);
            b.Property(x => x.SoQD2).HasColumnName("so_qd_2").HasMaxLength(DenounceConsts.MaxSoQDLength);
            b.Property(x => x.KetQua2).HasColumnName("ket_qua_2");

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
            b.Property(x => x.GiaiDoan).IsRequired().HasColumnName("giai_doan");
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