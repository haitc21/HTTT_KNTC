using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;

namespace KNTC.Users;

public class CreateAndUpdateUserDto : IdentityUserUpdateDto
{
    public DateTime? Dob { get; set; }
    [Required]
    public UserType UserType { get; set; }
    public int[]? ManagedUnitIds { get; set; }
}