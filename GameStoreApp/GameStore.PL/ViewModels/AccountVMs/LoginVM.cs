using System.ComponentModel.DataAnnotations;

namespace GameStore.PL.ViewModels.AccountVMs;

public class LoginVM
{
    [Required]
    [Display(Prompt = "Email or Username")]
    public string EmailOrUserName { get; set; }
    
    [DataType(DataType.Password), Required]
    [Display(Prompt = "Password")]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}