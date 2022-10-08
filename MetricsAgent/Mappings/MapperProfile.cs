using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.DTO;
using MetricsAgent.Models.Requests;

namespace MetricsAgent.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CPU
            CreateMap<CpuMetricCreateRequest, CPU_Metrics>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(
                    src => (long)src.Time.TotalSeconds));

            CreateMap<CPU_Metrics,CPU_MetricsDTO>();

            ////DotNet
            //CreateMap<DotNetMetricsCreateRequest, DotNet_Metrics>()
            //    .ForMember(x => x.Time,
            //        opt => opt.MapFrom(
            //            src => (long)src.Time.TotalSeconds));

            //CreateMap<DotNet_Metrics, DotNet_MetricsDTO>();

            ////HDD
            //CreateMap<HDDMetricsCreateRequest, HDD_Metrics>()
            //    .ForMember(x => x.Time,
            //        opt => opt.MapFrom(
            //            src => (long)src.Time.TotalSeconds));

            //CreateMap<HDD_Metrics, HDD_MetricsDTO>();

            ////Network
            //CreateMap<NetworkMetricsCreateRequest, Network_Metrics>()
            //    .ForMember(x => x.Time,
            //        opt => opt.MapFrom(
            //            src => (long)src.Time.TotalSeconds));

            //CreateMap<Network_Metrics, Network_MetricsDTO>();

            ////RAM
            //CreateMap<RAMMetricsCreateRequest, RAM_Metrics>()
            //    .ForMember(x => x.Time,
            //        opt => opt.MapFrom(
            //            src => (long)src.Time.TotalSeconds));

            //CreateMap<RAM_Metrics, RAM_MetricsDTO>();
        }
    }
}
