using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
        IScheduleRepository _iScheduleRepository;
        public PredictionService(IProgramRepository iProgramRepository, IScheduleRepository iScheduleRepository)
        {
            _iProgramRepository = iProgramRepository;
            _iScheduleRepository = iScheduleRepository;
        }
        public int QuantityPredict(string programCode, int noTimes)
        {
            var program = _iProgramRepository.All.Where(x=>x.ProgramCode == programCode).FirstOrDefault();
            //if (program != null)
            //{
            //    var programGroup = _iProgramRepository.All.Where(x => x.ProductId == program.ProductId).ToList();
            //}
            var scheduleDay = _iScheduleRepository.All.Where(x => x.ProgramCode == program.ProgramCode).GroupBy(x => EntityFunctions.TruncateTime(x.Date)).ToList().Where(x=>x.Count() == noTimes);


            return scheduleDay.Count();
        }
    }
}
