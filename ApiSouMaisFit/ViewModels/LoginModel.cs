using System.ComponentModel.DataAnnotations;

namespace ApiSouMaisFit.ViewModels;

public class LoginModel
{
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "O email é inválido")]
    [StringLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [DataType(DataType.Password)]
    [StringLength(20, ErrorMessage = "A senha deve ter no máximo 20 caracteres")]
    public string Password { get; set; }
}
