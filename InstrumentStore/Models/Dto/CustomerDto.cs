using System.Text.Json.Serialization;

namespace InstrumentStore.Models.Dto;

/// <summary>
/// This is the Dto for the customer model.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public class CustomerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public DateTime ContactDate { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public ICollection<Order> Orders { get; set; }
}