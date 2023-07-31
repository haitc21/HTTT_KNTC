using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;

namespace KNTC.Complains;

[EventName("DeleteMultipleComplain")]
public class DeleteMultipleComplainEto
{
	public DeleteMultipleComplainEto()
	{

	}
	public DeleteMultipleComplainEto(List<Guid> ids)
	{
		Ids= ids;
	}
    public List<Guid> Ids { get; set; }
}
