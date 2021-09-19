using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication4.Controllers
{
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsHandler _statisticsHandler;

        public StatisticsController(IStatisticsHandler statisticsHandler)
        {
            _statisticsHandler = statisticsHandler;
        }

        [HttpGet]
        [Route("GetTypes")]
        public IEnumerable<TypeStatistics> GetStatisticsOnTypes()
        {
            return _statisticsHandler.GetStatisticsOnTypes();
        }

        [HttpGet]
        [Route("GetStatuses")]
        public IEnumerable<StatusStatistics> GetStatisticsOnStatuses()
        {
            return _statisticsHandler.GetStatisticsOnStatuses();
        }

        [HttpGet]
        [Route("GetMarques")]
        public IEnumerable<MarqueStatistics> GetStatisticsOnMarques()
        {
            return _statisticsHandler.GetStatisticsOnMarques();
        }

        [HttpGet]
        [Route("GetGeneralStatistics")]
        public GeneralStatistics GetGeneralStatistics()
        {
            return _statisticsHandler.GetGeneralStatistics();
        }
    }
}
