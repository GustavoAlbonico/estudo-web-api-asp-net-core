using CategoriasMvc.Models;
using CategoriasMvc.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace CategoriasMvc.Services
{
    public class CategoriaService : ICategoriaService
    {

        private const string ApiEndpoint = "/api/1/categorias/";

        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        private CategoriaViewModel _categoriaVM = new CategoriaViewModel();
        private IEnumerable<CategoriaViewModel> _categoriasVM = new List<CategoriaViewModel>();

        public CategoriaService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<CategoriaViewModel>> GetCategorias()
        {
            var client = _clientFactory.CreateClient("CategoriasApi");
            using (var response = await client.GetAsync(ApiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _categoriasVM = await JsonSerializer
                                   .DeserializeAsync<IEnumerable<CategoriaViewModel>>
                                   (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return _categoriasVM;
        }

        public async Task<CategoriaViewModel> GetCategoriaPorId(int id)
        {
            var client = _clientFactory.CreateClient("CategoriasApi");
            using (var response = await client.GetAsync(ApiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _categoriaVM = await JsonSerializer
                        .DeserializeAsync<CategoriaViewModel>
                        (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return _categoriaVM;
        }

        public async Task<CategoriaViewModel> CriarCategoria(CategoriaViewModel categoriaVM)
        {
            var client = _clientFactory.CreateClient("CategoriasApi");
            var categoria = JsonSerializer.Serialize(categoriaVM);
            StringContent content = new StringContent(categoria, Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(ApiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriaVM = await JsonSerializer
                        .DeserializeAsync<CategoriaViewModel>
                        (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return categoriaVM;
        }

        public async Task<bool> AtualizarCategoria(int id, CategoriaViewModel categoriaVM)
        {
            var client = _clientFactory.CreateClient("CategoriasApi");
            using (var response = await client.PutAsJsonAsync(ApiEndpoint + id, categoriaVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeletarCategoria(int id)
        {
            var client = _clientFactory.CreateClient("CategoriasApi");

            using (var response = await client.DeleteAsync(ApiEndpoint + id))
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
