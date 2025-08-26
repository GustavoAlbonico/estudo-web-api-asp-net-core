using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    //Aqui eu poderia usar apenas o repositório específico
    //Pois como ele implementa IRepository ele contém todos
    //os métodos do repositório genérico e também o método específico
    //sendo suficiente para realizar todas as operações 
    private readonly IProdutoRepository _produtoRepository;
    // private readonly IRepository<Produto> _repository;
    public ProdutosController(
        // IRepository<Produto> repository,
        IProdutoRepository produtoRepository
    )
    {
        _produtoRepository = produtoRepository;
        // _repository = repository;
    }

    [HttpGet("produtos/{id}")]
    public ActionResult <IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
        var produtos = _produtoRepository.GetProdutosPorCategoria(id);
        
        if (produtos is null)
            return NotFound();

        return Ok(produtos);    
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _produtoRepository.GetAll();
        if (produtos is null)
        {
            return NotFound();
        }
        return Ok(produtos);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _produtoRepository.Get(c=> c.Id == id);   
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        var novoProduto = _produtoRepository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProduto.Id }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.Id)
        {
            return BadRequest();//400
        }

        var produtoAtualizado = _produtoRepository.Update(produto);

       return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _produtoRepository.Get(p => p.Id == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        var produtoDeletado = _produtoRepository.Delete(produto);
        return Ok(produtoDeletado);
    }
}