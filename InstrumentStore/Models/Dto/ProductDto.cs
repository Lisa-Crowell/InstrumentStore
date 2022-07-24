namespace InstrumentStore.Models.Dto;

/// <summary>
/// This is the Dto for the product model
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ModelNumber { get; set; }
    public decimal RetailPrice { get; set; }
    public decimal WholeSalePrice { get; set; }
    public string Category { get; set; }
    public string Manufacturer { get; set; }
    public int QuantityInStock { get; set; }
}