using Core.Models;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IStatisticsHandler
    {
        public IEnumerable<TypeStatistics> GetStatisticsOnTypes();
        public IEnumerable<MarqueStatistics> GetStatisticsOnMarques();
        public IEnumerable<StatusStatistics> GetStatisticsOnStatuses();
        public GeneralStatistics GetGeneralStatistics();
    }
}
