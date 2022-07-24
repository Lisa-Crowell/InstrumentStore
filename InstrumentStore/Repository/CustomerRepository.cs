using AutoMapper;
using InstrumentStore.DbContexts;
using InstrumentStore.Models;
using InstrumentStore.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace InstrumentStore.Repository;

/// <summary>
/// This is the class representing the customer repository.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public class CustomerRepository : ICustomerRepository
{
    private readonly InstrumentStoreDbContext _db;
    private readonly IMapper _mapper;

    public CustomerRepository(InstrumentStoreDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomers()
    {
        var customerList = await _db.Customers.ToListAsync();
        return _mapper.Map<List<CustomerDto>>(customerList);
    }

    public async Task<CustomerDto> GetCustomerById(int id)
    {
        var customer = await _db.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<IEnumerable<CustomerDto>> GetByFieldContains(string value)
    {
        var customerList = await _db.Customers.Where(c => c.Id.ToString().Contains(value) ||
                                                          c.FirstName.Contains(value) ||
                                                          c.LastName.Contains(value) ||
                                                          c.Email.Contains(value) ||
                                                          c.Phone.Contains(value) ||
                                                          c.Address.Contains(value) ||
                                                          c.City.Contains(value) ||
                                                          c.State.Contains(value) ||
                                                          c.Zip.Contains(value) ||
                                                          c.ContactDate.ToString().Contains(value)).ToListAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customerList);
    }

    public async Task<CustomerDto> CreateUpdateCustomer(CustomerDto customerDto)
    {
        var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
        if (customer.Id == 0)
        {
            _db.Customers.Add(customer);
        }
        else
        {
            _db.Customers.Update(customer);
        }

        await _db.SaveChangesAsync();
        return _mapper.Map<Customer, CustomerDto>(customer);
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        try
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return false;
            }

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<IEnumerable<Tuple<CustomerDto, decimal>>> GetCustomersWithSpentAmount()
    {
        var results = new List<Tuple<CustomerDto, decimal>>();
        var customers = await _db.Customers.ToListAsync();
        foreach (var customer in customers)
        {
            var amount = await _db.Orders.Include(o => o.CustomerDetails).Where(o => o.Customer == customer.Id)
                .ToListAsync();
            results.Add(new Tuple<CustomerDto, decimal>(_mapper.Map<Customer, CustomerDto>(customer),
                amount.Sum(o => o.OrderTotal)));
        }

        return results;
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomersByInstrument(string instrument)
    {
        var customerList = await _db.Customers
            .Include(c => c.Orders)
            .ThenInclude(o => o.Items)
            .Where(c => c.Orders.Any(o => o.Items.Any(i => i.Name.Contains(instrument))))
            .ToListAsync();

        return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customerList);
    }
}