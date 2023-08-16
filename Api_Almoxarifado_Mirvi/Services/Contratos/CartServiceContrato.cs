using Api_Almoxarifado_Mirvi.Models.ViewModels;
using System.Text;
using System.Text.Json;

namespace Api_Almoxarifado_Mirvi.Services.Contratos
{
    public class CartServiceContrato : ICartServiceContrato
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/cart";
        private CartViewModel cartVM = new CartViewModel();

        public CartServiceContrato(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<CartViewModel> GetCartByUserIdAsync(string userId)
        {
            var client = _clientFactory.CreateClient("CarApi");

            using (var response = await client.GetAsync($"{apiEndpoint}/getcart/{userId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cartVM = await JsonSerializer.DeserializeAsync<CartViewModel>
                        (apiResponse);
                }
                else
                {
                    return null;
                }
            }
            return cartVM;
        }

        public async Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM)
        {
            var client = _clientFactory.CreateClient("CartApi");

            using (var response = await client.PostAsync($"{apiEndpoint}/addcart/", null))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cartVM = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse);
                }
                else
                {
                    return null;
                }
            }
            return cartVM;
        }

        public async Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM)
        {
            var client = _clientFactory.CreateClient("CartApi");

            CartViewModel cartUpdated = new CartViewModel();

            using (var response = await client.PutAsJsonAsync($"{apiEndpoint}/updatecart/", cartVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cartUpdated = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse);
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
            var client  = _clientFactory.CreateClient("CartApi");

            using (var response = await client.DeleteAsync($"{apiEndpoint}/deletecart/" + cartId))
            {
                if(response.IsSuccessStatusCode )
                {
                    return true;
                }
            } 
            return false;
        }
    }
}
