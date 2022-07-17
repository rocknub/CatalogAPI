using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly CatalogDbContext? _context;

        public ProdutosController(CatalogDbContext? context)
        {
            _context = context;
        }

        [HttpGet("get/listarTP")]
        public ActionResult<IEnumerable<Produto>> GetOptimized()
        {
            try
            {
                return _context.Produtos.AsNoTracking().ToList();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema com o servidor ao tratar sua solicitação. " +
                    "Tente novamente em alguns minutos, caso erro persista contate a TI.");
            }
            //AsNoTracking() apenas para consultar que apenas leem sem alterar os dados - logo menos consumo na memoria.
        }

        [HttpGet("get/listarP/quantidade/{filtroQuantidade:int}")]
        public ActionResult<IEnumerable<Produto>> GetFilter(int filtroQuantidade)
        {
            if (filtroQuantidade is int)
            {
                var produtos = _context.Produtos.Take(filtroQuantidade).ToList();

                if (produtos is null)
                {
                    return NotFound("Produtos não encontrado.");
                }
                return produtos;
            }
            else
            {
                return BadRequest("Por favor digite um numero inteiro sem usar nenhum ponto ou vírgula.");
            }
        }

        [HttpGet("get/listarPperId//{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            
            if (produto is null)
            {
                return NotFound("Produto não encontrado. Tente novamente.");
            }
            return produto;
        }

        [HttpPost("post/")]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("put/{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            //var produto = _context.Produtos.Find(id); Vantagem de usar Find é primeiro localizar o produto na memoria mas o id precisa estar defenido.

            if (produto is null)
            {
                return NotFound("Produto não encontrado. Tente novamente.");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}
