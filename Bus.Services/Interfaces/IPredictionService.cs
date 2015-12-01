using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Services.Interfaces
{
    public interface IPredictionService
    {
        int QuantityPredict(string programCode, List<string> timesFrame);
    }
}
