using InstrumentStore.Models.Dto;
using InstrumentStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentStore.Controllers;

/// <summary>
/// This is the controller for the order.
/// </summary>
/// <author>lisa.l.crowell@gmail.com</author>
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    protected ResponseDto _response;
    
    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
        _response = new ResponseDto();
    }

    [HttpGet]
    public async Task<object> GetAll(string? search, string? start, string? end)
    {
        try
        {
            IEnumerable<OrderDto> orderDtos;

            switch (search)
            {
                case "date":
                    orderDtos = await _orderRepository.GetByDateBetweenDates(start, end);
                    break;
                case "price":
                    orderDtos = await _orderRepository.GetByPriceBetweenPrices(start, end);
                    break;
                case null:
                    orderDtos = await _orderRepository.GetOrders();
                    break;
                default:
                    orderDtos = await _orderRepository.GetByFieldContains(search);
                    break;
            }
            _response.Result = orderDtos;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};

        }
        
        return _response.Result;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<object> Get(int id)
    {
        try
        {
            var orderDto = await _orderRepository.GetOrderById(id);
            _response.Result = orderDto;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response.Result;
    }
    [HttpPost]
    public async Task<object> Post([FromBody] OrderDto orderDto)
    {
        try
        {
            await _orderRepository.CreateUpdateOrder(orderDto);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response.Result;
    }
    [HttpPost]
    public async Task<object> Put([FromBody] OrderDto orderDto)
    {
        try
        {
            var order = await _orderRepository.CreateUpdateOrder(orderDto);
            _response.Result = order;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response.Result;
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<object> Delete(int id)
    {
        try
        {
            var isSuccess = await _orderRepository.DeleteOrder(id);
            _response.IsSuccess = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response.Result;
    }
}