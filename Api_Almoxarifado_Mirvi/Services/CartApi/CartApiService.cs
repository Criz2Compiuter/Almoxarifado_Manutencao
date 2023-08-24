using Api_Almoxarifado_Mirvi.DTOs;
using Api_Almoxarifado_Mirvi.Models;
using AutoMapper;
using System.Data.Entity;

namespace Api_Almoxarifado_Mirvi.Services.CartApi
{
    public class CartApiService : ICartApiService
    {
        private readonly Api_Almoxarifado_MirviContext _context;
        private IMapper mapper;
        public CartApiService(Api_Almoxarifado_MirviContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }
        public async Task<CartDTO> GetCartByUserIdAsync(string userId)
        {
            Cart cart = new Cart
            {
                CartHeader = await _context.CartHeader.FirstOrDefaultAsync(c => c.UserId == userId)
            };

            cart.CartItems = _context.CartItems.Where(c => c.CartHeaderId == cart.CartHeader.Id)
                .Include(c => c.Produto);

            return mapper.Map<CartDTO>(cart);
        }

        public async Task<bool> DeleteItemCartAsync(int cartItemId)
        {
            try
            {
                CartItem cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

                int total = _context.CartItems.Where(c => c.CartHeaderId == cartItem.CartHeaderId).Count();

                _context.CartItems.Remove(cartItem);

                if (total == 1)
                {
                    var cartHeaderRemove = await _context.CartHeader.FirstOrDefaultAsync(c => c.Id == cartItem.CartHeaderId);

                    _context.CartHeader.Remove(cartHeaderRemove);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CleanCartAsync(string userId)
        {
            var cartHeader = await _context.CartHeader.FirstOrDefaultAsync(c => c.UserId == userId);

            if(cartHeader is not null )
            {
                _context.CartItems.RemoveRange(_context.CartItems.Where(c => c.CartHeaderId == cartHeader.Id));

                _context.CartHeader.Remove(cartHeader);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartDTO> UpdatecartAsync(CartDTO cartDto)
        {
            Cart cart = mapper.Map<Cart>(cartDto);

            await SaveProdutoInDatabase(cartDto, cart);

            var cartHeader = await _context.CartHeader.AsNoTracking().FirstOrDefaultAsync(
                c => c.UserId == cart.CartHeader.UserId);

            if( cartHeader is null){
                await CreateCartHeaderAndItems(cart);
            }
            else
            {
                await UpdateQuantidadeAndItems(cartDto, cart, cartHeader);
            }
            return mapper.Map<CartDTO>(cart);
        }

        private async Task UpdateQuantidadeAndItems(CartDTO cartDto, Cart cart, CartHeader? cartHeader)
        {
            var cartDetails = await _context.CartItems.AsNoTracking().FirstOrDefaultAsync(
                p => p.ProdutoId == cartDto.CartItems.FirstOrDefault().ProdutoId && p.CartHeaderId == cartHeader.Id);

            if(cartDetails is null)
            {
                cart.CartItems.FirstOrDefault().CartHeaderId = cartHeader.Id;
                cart.CartItems.FirstOrDefault().Produto = null;
                _context.CartItems.Add(cart.CartItems.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                cart.CartItems.FirstOrDefault().Produto = null;
                cart.CartItems.FirstOrDefault().Quantidade += cartDetails.Quantidade;
                cart.CartItems.FirstOrDefault().Id = cartDetails.Id;
                cart.CartItems.FirstOrDefault().CartHeaderId = cartDetails.CartHeaderId;
                _context.CartItems.Update(cart.CartItems.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
        }

        private async Task CreateCartHeaderAndItems(Cart cart)
        {
            _context.CartHeader.Add(cart.CartHeader);
            await _context.SaveChangesAsync();

            cart.CartItems.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
            cart.CartItems.FirstOrDefault().Produto = null;

            _context.CartItems.Add(cart.CartItems.FirstOrDefault());

            await _context.SaveChangesAsync();
        }

        private async Task SaveProdutoInDatabase(CartDTO cartDto, Cart cart)
        {
            var produto = await _context.Produto.FirstOrDefaultAsync(p => p.Id ==
            cartDto.CartItems.FirstOrDefault().ProdutoId);

            if(produto is null)
            {
                _context.Produto.Add(cart.CartItems.FirstOrDefault().Produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
