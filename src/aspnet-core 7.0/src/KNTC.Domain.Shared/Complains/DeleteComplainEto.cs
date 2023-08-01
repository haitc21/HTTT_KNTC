﻿using System;
using Volo.Abp.EventBus;

namespace KNTC.Complains;

[EventName("DeleteComplain")]
public class DeleteComplainEto
{
    public DeleteComplainEto()
    {
    }

    public DeleteComplainEto(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}