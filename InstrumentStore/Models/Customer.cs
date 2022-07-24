using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InstrumentStore.Models;

/// <summary>
/// This is the class that represents a customer.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public class Customer
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public DateTime ContactDate { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    [InverseProperty(nameof(Order.CustomerDetails))]
    public ICollection<Order> Orders { get; set; }
}