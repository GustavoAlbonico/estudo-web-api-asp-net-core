using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // [HttpGet("{valor:alpha:length(5)}")]
    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        try
        {
            //além disso sempre retornar com paginação para não sobrecarregar
            var produtos = _context.Produtos.AsNoTracking().ToList();//AsNoTracking faz mlehorar o desempenho da consulta, mas utilziar apenas para itens que não vao ser alterados

            if (produtos is null)
                return NotFound("Produto não encontrado...");

            return produtos;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }

    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        try
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if (produto is null)
                return NotFound("Produto não encontrado...");

            return produto;

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        try
        {
            if (produto is null)
                return BadRequest();

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id }, produto);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        try
        {
            if (id != produto.Id)
                return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto is null)
                return NotFound("Produto não localizado...");


            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
    }
}