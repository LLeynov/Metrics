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
    
    [Route("api/metrics/RAM")]
    [ApiController]
    public class RAM_Metrics_Controller : ControllerBase
    {
        private readonly ILogger<RAM_Metrics_Controller> _logger;
        private readonly IRAMMetricsRepository _ramMetricsRepository;
        private readonly IMapper _mapper;
        public RAM_Metrics_Controller(IRAMMetricsRepository ramMetrics, ILogger<RAM_Metrics_Controller> logger,IMapper mapper)
        {
            _ramMetricsRepository = ramMetrics;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] RAMMetricsCreateRequest request)
        {
            _logger.LogInformation("Create RAM metric.");
            _ramMetricsRepository.Create(
                _mapper.Map<RAM_Metrics>(request));
            return Ok(); 
        }

        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<RAM_Metrics>> GetRamMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get RAM metrics.");
            return Ok(_mapper.Map<List<RAM_MetricsDTO>>(_ramMetricsRepository.GetByTimePeriod(timeFrom, timeTo)));
        }

        [HttpGet("all")]
        public ActionResult<IList<RAM_MetricsDTO>> GetCpuMetricsAll() => Ok(_mapper.Map<List<RAM_MetricsDTO>>(_ramMetricsRepository.GetAll()));
    }
}
