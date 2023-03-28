using AutoMapper;
using KNTC.Complains;
using KNTC.Configs;
using KNTC.Denounces;
using KNTC.DocumentTypes;
using KNTC.FileAttachments;
using KNTC.LandTypes;
using KNTC.Roles;
using KNTC.SpatialDatas;
using KNTC.Summaries;
using KNTC.Units;
using KNTC.UnitTypes;
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
        CreateMap<Complain, ComplainExcelDto>();
        CreateMap<CreateComplainDto, Complain>();
        CreateMap<UpdateComplainDto, Complain>();

        CreateMap<Denounce, DenounceDto>();
        CreateMap<Denounce, UnitTypeListDto>();
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


    }
}