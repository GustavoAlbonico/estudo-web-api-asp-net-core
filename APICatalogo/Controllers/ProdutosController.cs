using APICatalogo.Filters;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public ProdutosController(AppDbContext context, ILogger<ProdutosController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // [HttpGet("{valor:alpha:length(5)}")]
    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<Produto>>> Get()
    {
        _logger.LogInformation("====================== GET produtos/ ======================");

        //além disso sempre retornar com paginação para não sobrecarregar
        var produtos = await _context.Produtos.AsNoTracking().ToListAsync();//AsNoTracking faz mlehorar o desempenho da consulta, mas utilziar apenas para itens que não vao ser alterados

        if (produtos is null)
            return NotFound("Produto não encontrado...");

        return produtos;
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public async Task<ActionResult<Produto>> Get([FromRoute] int id, [BindRequired] string nome)//BindRequired query param obrigatorio
                                              // [FromBody]
                                              // [FromHeader]
                                              // [FromForm]
                                              // [FromQuery]
                                              // [FromRoute]
                                              // [FromServices]
                                              // [FromKeyedServices]
    {
        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        if (produto is null)
            return NotFound("Produto não encontrado...");

        return produto;
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.Id)
            return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto is null)
            return NotFound("Produto não localizado...");


        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }
}