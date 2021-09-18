using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication4.Controllers
{
    //[ApiController]
    //[Route("[StatisticController]")]
    public class StatisticController : ControllerBase
    {
        private readonly IStatistic _statisticHandler;

        public StatisticController(IStatistic statisticHandler)
        {
            _statisticHandler = statisticHandler;
        }

        [HttpGet]
        [Route("Types")]
        public IEnumerable<TypeStatistic> GetStatisticOnTypes()
        {
            return _statisticHandler.GetStatisticOnTypes();
        }

        [HttpGet]
        [Route("Statuses")]
        public IEnumerable<StatusStatistic> GetStatisticOnStatuses()
        {
            return _statisticHandler.GetStatisticOnStatuses();
        }

        [HttpGet]
        [Route("Marques")]
        public IEnumerable<MarqueStatistic> GetStatisticOnMarques()
        {
            return _statisticHandler.GetStatisticOnMarques();
        }

        [HttpGet]
        [Route("GeneralStatistic")]
        public GeneralStatistic GetGeneralStatistic()
        {
            return _statisticHandler.GetGeneralStatistic();
        }
    }
}
