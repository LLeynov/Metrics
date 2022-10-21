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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNet_Metrics_Controller : ControllerBase
    {
        private readonly ILogger<DotNet_Metrics_Controller> _logger;
        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;
        private readonly IMapper _mapper;
        public DotNet_Metrics_Controller(IDotNetMetricsRepository dotnetMetrics, ILogger<DotNet_Metrics_Controller> logger,IMapper mapper)
        {
            _dotNetMetricsRepository = dotnetMetrics;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetDotNetMetricsResponse> GetDotNetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get dotnet metrics.");
            return Ok(new GetDotNetMetricsResponse
            {
                Metrics = _dotNetMetricsRepository.GetByTimePeriod(fromTime, toTime)
                    .Select(metric => _mapper.Map<DotNet_MetricsDTO>(metric)).ToList()
            });
        }

        [HttpGet("all")]
        public ActionResult<IList<DotNet_MetricsDTO>> GetCpuMetricsAll() => Ok(_mapper.Map<List<DotNet_MetricsDTO>>(_dotNetMetricsRepository.GetAll()));
    }
}
