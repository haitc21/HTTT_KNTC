using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNTC.Common;

public interface IDropdownAppService
{
    Task<List<DropdownItem>> GetDropdownItems();
}
public interface IDropdownAppService<TId> where TId : struct
{
    Task<List<DropdownItem<TId>>> GetDropdownItems();
}
public interface IDropdownValueAppService
{
    Task<List<DropdownItemValue>> GetDropdownItemValues();
}
public interface IDropdownValueAppService<TId> where TId : struct
{
    Task<List<DropdownItemValue<TId>>> GetDropdownItemValues();
}
public interface IDropdownValueAppService<TId, TVal> where TId : struct where TVal : struct
{
    Task<List<DropdownItemValue<TId, TVal>>> GetDropdownItemValues();
}

public interface IDropdownItem
{
    string Code { get; set; }
    string Name { get; set; }
    bool Selected { get; set; }
    bool Hidden { get; set; }
}
