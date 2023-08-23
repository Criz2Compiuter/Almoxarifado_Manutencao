using Api_Almoxarifado_Mirvi.Models;
using System.Web;

namespace Api_Almoxarifado_Mirvi.Services;

public class CartItemService : IDisposable
{
    public string cartItemId { get; set; }

    private Api_Almoxarifado_MirviContext _context {  get; set; }
    private readonly IHttpContextAccessor _httpContextAccessor;

    public const string CartKey = "CartId";

    public CartItemService(Api_Almoxarifado_MirviContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public void AddToCart(int id)
    {
        // Retrieve the product from the database.           
        cartItemId = GetCartId();

        var cartItem = _context.CartItems.SingleOrDefault(
            c => c.CartId == cartItemId
            && c.ProdutoId == id);
        if (cartItem == null)
        {
            // Create a new cart item if no cart item exists.                 
            cartItem = new CartItem
            {
                Id = Guid.NewGuid().ToString(),
                ProdutoId = id,
                CartId = cartItemId,
                Produto = _context.Produto.SingleOrDefault(
               p => p.Id == id),
                Quantidade = 1,
                DateCreated = DateTime.Now
            };

            _context.CartItems.Add(cartItem);
        }
        else
        {
            // If the item does exist in the cart,                  
            // then add one to the quantity.                 
            cartItem.Quantidade++;
        }
        _context.SaveChanges();
    }

    public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

    public string GetCartId()
    {
        if (_httpContextAccessor.HttpContext.Session.GetString(CartKey) == null)
        {
            if (!string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.User.Identity.Name))
            {
                _httpContextAccessor.HttpContext.Session.SetString(CartKey, _httpContextAccessor.HttpContext.User.Identity.Name);
            }
            else
            {
                Guid tempCartId = Guid.NewGuid();
                _httpContextAccessor.HttpContext.Session.SetString(CartKey, tempCartId.ToString());
            }
        }
        return _httpContextAccessor.HttpContext.Session.GetString(CartKey);
    }

    public List<CartItem> GetCartItems()
    {
        cartItemId = GetCartId();

        return _context.CartItems.Where(
            c => c.CartId == cartItemId).ToList();
    }
}
