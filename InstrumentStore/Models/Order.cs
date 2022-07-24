using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InstrumentStore.Models;

/// <summary>
/// This is the class that represents an order.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public class Order
{
    [Key]
    public int Id { get; set; }
    public int Customer { get; set; }
    [ForeignKey(nameof(Customer))]
    [InverseProperty(nameof(Models.Customer.Orders))]
    public virtual Customer CustomerDetails { get; set; }
    public string Notes { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShipDate { get; set; }
    public string OrderStatus { get; set; }
    [Precision(10, 2)]
    public decimal OrderTotal { get; set; }
    public ICollection<Product> Items { get; set; }
}