using AutoMapper;
using InstrumentStore.Models;
using InstrumentStore.Models.Dto;

namespace InstrumentStore;

public class MappingConfig
{
    // method that returns auto mapper configuration
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDto, Product>().ReverseMap();
            config.CreateMap<CustomerDto, Customer>().ReverseMap();
            config.CreateMap<OrderDto, Order>().ReverseMap();
        });
        return mappingConfig;
    }
}