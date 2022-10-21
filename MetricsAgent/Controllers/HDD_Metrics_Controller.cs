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
    [Route("api/metrics/hdd")]
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

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetHddMetricsResponse> GetHDDMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get hdd metrics.");
            return Ok(new GetHddMetricsResponse()
            {
                Metrics = _hddMetricsRepository.GetByTimePeriod(fromTime, toTime)
                    .Select(metric => _mapper.Map<HDD_MetricsDTO>(metric)).ToList()
            });
        }

        [HttpGet("all")]
        public ActionResult<IList<HDD_MetricsDTO>> GetCpuMetricsAll() => Ok(_mapper.Map<List<HDD_MetricsDTO>>(_hddMetricsRepository.GetAll()));
    }

}
