using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class CategoryService : ICategoryService
{

    private const string _apiEndpoint = "/api/categories/";
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;

    public CategoryService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        IEnumerable<CategoryViewModel> categories;

        using (var response = await client.GetAsync(_apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                categories = await JsonSerializer.
                    DeserializeAsync<IEnumerable<CategoryViewModel>>
                    (apiResponse,_options);
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        return categories;
    }
}
