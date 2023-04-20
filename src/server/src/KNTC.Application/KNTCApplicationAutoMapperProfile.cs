using AutoMapper;
using KNTC.Complains;
using KNTC.Configs;
using KNTC.Denounces;
using KNTC.DocumentTypes;
using KNTC.Extenssions;
using KNTC.FileAttachments;
using KNTC.LandTypes;
using KNTC.Roles;
using KNTC.SpatialDatas;
using KNTC.Summaries;
using KNTC.Units;
using KNTC.CategoryUnitTypes;
using KNTC.Users;
using Volo.Abp.Identity;

namespace KNTC;

public class KNTCApplicationAutoMapperProfile : Profile
{
    public KNTCApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        //Roles
        CreateMap<IdentityRole, RoleDto>().ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ?
            x.ExtraProperties[RoleConsts.DescriptionFieldName]
            :
            null));
        CreateMap<CreateUpdateRoleDto, IdentityRole>();
        CreateMap<IdentityRole, RoleLookupDto>();
        //User
        CreateMap<IdentityUser, UserDto>();
        CreateMap<IdentityUser, UserListDto>();
        CreateMap<UserInfo, UserInfoDto>();
        CreateMap<CrateAndUpdateUserDto, IdentityUser>();
        CreateMap<CrateAndUpdateUserDto, UserInfo>();
        CreateMap<IdentityUser, UserInfo>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));


        CreateMap<Complain, ComplainDto>();
        CreateMap<Complain, ComplainInListDto>();
        CreateMap<Complain, ComplainExcelDto>()
                 .ForMember(dto => dto.KetQua1, opt => opt.MapFrom(c => c.KetQua1.HasValue ? c.KetQua1.Value.ToVNString() : string.Empty))
                 .ForMember(dto => dto.KetQua2, opt => opt.MapFrom(c => c.KetQua2.HasValue ? c.KetQua2.Value.ToVNString() : string.Empty))
                 .ForMember(dto => dto.KetQua, opt => opt.MapFrom(c => c.KetQua.HasValue ? c.KetQua.Value.ToVNString() : string.Empty));
        CreateMap<CreateComplainDto, Complain>();
        CreateMap<UpdateComplainDto, Complain>();

        CreateMap<Denounce, DenounceDto>();
        CreateMap<Denounce, DenounceInListDto>();
        CreateMap<Denounce, DenounceExcelDto>()
                 .ForMember(dto => dto.KetQua, opt => opt.MapFrom(denounce => denounce.KetQua.Value.ToVNString()));
        CreateMap<CreateDenounceDto, Denounce>();
        CreateMap<UpdateDenounceDto, Denounce>();

        CreateMap<FileAttachment, FileAttachmentDto>();
        CreateMap<CreateAndUpdateFileAttachmentDto, FileAttachment>();

        CreateMap<DocumentType, DocumentTypeDto>();
        CreateMap<DocumentType, DocumentTypeLookupDto>();
        CreateMap<CreateAndUpdateDocumentTypeDto, DocumentType>();

        CreateMap<LandType, LandTypeDto>();
        CreateMap<LandType, LandTypeLookupDto>();
        CreateMap<CreateAndUpdateLandTypeDto, LandType>();

        CreateMap<Unit, UnitDto>();
        CreateMap<Unit, UnitLookupDto>();
        CreateMap<CreateAndUpdateUnitDto, Unit>();

        CreateMap<UnitType, UnitTypeDto>();
        CreateMap<UnitType, UnitTypeLookupDto>();
        CreateMap<CreateAndUpdateUnitTypeDto, UnitType>();

        CreateMap<Config, ConfigDto>();
        CreateMap<Config, ConfigLookupDto>();
        CreateMap<CreateAndUpdateConfigDto, Config>();

        CreateMap<SpatialData, SpatialDataDto>();
        CreateMap<SpatialData, SpatialDataLookupDto>();
        CreateMap<CreateAndUpdateSpatialDataDto, SpatialData>();

        CreateMap<Summary, SummaryDto>();
        CreateMap<Summary, SummaryMapDto>();
        CreateMap<Summary, SummaryExcelDto>()
            .ForMember(dto => dto.LoaiVuViec, opt => opt.MapFrom(c => c.LoaiVuViec.ToVNString()))
            .ForMember(dto => dto.LinhVuc, opt => opt.MapFrom(c => c.LinhVuc.ToVNString()))
            .ForMember(dto => dto.KetQua, opt => opt.MapFrom(c => c.KetQua.HasValue ? c.KetQua.Value.ToVNString() : string.Empty));
    }
}