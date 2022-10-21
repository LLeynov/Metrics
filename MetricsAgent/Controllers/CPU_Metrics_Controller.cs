using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.DTO;
using MetricsAgent.Models.DTO.Response;
using MetricsAgent.Services.Impl;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;

namespace MetricsAgent.Controllers
{

    [Route("api/metrics/cpu")]
    [ApiController]
    public class CPU_Metrics_Controller : ControllerBase
    {
        private readonly ILogger<CPU_Metrics_Controller> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper;
        
        
        public CPU_Metrics_Controller(ICpuMetricsRepository cpuMetrics, 
            ILogger<CPU_Metrics_Controller> logger, 
            IMapper mapper)
        {
            _cpuMetricsRepository = cpuMetrics;
            _logger = logger;
            _mapper = mapper ;
        }

        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetCpuMetricsResponse> GetCpuMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics.");

            return Ok(new GetCpuMetricsResponse
            {
                Metrics = _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)
                    .Select(metric => _mapper.Map<CPU_MetricsDTO>(metric)).ToList()
            });
        }
        

        [HttpGet("all")]
        public ActionResult<IList<CPU_MetricsDTO>> GetCpuMetricsAll() => Ok(_mapper.Map<List<CPU_MetricsDTO>>(_cpuMetricsRepository.GetAll()));

    }
}
