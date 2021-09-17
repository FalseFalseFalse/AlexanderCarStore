using Core.Models;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IStatistic
    {
        public IEnumerable<TypeStatistic> GetStatisticOnTypes();
        public IEnumerable<MarqueStatistic> GetStatisticOnMarques();
        public IEnumerable<StatusStatistic> GetStatisticOnStatuses();
        public GeneralStatistic GetGeneralStatistic();
    }
}
