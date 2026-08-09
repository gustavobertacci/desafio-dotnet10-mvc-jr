using Desafio.Data;
using Desafio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Controllers;

public class ProdutoController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProdutoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Produto
    public async Task<IActionResult> Index()
    {
        var produtos = await _context.Produtos
            .AsNoTracking()
            .ToListAsync();

        return View(produtos);
    }

    // GET: /Produto/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Produto/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Nome,Descricao,Preco")] Produto produto)
    {
        if (!ModelState.IsValid)
        {
            return View(produto);
        }

        produto.DataCadastro = DateTime.Now;

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Produto cadastrado com sucesso.";

        return RedirectToAction(nameof(Index));
    }

    // GET: /Produto/Edit/5
    // GET: /Produto/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var produto = await _context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(produto => produto.Id == id);

        if (produto is null)
        {
            return NotFound();
        }

        return View(produto);
    }

    // POST: /Produto/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Nome,Descricao,Preco")] Produto produto)
    {
        if (id != produto.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(produto);
        }

        var produtoExistente = await _context.Produtos.FindAsync(id);

        if (produtoExistente is null)
        {
            return NotFound();
        }

        produtoExistente.Nome = produto.Nome;
        produtoExistente.Descricao = produto.Descricao;
        produtoExistente.Preco = produto.Preco;

        await _context.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Produto editado com sucesso.";

        return RedirectToAction(nameof(Index));
    }

    // GET: /Produto/Details/5
    // GET: /Produto/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var produto = await _context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(produto => produto.Id == id);

        if (produto is null)
        {
            return NotFound();
        }

        return View(produto);
    }

    // GET: /Produto/Delete/5
    // GET: /Produto/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _context.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(produto => produto.Id == id);

        if (produto is null)
        {
            return NotFound();
        }

        return View(produto);
    }

    // POST: /Produto/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto is null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Produto excluído com sucesso.";

        return RedirectToAction(nameof(Index));
    }
}
