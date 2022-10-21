using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.DTO;
using MetricsAgent.Models.DTO.Response;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    
    [Route("api/metrics/ram")]
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

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetRamMetricsResponse> GetRamMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get ram metrics.");
            return Ok(new GetRamMetricsResponse
            {
                Metrics = _ramMetricsRepository.GetByTimePeriod(fromTime, toTime)
                    .Select(metric => _mapper.Map<RAM_MetricsDTO>(metric)).ToList()
            });
        }

        [HttpGet("all")]
        public ActionResult<IList<RAM_MetricsDTO>> GetCpuMetricsAll() => Ok(_mapper.Map<List<RAM_MetricsDTO>>(_ramMetricsRepository.GetAll()));
    }
}
