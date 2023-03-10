using System;
using Volo.Abp.Domain.Entities;

namespace KNTC.Users;

public class UserInfo : Entity<Guid>
{
    public UserInfo(Guid id) : base(id)
    {

    }
    public Guid UserId { get; set; }
    public DateTime Dob { get; set; }
}
