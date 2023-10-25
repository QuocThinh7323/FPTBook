using FPTBook.Data;
using FPTBook.Models;
using FPTBookDemo.Data;
using FPTBookDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FPTBook.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        public CartController(ILogger<CartController> logger, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Index()
        {
            return View(GetCartItems());
        }

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

        /// Thêm sản phẩm vào cart
		[Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {
            var product = _db.Book
                            .Where(p => p.Id == productid)
                            .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Book.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { Quantity = 1, Book = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Index));
        }

        /// Cập nhật
		[Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Book.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            CheckOut checkOut = new CheckOut();
            checkOut.User = user;
            checkOut.CartItems = GetCartItems();
            return View(checkOut);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckOut checkOut)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            decimal total = 0;
            foreach (var item in GetCartItems())
            {
                total += item.Quantity * item.Book.Price;
            }

            Order order = new Order();
            order.UserName = user.UserName;
            order.User = user;
            order.OrderTime = DateTime.Now;
            order.Total = total;
            order.State = 0;
            _db.Order.Add(order);
            _db.SaveChanges();

            int orderId = order.Id;

            foreach (var item in GetCartItems())
            {
                OrderItem orderItem = new OrderItem();
                orderItem.BookID = item.Book.Id;
                orderItem.Quantity = item.Quantity;
                orderItem.OrderID = orderId;

                _db.OrderItem.Add(orderItem);
                _db.SaveChanges();

                Book book = item.Book;
                book.Quantity -= item.Quantity;

                _db.Update(book);
                _db.SaveChanges();
            }

            ClearCart();

            return RedirectToAction("Shop", "Home");
        }
    }
}
