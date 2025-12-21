using CategoriasMvc.Models;
using CategoriasMvc.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace CategoriasMvc.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private const string ApiEndpointAutentica = "api/autoriza/login";
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;
        private TokenViewModel _tokenUsuario;

        public AutenticacaoService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<TokenViewModel> AutenticarUsuario(UsuarioViewModel usuarioVM)
        {
            var client = _clientFactory.CreateClient("AutenticaApi");
            var usuario = JsonSerializer.Serialize(usuarioVM);
            StringContent content = new StringContent(usuario, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(ApiEndpointAutentica, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _tokenUsuario = await JsonSerializer
                                .DeserializeAsync<TokenViewModel>
                                (apiResponse,_options);

                    Console.WriteLine($"\n\n {_tokenUsuario}  \n\n");
                } else
                {
                    return null;
                }
            }

            return _tokenUsuario;
        }
    }
}
