using CatalogAPI.Models;
using CatalogAPI.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly CatalogDbContext _context;
        
        public ProdutosController(CatalogDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();

            if (produtos is null)
            {
                return NotFound("Produto não encontrado.");
            }
            
            return produtos;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }

            return produto;
        }
    }
}
