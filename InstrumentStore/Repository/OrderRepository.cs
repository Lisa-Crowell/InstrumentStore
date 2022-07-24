using AutoMapper;
using InstrumentStore.DbContexts;
using InstrumentStore.Models;
using InstrumentStore.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace InstrumentStore.Repository;

/// <summary>
/// This is the class representing the order repository.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public class OrderRepository : IOrderRepository
{
    private readonly InstrumentStoreDbContext _db;
    private readonly IMapper _mapper;
    
    public OrderRepository(InstrumentStoreDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        var orderList = _db.Orders.Include(o => o.Items).ToList();
        return _mapper.Map<List<OrderDto>>(orderList);
    }

    public async Task<OrderDto> GetOrderById(int id)
    {
        var product = await _db.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<OrderDto>(product);
    }

    public Task<IEnumerable<OrderDto>> GetByFieldContains(string value)
    {
        var orderList = _db.Orders
            .Where(o => 
                o.Id.ToString().Contains(value) || 
                o.Customer.Equals(int.Parse(value)) || 
                o.CustomerDetails.FirstName.Contains(value) ||
                o.CustomerDetails.LastName.Contains(value) ||
                o.CustomerDetails.Email.Contains(value) ||
                o.CustomerDetails.Phone.Contains(value) ||
                o.CustomerDetails.Address.Contains(value) ||
                o.CustomerDetails.City.Contains(value) ||
                o.CustomerDetails.State.Contains(value) ||
                o.CustomerDetails.Zip.Contains(value) ||
                o.CustomerDetails.ContactDate.Equals(DateTime.Parse(value)) ||
                o.Notes.Contains(value) ||
                o.OrderDate.Equals(DateTime.Parse(value)) ||
                o.ShipDate.Equals(DateTime.Parse(value)) ||
                o.OrderStatus.Contains(value) ||
                o.OrderTotal.Equals(decimal.Parse(value)) ||
                o.Items.Any(item => 
                    item.Id.Equals(int.Parse(value)) || 
                    item.Name.Contains(value) ||
                    item.Manufacturer.Contains(value) || 
                    item.ModelNumber.Contains(value) ||
                    item.RetailPrice.Equals(Decimal.Parse(value)) ||
                    item.QuantityInStock.Equals(int.Parse(value)) ||
                    item.WholeSalePrice.Equals(Decimal.Parse(value)))
                ).ToListAsync();


        return _mapper.Map<Task<IEnumerable<OrderDto>>>(orderList);
    }

    public async Task<IEnumerable<OrderDto>> GetByDateBetweenDates(string startDate, string endDate)
    {
        Console.WriteLine("Starting");
        var start = DateTime.Parse(startDate);
        var end = DateTime.Parse(endDate);
        
        var orderList = await _db.Orders
            .Include(o => o.Items)
            .Include(o => o.CustomerDetails)
            .Where(o => 
                o.OrderDate.Date >= start.Date && 
                o.OrderDate.Date <= end.Date).ToListAsync();
        Console.WriteLine("Success");
        return _mapper.Map<IEnumerable<OrderDto>>(orderList);
    }

    public async Task<OrderDto> CreateUpdateOrder(OrderDto orderDto)
    {
        var order = _mapper.Map<OrderDto, Order>(orderDto);
        if (order.Id == 0)
        {
            _db.Orders.Add(order);
        }
        else
        {
            _db.Orders.Update(order);
        }
        await _db.SaveChangesAsync();
        return _mapper.Map<Order, OrderDto>(order);
    }

    public async Task<bool> DeleteOrder(int id)
    {
        try
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return false;
            }
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<IEnumerable<OrderDto>> GetByPriceBetweenPrices(string? start, string? end)
    {
        var orderDtos = await _db.Orders.Where(o => 
            o.OrderTotal >= decimal.Parse(start) && 
            o.OrderTotal <= decimal.Parse(end)).ToListAsync();
        
        return _mapper.Map<IEnumerable<OrderDto>>(orderDtos);
    }
}