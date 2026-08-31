using System.ComponentModel.DataAnnotations;

namespace LanchesMac.Models;

public class Lanche
{
    [Key]
    public int LancheId { get; set; }
    [Required(ErrorMessage = "Informe o nome do lanche")]
    [Display(Name = "Nome do Lanche")]
    public string Nome { get; set; }
    [Display(Name = "Descrição Curta")]
    public string DescricaoCurta { get; set; }
    [Display(Name = "Descrição Detalhada")]
    public string DescricaoDetalhada { get; set; }
    [Required(ErrorMessage = "Informe o preço do lanche")]
    [Display(Name = "Preço")]
    public decimal Preco { get; set; }
    [Display(Name = "URL da Imagem")]
    public string ImagemUrl { get; set; }
    [Display(Name = "URL do Thumbnail")]
    public string ImagemThumbnailUrl { get; set; }
    [Display(Name = "É um lanche preferido?")]
    public bool IsLanchePreferido { get; set; }
    [Display(Name = "Está em estoque?")]
    public bool EmEstoque { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione uma categoria")]
    public int CategoriaId { get; set; }
    public virtual Categoria Categoria { get; set; }
}
