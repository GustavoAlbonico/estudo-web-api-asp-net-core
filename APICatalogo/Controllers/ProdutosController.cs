using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    public ProdutosController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet("produtos/{id}")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosCategoria(int id)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosPorCategoriaAsync(id);

        if (produtos is null)
            return NotFound();

        var produtosDto = produtos.Adapt<IEnumerable<ProdutoDTO>>();

        return Ok(produtosDto);
    }

        [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosAsync(produtosParameters);

        return ObterProdutos(produtos);
    }

    [HttpGet("filter/preco/pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco produtosFilterParameters)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFilterParameters);
        return ObterProdutos(produtos);
    }
    private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutos(PagedList<Produto> produtos)
    {
        var metadata = new
        {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
        var produtosDto = produtos.Adapt<IEnumerable<ProdutoDTO>>();
        return Ok(produtosDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
    {
        var produtos = await _uof.ProdutoRepository.GetAllAsync();
        if (produtos is null)
        {
            return NotFound();
        }
        var produtosDto = produtos.Adapt<IEnumerable<ProdutoDTO>>();
        return Ok(produtosDto);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public async Task<ActionResult<ProdutoDTO>> Get(int id)
    {
        var produto = await _uof.ProdutoRepository.GetAsync(c => c.Id == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        var produtoDto = produto.Adapt<ProdutoDTO>();
        return Ok(produtoDto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> Post(ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
            return BadRequest();

        var produto = produtoDto.Adapt<Produto>();

        var novoProduto = _uof.ProdutoRepository.Create(produto);
        await _uof.CommitAsync();

        var novoProdutoDto = novoProduto.Adapt<ProdutoDTO>();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProdutoDto.Id }, novoProdutoDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.Id)
            return BadRequest();//400

        var produto = produtoDto.Adapt<Produto>();

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        await _uof.CommitAsync();

        var produtoAtualizadoDto = produtoAtualizado.Adapt<ProdutoDTO>();

        return Ok(produtoAtualizadoDto);
    }

    
    [HttpPatch("{id}/UpdatePartial")]
    public async Task<ActionResult<ProdutoDTOUpdateResponse>> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDto)
    {
        //valida input 
        if (patchProdutoDto == null || id <= 0 )
            return BadRequest();

        //obtem o produto pelo Id
        var produto = await _uof.ProdutoRepository.GetAsync(c => c.Id == id);

        //se não econtrou retorna
        if (produto == null)
            return NotFound();

        //mapeia produto para ProdutoDTOUpdateRequest
        var produtoUpdateRequest = produto.Adapt<ProdutoDTOUpdateRequest>();

        //aplica as alterações definidas no documento JSON Patch ao objeto ProdutoDTOUpdateRequest
        patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);

        if (!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
            return BadRequest(ModelState);

        // Mapeia as alterações de volta para a entidade Produto
        produtoUpdateRequest.Adapt(produto);

        // Atualiza a entidade no repositório
        _uof.ProdutoRepository.Update(produto);
        // Salve as alterações no banco de dados
        await _uof.CommitAsync();

        //retorna ProdutoDTOUpdateResponse
        return Ok(produto.Adapt<ProdutoDTOUpdateResponse>());
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Delete(int id)
    {
        var produto = await _uof.ProdutoRepository.GetAsync(p => p.Id == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
        await _uof.CommitAsync();

        var produtoDeletadoDto = produtoDeletado.Adapt<ProdutoDTO>();

        return Ok(produtoDeletadoDto);
    }
}