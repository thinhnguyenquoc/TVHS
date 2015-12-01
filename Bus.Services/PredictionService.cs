using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVHS.Repositories.Interfaces;
using TVHS.Services.Interfaces;

namespace TVHS.Services
{
    public class PredictionService : IPredictionService
    {
        IProgramRepository _iProgramRepository;
        public PredictionService(IProgramRepository iProgramRepository)
        {
            _iProgramRepository = iProgramRepository;
        }
        public int QuantityPredict(string programCode, List<string> timesFrame)
        {
            var program = _iProgramRepository.All.Where(x=>x.ProgramCode == programCode).FirstOrDefault();
            if (program != null)
            {
                var programGroup = _iProgramRepository.All.Where(x => x.ProductId == program.ProductId).ToList();
            }
            return 1;
        }
    }
}
