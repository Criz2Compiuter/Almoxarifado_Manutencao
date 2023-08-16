using Api_Almoxarifado_Mirvi.DTOs;
using Api_Almoxarifado_Mirvi.Services;
using Api_Almoxarifado_Mirvi.Services.Contratos;
using Microsoft.AspNetCore.Mvc;

namespace VShop.CartApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartServiceContrato _cartService;

    public CartController(ICartServiceContrato repository)
    {
        _cartService = repository;
    }

    [HttpGet("getcart/{userid}")]
    public async Task<ActionResult<CartDTO>> GetByUserId(string userid)
    {
        var cartDto = await _cartService.GetCartByUserIdAsync(userid);

        if (cartDto is null)
            return NotFound();

        return Ok(cartDto);
    }


    [HttpPost("addcart")]
    public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDto)
    {
        var cart = await _cartService.UpdateCartAsync(cartDto);

        if (cart is null)
            return NotFound();

        return Ok(cart);
    }

    [HttpPut("updatecart")]
    public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDto)
    {
        var cart = await _cartService.UpdateCartAsync(cartDto);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpDelete("deletecart/{id}")]
    public async Task<ActionResult<bool>> DeleteCart(int id)
    {
        var status = await _cartService.DeleteItemCartAsync(id);

        if (!status)
            return BadRequest();

        return Ok(status);
    }
}