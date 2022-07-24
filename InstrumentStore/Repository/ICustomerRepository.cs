using InstrumentStore.Models.Dto;

namespace InstrumentStore.Repository;


/// <summary>
/// This is the interface for the customer repository.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public interface ICustomerRepository
{
    Task<IEnumerable<CustomerDto>> GetCustomers();
    Task<CustomerDto> GetCustomerById(int id);
    Task<IEnumerable<CustomerDto>> GetByFieldContains(string value);
    Task<CustomerDto> CreateUpdateCustomer(CustomerDto customer);
    Task<bool> DeleteCustomer(int id);
    Task<IEnumerable<Tuple<CustomerDto, decimal>>> GetCustomersWithSpentAmount();
    Task<IEnumerable<CustomerDto>> GetCustomersByInstrument(string instrument);
}