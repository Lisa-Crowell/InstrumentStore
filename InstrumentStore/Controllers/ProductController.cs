using InstrumentStore.Models.Dto;
using InstrumentStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentStore.Controllers;

/// <summary>
/// This is the controller for the order.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    protected ResponseDto _response;
    
    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    [HttpGet]
    public async Task<object> GetAll(string? search, string? startDate, string? endDate, int? customer)
    {
        _response = new ResponseDto();
        object productDtos = null;
        
        try
        {
            switch (search)
            {
                case "recent":
                    productDtos = await _productRepository.GetByDateBetween((DateTime.Now.AddMonths(-6)).ToString(), DateTime.Now.ToString());
                    break;
                case null:
                    if (customer != null)
                    {
                        productDtos = await _productRepository.GetByCustomer(customer);
                    }
                    else
                    {
                        productDtos = await _productRepository.GetProducts();
                    }
                    break;
                default:
                    productDtos = await _productRepository.GetByFieldContains(search);
                    break;
            }
           
            _response.Result = productDtos;
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
            var productDto = await _productRepository.GetProductById(id);
            _response.Result = productDto;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response.Result;
    }
    [HttpPost]
    public async Task<object> Post([FromBody] ProductDto productDto)
    {
        try
        {
            var product = await _productRepository.CreateUpdateProduct(productDto);
            _response.Result = product;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response;
    }
    
    [HttpPut]
    public async Task<object> Put([FromBody] ProductDto productDto)
    {
        try
        {
            var product = await _productRepository.CreateUpdateProduct(productDto);
            _response.Result = product;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response;
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<object> Delete(int id)
    {
        try
        {
            var isSuccess = await _productRepository.DeleteProduct(id);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() {e.ToString()};
        }
        return _response;
    }
}