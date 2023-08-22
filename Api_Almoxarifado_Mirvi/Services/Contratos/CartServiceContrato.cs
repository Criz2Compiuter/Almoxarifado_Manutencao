using Api_Almoxarifado_Mirvi.Models.ViewModelCart;
using System.Text;
using System.Text.Json;

namespace Api_Almoxarifado_Mirvi.Services.Contratos
{
    public class CartServiceContrato : ICartServiceContrato
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions? _options;
        private const string apiEndpoint = "api/cart";
        private CartViewModel cartVM = new CartViewModel();

        public CartServiceContrato(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
        }

        public async Task<CartViewModel> GetCartByUsersIdAsync(string userId)
        {
            var client = _clientFactory.CreateClient("CartApi");

            using(var response = await client.GetAsync($"{apiEndpoint}/getcart/{userId}"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cartVM = await JsonSerializer
                        .DeserializeAsync<CartViewModel>
                        (apiResponse, _options);
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

            StringContent content = new StringContent(JsonSerializer.Serialize(cartVM),
                Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync($"{apiEndpoint}/addcart/", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    cartVM = await JsonSerializer
                        .DeserializeAsync<CartViewModel>
                        (apiResponse, _options);
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
                    cartUpdated = await JsonSerializer
                        .DeserializeAsync<CartViewModel>
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
    }
}
