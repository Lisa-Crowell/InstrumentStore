using InstrumentStore.Models.Dto;

namespace InstrumentStore.Repository;

/// <summary>
/// This is the interface for the order repository.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public interface IOrderRepository
{
    Task<IEnumerable<OrderDto>> GetOrders();
    Task<OrderDto> GetOrderById(int id);
    Task<IEnumerable<OrderDto>> GetByFieldContains(string value);
    Task<IEnumerable<OrderDto>> GetByDateBetweenDates(string startDate, string endDate);
    Task<OrderDto> CreateUpdateOrder(OrderDto order);
    Task<bool> DeleteOrder(int id);
    Task<IEnumerable<OrderDto>> GetByPriceBetweenPrices(string? start, string? end);
}