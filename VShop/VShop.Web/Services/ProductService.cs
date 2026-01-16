using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class ProductService : IProductService
{

    private readonly IHttpClientFactory _clientFactory;
    private const string _apiEndpoint = "/api/products/";
    private readonly JsonSerializerOptions _options;
    private ProductViewModel _productVM;
    private IEnumerable<ProductViewModel> _productsVM;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts(string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.GetAsync(_apiEndpoint))
        {
            if(response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _productsVM = await JsonSerializer
                    .DeserializeAsync<IEnumerable<ProductViewModel>>
                    (apiResponse, _options);
            } else
            {
                return null;
            }
        }
        return _productsVM;
    }   

    public async Task<ProductViewModel> FindProductById(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.GetAsync(_apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _productVM = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return _productVM;
    }

    public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHeaderAuthorization(token, client);

        var product = JsonSerializer.Serialize(productVM);
        StringContent content = new StringContent(product, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(_apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _productVM = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return _productVM;
    }

    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.PutAsJsonAsync(_apiEndpoint, productVM))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _productVM = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>
                    (apiResponse, _options);
            } else
            {
                return null;
            }
        }

        return _productVM;
    }

    public async Task<bool> DeleteProductById(int id, string token)
    {

        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.DeleteAsync(_apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", token);
    }

}
