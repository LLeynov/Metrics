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
    [Route("api/metrics/HDD")]
    [ApiController]
    public class HDD_Metrics_Controller : ControllerBase
    {
        private readonly ILogger<HDD_Metrics_Controller> _logger;
        private readonly IHDDMetricsRepository _hddMetricsRepository;
        private readonly IMapper _mapper;
        public HDD_Metrics_Controller(IHDDMetricsRepository hddMetrics, ILogger<HDD_Metrics_Controller> logger,IMapper mapper)
        {
            _hddMetricsRepository = hddMetrics;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HDDMetricsCreateRequest request)
        {
            _logger.LogInformation("Create HDD metric.");
            _hddMetricsRepository.Create(
                _mapper.Map<HDD_Metrics>(request));
            return Ok();
        }

        [HttpGet("from /{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<HDD_MetricsDTO>> GetHDDMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get HDD metrics.");
            return Ok(_mapper.Map<List<HDD_MetricsDTO>>(_hddMetricsRepository.GetByTimePeriod(timeFrom, timeTo)));
        }

        [HttpGet("all")]
        public ActionResult<IList<HDD_MetricsDTO>> GetCpuMetricsAll() => Ok(_mapper.Map<List<HDD_MetricsDTO>>(_hddMetricsRepository.GetAll()));
    }

}
