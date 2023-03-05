using AutoMapper;
using Volo.Abp.Identity;
using KNTC.Roles;
using KNTC.Users;
using KNTC.HoSos;
using Volo.Abp.AutoMapper;

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


        CreateMap<HoSo, HoSoDto>();
        CreateMap<KQGQHoSo, KQGQHoSoDto>();
        CreateMap<CreateAndUpdateKQGQHoSoDto, KQGQHoSo>();
        CreateMap<TepDinhKemHoSo, TepDinhKemHoSoDto>();
        CreateMap<CreateAndUpdateTepDinhKemHoSoDto, TepDinhKemHoSo>();

    }
}