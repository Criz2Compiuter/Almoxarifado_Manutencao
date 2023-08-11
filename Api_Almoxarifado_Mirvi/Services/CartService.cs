using Api_Almoxarifado_Mirvi.DTOs;
using Api_Almoxarifado_Mirvi.Models;
using AutoMapper;
using System.Data.Entity;

namespace Api_Almoxarifado_Mirvi.Services {
    public class CartService : ICartService {

        private readonly Api_Almoxarifado_MirviContext _context;
        private IMapper _mapper;

        public CartService() {
        }

        public CartService(Api_Almoxarifado_MirviContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartDTO> GetCartByUserIdAsync(string userId) {
            Cart cart = new Cart {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId)
            };

            cart.CartItems = _context.CartItems.Where(c => c.CartHeaderId == cart.CartHeader.Id)
                .Include(c => c.Produto);

            return _mapper.Map<CartDTO>(cart);
        }

        public async Task<bool> DeleteItemCartAsync(int cartItemId) {
            try {
                CartItem cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

                int total = _context.CartItems.Where(c => c.CartHeaderId == cartItem.CartHeaderId).Count();

                _context.CartItems.Remove(cartItem);

                if (total == 1) {
                    var cartHeaderRemove = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartItem.CartHeaderId);

                    _context.CartHeaders.Remove(cartHeaderRemove);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) {
                return false;
            }
            
        }

        public Task<CartDTO> GetCartbyUserIdAsync(string userid) {
            throw new NotImplementedException();
        }

        public async Task<CartDTO> UpdateCartAsync(CartDTO cartDto) {
            Cart cart = _mapper.Map<Cart>(cartDto);

            await SaveProdutoInDataBase(cartDto, cart);

            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                c => c.UserId == cart.CartHeader.UserId);

            if(cartHeader is null) {
                await CreateCartHeaderAndItems(cart);
            }
            else {
                await UpdateQuantityAndItems(cartDto, cart, cartHeader);
            }
            return _mapper.Map<CartDTO>(cart);
        }

        private async Task UpdateQuantityAndItems(CartDTO cartDto, Cart cart, CartHeader cartHeader) {
            var cartDetail = await _context.CartItems.AsNoTracking().FirstOrDefaultAsync(
                p => p.ProdutoId == cartDto.CartItems.FirstOrDefault()
                .ProdutoId && p.CartHeaderId == cartHeader.Id);

            if(cartDetail is null)
            {
                cart.CartItems.FirstOrDefault().CartHeaderId = cartHeader.Id;
                cart.CartItems.FirstOrDefault().Produto = null;
                _context.CartItems.Add(cart.CartItems.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else {
                cart.CartItems.FirstOrDefault().Produto = null;
                cart.CartItems.FirstOrDefault().Quantity += cartDetail.Quantity;
                cart.CartItems.FirstOrDefault().Id = cartDetail.Id;
                cart.CartItems.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                _context.CartItems.Update(cart.CartItems.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
        }

        private async Task CreateCartHeaderAndItems(Cart cart) {
            _context.CartHeaders.Add(cart.CartHeader);
            await _context.SaveChangesAsync();

            cart.CartItems.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
            cart.CartItems.FirstOrDefault().Produto = null;

            _context.CartItems.Add(cart.CartItems.FirstOrDefault());

            await _context.SaveChangesAsync();
        }

        private async Task SaveProdutoInDataBase(CartDTO cartDto, Cart cart) {
            var produto = await _context.Produto.FirstOrDefaultAsync(p => p.Id ==
            cartDto.CartItems.FirstOrDefault().ProdutoId);
        }

        public async Task<bool> CleancartAsync(string userId) {
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

            if(cartHeader is not null) {
                _context.CartItems.RemoveRange(_context.CartItems
                    .Where(c => c.CartHeaderId == cartHeader.Id));

                _context.CartHeaders.Remove(cartHeader);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
