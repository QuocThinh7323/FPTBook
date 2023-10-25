using FPTBookDemo.Models;
using System.ComponentModel.DataAnnotations;
namespace FPTBook.Models;
public class Order
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public ApplicationUser? User { get; set; }

    [DataType(DataType.Date)]
    public DateTime OrderTime { get; set; }
    public decimal Total { get; set; }
    public int State { get; set; }
    public ICollection<OrderItem>? OrderItem { get; set; }
}

