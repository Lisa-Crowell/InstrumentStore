using InstrumentStore.Models.Dto;
using InstrumentStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentStore.Controllers;

/// <summary>
/// This is the controller for the customer.
/// </summary>
/// <author>lisa.l.crowell@gmail.com</author>
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    protected ResponseDto _response;
    
    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _response = new ResponseDto();
    }

    [HttpGet]
    public async Task<object> GetAll(string? search, int? customer, string? instrument)
    {
        _response = new ResponseDto();
        try
        {
            IEnumerable<object> customerDtos;

            switch (search)
            {
                case "spent":
                    customerDtos = await _customerRepository.GetCustomersWithSpentAmount();
                    _response.Result = customerDtos;
                    break;
                case null:
                    if (instrument != null)
                    {
                        customerDtos = await _customerRepository.GetCustomersByInstrument(instrument);
                    }
                    else
                    {
                        customerDtos = await _customerRepository.GetCustomers();
                    }
                    break;
                default:
                    customerDtos = await _customerRepository.GetByFieldContains(search);
                    break;
            }
            
            _response.Result = customerDtos;
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
        _response = new ResponseDto();
        try
        {
            var customerDto = await _customerRepository.GetCustomerById(id);
            _response.Result = customerDto;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response.Result;
    }
    

    
    [HttpPost]
    public async Task<object> Post([FromBody] CustomerDto customerDto)
    {
        _response = new ResponseDto();
        try
        {
            var customer = await _customerRepository.CreateUpdateCustomer(customerDto);
            _response.Result = customer;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response.Result;
    }
    
    [HttpPut]
    public async Task<object> Put([FromBody] CustomerDto customerDto)
    {
        _response = new ResponseDto();
        try
        {
            var customer = await _customerRepository.CreateUpdateCustomer(customerDto);
            _response.Result = customer;
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
        _response = new ResponseDto();
        try
        {
            var isSuccess = await _customerRepository.DeleteCustomer(id);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response.Result;
    }
}