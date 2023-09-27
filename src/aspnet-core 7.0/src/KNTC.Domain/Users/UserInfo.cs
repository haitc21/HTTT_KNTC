using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KNTC.Users;

public class UserInfo : Entity<Guid>
{
    public UserInfo(Guid id) : base(id)
    {
    }

    public Guid UserId { get; set; }    
    public DateTime Dob { get; set; }
    //Loại user -> Cũng là cấp độ quản lý
    public UserType? UserType { get; set; }
    //Danh sách các đơn vị được quản lý, tùy thuộc vào cấp độ quản lý
    public int[]? ManagedUnitIds { get; set; }
}