namespace InstrumentStore.Models.Dto;

/// <summary>
/// This is the Dto for the order model.
/// </summary>
/// /// <author>lisa.l.crowell@gmail.com</author>
public class OrderDto
{
    public int OrderId { get; set; }
    public int Customer { get; set; }
    public CustomerDto CustomerDetails { get; set; }
    public string Notes { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShipDate { get; set; }
    public string OrderStatus { get; set; }
    public decimal OrderTotal { get; set; }
    public ICollection<ProductDto> Items { get; set; }
}