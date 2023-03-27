using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNTC.Summaries;

public interface ISummaryRepository 
{
    Task<IQueryable<Summary>> GetListAsync(bool landComplain,
                                           bool enviromentComplain,
                                           bool waterComplain,
                                           bool mineralComplain,
                                           bool landDenounce,
                                           bool enviromentDenounce,
                                           bool waterDenounce,
                                           bool mineralDenounce);
}
