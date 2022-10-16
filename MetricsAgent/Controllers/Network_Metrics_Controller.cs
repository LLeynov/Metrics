using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.DTO;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/NETWORK")]
    [ApiController]
    public class Network_Metrics_Controller : ControllerBase
    {
        private readonly ILogger<Network_Metrics_Controller> _logger;
        private readonly INetWorkMetricsRepository _netWorkMetricsRepository;
        private readonly IMapper _mapper;

        public Network_Metrics_Controller(INetWorkMetricsRepository networkMetrics, ILogger<Network_Metrics_Controller> logger,IMapper mapper)
        {
            _netWorkMetricsRepository = networkMetrics;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<Network_MetricsDTO>> GetNetworkMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get Network metrics.");
            return Ok(_mapper.Map<List<Network_MetricsDTO>>(_netWorkMetricsRepository.GetByTimePeriod(timeFrom, timeTo)));
        }

        [HttpGet("all")]
        public ActionResult<IList<Network_MetricsDTO>> GetCpuMetricsAll() => Ok(_mapper.Map<List<Network_MetricsDTO>>(_netWorkMetricsRepository.GetAll()));

        //[HttpPost("create")]
        //public IActionResult Create([FromBody] NetworkMetricsCreateRequest request)
        //{
        //    _logger.LogInformation("Create Network metric.");
        //    _netWorkMetricsRepository.Create(
        //        _mapper.Map<Network_Metrics>(request));
        //    return Ok();
        //}

        //[HttpDelete("delete")]
        //public IActionResult Delete([FromQuery] int id)
        //{
        //    _logger.LogInformation("Delete network metric.");
        //    _netWorkMetricsRepository.Delete(id);
        //    return Ok();
        //}
    }
}
