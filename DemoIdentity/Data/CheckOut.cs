using FPTBook.Models;
using FPTBookDemo.Models;

namespace FPTBook.Data
{
	public class CheckOut
	{
		public Order Order { get; set; }
		public ApplicationUser User { get; set; }
		public List<CartItem> CartItems { get; set; }
	}
}
