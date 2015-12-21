using AutoMapper;
using FanSelector.Models.Db;

namespace FanSelector.Api
{
    public class AutoMapperConfiguration
    {
        public static void Setup()
        {
            Mapper.CreateMap<Contests, FanSelector.Models.Output.Contests>();
        }
    }
}