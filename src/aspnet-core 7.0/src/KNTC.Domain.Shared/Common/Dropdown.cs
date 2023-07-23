namespace KNTC.Common;

public class DropdownItem : IDropdownItem
{
    public string Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool Selected { get; set; }
    public bool Hidden { get; set; }
}

public class DropdownItem<TId> : IDropdownItem where TId : struct
{
    public TId Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool Selected { get; set; }
    public bool Hidden { get; set; }
}

public class DropdownItemValue : IDropdownItem
{
    public string Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public bool Selected { get; set; }
    public bool Hidden { get; set; }
}

public class DropdownItemValue<TVal> : IDropdownItem where TVal : struct
{
    public string Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public TVal Value { get; set; }
    public bool Selected { get; set; }
    public bool Hidden { get; set; }
}

public class DropdownItemValue<TId, TVal> : IDropdownItem where TId : struct where TVal : struct
{
    public TId Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public TVal Value { get; set; }
    public bool Selected { get; set; }
    public bool Hidden { get; set; }
}