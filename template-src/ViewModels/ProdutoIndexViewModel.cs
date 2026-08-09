using Desafio.Models;

namespace Desafio.ViewModels;

public class ProdutoIndexViewModel
{
    public List<Produto> Produtos { get; set; } = new();

    public string? Busca { get; set; }

    public string Ordenacao { get; set; } = "nome_asc";

    public int PaginaAtual { get; set; } = 1;

    public int TotalPaginas { get; set; }

    public int TotalProdutos { get; set; }

    public bool TemPaginaAnterior => PaginaAtual > 1;

    public bool TemProximaPagina => PaginaAtual < TotalPaginas;
}
