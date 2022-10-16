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
    [Route("api/metrics/DotNet")]
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

        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<DotNet_MetricsDTO>> GetDotNetMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get DotNet metrics.");
            return Ok(_mapper.Map<List<DotNet_MetricsDTO>>(_dotNetMetricsRepository.GetByTimePeriod(timeFrom, timeTo)));
        }

        [HttpGet("all")]
        public ActionResult<IList<DotNet_MetricsDTO>> GetCpuMetricsAll() => Ok(_mapper.Map<List<DotNet_MetricsDTO>>(_dotNetMetricsRepository.GetAll()));


        //[HttpPost("create")]
        //public IActionResult Create([FromBody] DotNetMetricsCreateRequest request)
        //{
        //    _logger.LogInformation("Create DotNet metric.");
        //    _dotNetMetricsRepository.Create(
        //        _mapper.Map<DotNet_Metrics>(request));
        //    return Ok();
        //}

        //[HttpDelete("delete")]
        //public IActionResult Delete([FromQuery] int id)
        //{
        //    _logger.LogInformation("Delete dotnet metric.");
        //    _dotNetMetricsRepository.Delete(id);
        //    return Ok();
        //}
    }
}
