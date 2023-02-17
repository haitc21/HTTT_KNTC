using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace WebBase.Users;

public class UserInfo : FullAuditedAggregateRoot<Guid>
{
    public UserInfo(Guid id) : base(id)
    {
            
    }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Dob { get; set; }
}
