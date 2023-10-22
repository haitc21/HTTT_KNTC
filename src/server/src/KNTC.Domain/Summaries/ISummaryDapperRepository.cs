using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNTC.Summaries;

public interface ISummaryDapperRepository
{
    Task RefreshView();
}
