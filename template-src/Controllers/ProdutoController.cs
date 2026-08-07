using Microsoft.AspNetCore.Mvc;

namespace Desafio.Controllers;

/// <summary>
/// Controller de Produtos.
///
/// As ações estão intencionalmente vazias. Cabe ao candidato(a) implementá-las,
/// definir as assinaturas necessárias (parâmetros, verbos HTTP, POSTs de
/// confirmação) e escolher como o acesso a dados será feito.
///
/// Consulte docs/desafio.md para a lista completa de requisitos.
/// </summary>
public class ProdutoController : Controller
{
    // GET: /Produto
    // TODO: listar produtos, com busca por nome e ordenação por nome e preço.
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Produto/Create
    // TODO: exibir o formulário e persistir o novo produto.
    public IActionResult Create()
    {
        return View();
    }

    // GET: /Produto/Edit/5
    // TODO: carregar o produto existente e salvar as alterações.
    public IActionResult Edit(int id)
    {
        return View();
    }

    // GET: /Produto/Details/5
    // TODO: exibir os dados completos do produto.
    public IActionResult Details(int id)
    {
        return View();
    }

    // GET: /Produto/Delete/5
    // TODO: confirmar e excluir o produto.
    public IActionResult Delete(int id)
    {
        return View();
    }
}
