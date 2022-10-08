using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.DTO;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{

    [Route("api/metrics/CPU")]
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
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metric.");
            _cpuMetricsRepository.Create(_mapper.Map<CPU_Metrics>(request));
          
            return Ok();
        }

        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<CPU_MetricsDTO>> GetCPUMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get CPU metrics.");
            return Ok(_mapper.Map<List<CPU_MetricsDTO>>
                (_cpuMetricsRepository.GetByTimePeriod(timeFrom, timeTo)));
        }

        [HttpGet("all")]
        public ActionResult<IList<CPU_MetricsDTO>> GetCpuMetricsAll() => Ok(_mapper.Map<List<CPU_MetricsDTO>>(_cpuMetricsRepository.GetAll()));
    }
}
