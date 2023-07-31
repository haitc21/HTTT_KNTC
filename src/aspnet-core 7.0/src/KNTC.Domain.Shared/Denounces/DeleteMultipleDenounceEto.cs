using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;

namespace KNTC.Denounces;

[EventName("DeleteMultipleDenounce")]
public class DeleteMultipleDenounceEto
{
	public DeleteMultipleDenounceEto()
	{

	}
	public DeleteMultipleDenounceEto(List<Guid> ids)
	{
		Ids= ids;
	}
	public List<Guid> Ids { get; set; }
}

