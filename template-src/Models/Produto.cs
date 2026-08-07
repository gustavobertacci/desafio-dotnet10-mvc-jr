using System.ComponentModel.DataAnnotations;

namespace Desafio.Models;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    [Display(Name = "Nome")]
    public string Nome { get; set; } = string.Empty;

    [Display(Name = "Descrição")]
    public string? Descricao { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    [Display(Name = "Preço")]
    public decimal Preco { get; set; }

    [Display(Name = "Data de Cadastro")]
    [DataType(DataType.DateTime)]
    public DateTime DataCadastro { get; set; }
}
