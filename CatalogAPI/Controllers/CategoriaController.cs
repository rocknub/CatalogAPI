using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CatalogDbContext? _context;

        public CategoriaController(CatalogDbContext context)
        {
            _context = context;
        }

        [HttpGet("listarCategoriasEProdutos/{filtroQuantidade:int}")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos(int filtroQuantidade)
        {
            //return _context.Categorias.Include(p => p.Produtos).ToList();
            return _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= filtroQuantidade).ToList();
        }


        [HttpGet("listarApenasCategoria")]
        public ActionResult<IEnumerable<Categoria>> GetOptimized()
        {
            //AsNoTracking() apenas para consultar que apenas leem sem alterar os dados - logo menos consumo na memoria.
            return _context.Categorias.AsNoTracking().ToList();
        }

        [HttpGet("categoriaEspecifica/{id:int}", Name="ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada. Tente novamente.");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada. Tente novamente.");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }
}
