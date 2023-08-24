using Api_Almoxarifado_Mirvi.DTOs;
using Api_Almoxarifado_Mirvi.Services.CartApi;
using Microsoft.AspNetCore.Mvc;

namespace Api_Almoxarifado_Mirvi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartApiService _cartApiService;

        public CartController(ICartApiService cartApiService)
        {
            _cartApiService = cartApiService;
        }

        [HttpGet("getcart/{id}")]
        public async Task<ActionResult<CartDTO>> GetByUserId(string id)
        {
            var cartDto = await _cartApiService.GetCartByUserIdAsync(id);

            if(cartDto is null)
            {
                return NotFound();
            }

            return Ok(cartDto);
        }

        [HttpPost("addcart")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDto)
        {
            var cart = await _cartApiService.UpdatecartAsync(cartDto);

            if(cart is null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPut("updatecart")]
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDto)
        {
            var cart = await _cartApiService.UpdatecartAsync(cartDto);
            if(cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("deletecart/{id}")]
        public async Task<ActionResult<bool>> DeleteCart(int id)
        {
            var status = await _cartApiService.DeleteItemCartAsync(id);
            if(!status)
                return BadRequest();
            return Ok(status);
        }
    }
}
