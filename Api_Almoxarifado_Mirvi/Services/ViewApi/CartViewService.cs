using Api_Almoxarifado_Mirvi.Models;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Api_Almoxarifado_Mirvi.Services.ViewApi
{
    public class CartViewService : ICartViewService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions? _options;
        private const string apiEndpoint = "/api/cart";
        private Cart cart = new Cart();
        public const string CartSessionKey = "CartId";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Api_Almoxarifado_MirviContext _context;

        public CartViewService(IHttpClientFactory clientFactory, Api_Almoxarifado_MirviContext context)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _context = context;
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            var client = _clientFactory.CreateClient("CartApi");

            using (var response = await client.GetAsync($"{apiEndpoint}/getcart/{userId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cart = await JsonSerializer
                        .DeserializeAsync<Cart>
                        (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return cart;
        }

        public async Task<Cart> AddItemToCartAsync(Cart cart)
        {
            var cartItem = _context.CartItems.SingleOrDefault(
         c => c.Id == id
         && c.ProdutoId == );
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(
                   p => p.ProductID == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public async Task<Cart> UpdateCartAsync(Cart cartVM)
        {
            var client = _clientFactory.CreateClient("CartApi");

            Cart cartUpdated = new Cart();

            using (var response = await client.PutAsJsonAsync($"{apiEndpoint}/updatecart/", cartVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cartUpdated = await JsonSerializer
                        .DeserializeAsync<Cart>
                        (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return cartUpdated;
        }

        public async Task<bool> RemoveItemFromCartAsync(int cartId)
        {
            var client = _clientFactory.CreateClient("CartApi");

            using (var response = await client.DeleteAsync($"{apiEndpoint}/deletecart/" + cartId))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
        public string GetCartId()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.User.Identity.Name))
                {
                    _httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, _httpContextAccessor.HttpContext.User.Identity.Name);
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    _httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return _httpContextAccessor.HttpContext.Session.GetString(CartSessionKey);
        }
    }
}
