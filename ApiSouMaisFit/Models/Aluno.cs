using System.ComponentModel.DataAnnotations;

namespace ApiSouMaisFit.Models;

public class Aluno
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [StringLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "A idade é obrigatória")]
    [Range(1, 100, ErrorMessage = "A idade deve ser entre 1 e 100 anos")]
    public int Idade { get; set; }
}
