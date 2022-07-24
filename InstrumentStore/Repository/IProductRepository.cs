using InstrumentStore.Models.Dto;

namespace InstrumentStore.Repository;

/// <summary>
/// This is the interface for the product repository.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetProducts();
    
    Task<ProductDto> GetProductById(int id);
    
    Task<IEnumerable<ProductDto>> GetByFieldContains(string value);
    Task<List<Tuple<ProductDto, int>>> GetByDateBetween(string? startDate, string? endDate);

    Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
   
    Task<bool> DeleteProduct(int id);

    Task<List<ProductDto>> GetByCustomer(int? customer);
}