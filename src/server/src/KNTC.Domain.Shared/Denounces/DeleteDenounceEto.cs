using System;
using Volo.Abp.EventBus;

namespace KNTC.Denounces;

[EventName("DeleteDenounce")]
public class DeleteDenounceEto
{
    public DeleteDenounceEto()
    {
    }

    public DeleteDenounceEto(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}