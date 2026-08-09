using Desafio.Data;
using Desafio.Models;
using Desafio.ViewModels;
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
    // GET: /Produto
    // GET: /Produto
    public async Task<IActionResult> Index(
        string? busca,
        string? ordenacao,
        int pagina = 1)
    {
        const int tamanhoPagina = 5;

        var consulta = _context.Produtos
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            busca = busca.Trim();

            consulta = consulta.Where(
                produto => produto.Nome.Contains(busca));
        }

        var totalProdutos = await consulta.CountAsync();

        var ordenacaoAtual = ordenacao switch
        {
            "nome_desc" => "nome_desc",
            "preco_asc" => "preco_asc",
            "preco_desc" => "preco_desc",
            _ => "nome_asc"
        };

        consulta = ordenacaoAtual switch
        {
            "nome_desc" => consulta
                .OrderByDescending(produto => produto.Nome)
                .ThenBy(produto => produto.Id),

            "preco_asc" => consulta
                .OrderBy(produto => produto.Preco)
                .ThenBy(produto => produto.Nome)
                .ThenBy(produto => produto.Id),

            "preco_desc" => consulta
                .OrderByDescending(produto => produto.Preco)
                .ThenBy(produto => produto.Nome)
                .ThenBy(produto => produto.Id),

            _ => consulta
                .OrderBy(produto => produto.Nome)
                .ThenBy(produto => produto.Id)
        };

        var totalPaginas = (int)Math.Ceiling(
            totalProdutos / (double)tamanhoPagina);

        if (pagina < 1)
        {
            pagina = 1;
        }

        if (totalPaginas == 0)
        {
            pagina = 1;
        }
        else if (pagina > totalPaginas)
        {
            pagina = totalPaginas;
        }

        var produtos = await consulta
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();

        var viewModel = new ProdutoIndexViewModel
        {
            Produtos = produtos,
            Busca = busca,
            Ordenacao = ordenacaoAtual,
            PaginaAtual = pagina,
            TotalPaginas = totalPaginas,
            TotalProdutos = totalProdutos
        };

        return View(viewModel);
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
