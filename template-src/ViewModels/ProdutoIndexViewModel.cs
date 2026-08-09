using Desafio.Models;

namespace Desafio.ViewModels;

public class ProdutoIndexViewModel
{
    public List<Produto> Produtos { get; set; } = new();

    public string? Busca { get; set; }

    public string Ordenacao { get; set; } = "nome_asc";
}
