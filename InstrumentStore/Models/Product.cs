using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InstrumentStore.Models;

/// <summary>
/// This is the class that represents a product.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [MinLength(1)]
    [MaxLength(25)]
    public string ModelNumber { get; set; }
    [Precision(10,2)]
    public decimal RetailPrice { get; set; }
    [Precision(10, 2)]
    public decimal WholeSalePrice { get; set; }
    public string Category { get; set; }
    public string Manufacturer { get; set; }
    public int QuantityInStock { get; set; }
    public ICollection<Order> Orders { get; set; }
}