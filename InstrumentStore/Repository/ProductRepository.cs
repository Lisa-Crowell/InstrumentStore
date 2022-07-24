using AutoMapper;
using InstrumentStore.DbContexts;
using InstrumentStore.Models;
using InstrumentStore.Models.Dto;
using MySqlConnector;
using Microsoft.EntityFrameworkCore;

namespace InstrumentStore.Repository;

/// <summary>
/// This is class representing the product repository.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public class ProductRepository : IProductRepository
{
    private readonly InstrumentStoreDbContext _db;
    private readonly IMapper _mapper;
    
    public ProductRepository(InstrumentStoreDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var productList = await _db.Products.ToListAsync();
        return _mapper.Map<List<ProductDto>>(productList);
    }

    public async Task<ProductDto> GetProductById(int id)
    {
        var product = await _db.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<ProductDto>(product);
    }

    // set as a pattern to use in the future ???Need ID or no????
    public async Task<IEnumerable<ProductDto>> GetByFieldContains(string value)
    {
        var productList = _db.Products.Where(p => p.Id.ToString() == value ||
                                                  p.Name.Contains(value) ||
                                                  p.Manufacturer.Contains(value) ||
                                                  p.ModelNumber.Contains(value) ||
                                                  p.RetailPrice.ToString() == value ||
                                                  p.WholeSalePrice.ToString() == value ||
                                                  p.QuantityInStock.ToString() == value);
        
        return _mapper.Map<IEnumerable<ProductDto>>(productList);
    }

    public async Task<List<Tuple<ProductDto, int>>> GetByDateBetween(string? startDate, string? endDate)
    {
        var escapedStartDate = new MySqlParameter("@startDate", DateTime.Parse(startDate));
        var escapedEndDate = new MySqlParameter("@endDate", DateTime.Parse(endDate));
        
        var cmd = new MySqlCommand(
            "SELECT P.Id, P.Name, P.ModelNumber, P.RetailPrice, P.WholeSalePrice, P.Category, P.Manufacturer, P.QuantityInStock FROM Products P JOIN OrderProduct OP on P.Id = OP.ItemsId JOIN Orders O on O.Id = OP.OrdersId WHERE O.OrderDate <= @endDate AND O.OrderDate >= @startDate GROUP BY P.Id");
        var items = new List<Tuple<ProductDto, int>>();

        var products = await _db.Products.FromSqlRaw(cmd.CommandText, escapedEndDate, escapedStartDate).ToListAsync();

        foreach (var product in products)
        {
            var count = _db.Orders.Count(o => o.Items.Contains(product));
            items.Add(Tuple.Create(_mapper.Map<Product, ProductDto>(product), count));
        }
        
        return items;
    }

    public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
    {
        var product = _mapper.Map<ProductDto, Product>(productDto);
        if (product.Id > 0)
        {
            _db.Products.Update(product);
        }
        else
        {
            _db.Products.Add(product);
        }
        await _db.SaveChangesAsync();
        return _mapper.Map<Product, ProductDto>(product);
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        try 
        {
            var product = await _db.Products.FirstOrDefaultAsync(u => u.Id == productId);
            if (product == null)
            {
                return false;
            }
            
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<ProductDto>> GetByCustomer(int? customer)
    {
        var customerId = new MySqlParameter("@customerId", customer);
        var cmd = new MySqlCommand(
            "SELECT p.Id, p.Name, ModelNumber, RetailPrice, WholeSalePrice, Category, Manufacturer, QuantityInStock FROM Products p JOIN OrderProduct OP on p.Id = OP.ItemsId JOIN Orders O on OP.OrdersId = O.Id WHERE O.Customer = @customerId");
        var products = await _db.Products.FromSqlRaw(cmd.CommandText, customerId).ToListAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }
}