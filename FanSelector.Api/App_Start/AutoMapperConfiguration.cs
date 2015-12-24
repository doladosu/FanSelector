using AutoMapper;
using FanSelector.Models.Db;

namespace FanSelector.Api
{
    public class AutoMapperConfiguration
    {
        public static void Setup()
        {
            Mapper.CreateMap<Contest, FanSelector.Models.Output.Contests>().ReverseMap();
        }
    }
}