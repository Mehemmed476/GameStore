using GameStore.BL.Utilities.Enums;
using GameStore.CORE.Models;
using GameStore.PL.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.PL.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public IActionResult Register()
    {
        return View();
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVm)
    {
        if (!ModelState.IsValid) { return View(registerVm); }

        AppUser user = new AppUser()
        {
            FirstName = registerVm.FirstName,
            LastName = registerVm.LastName,
            UserName = registerVm.Username,
            Email = registerVm.Email
        };
        
        await _userManager.CreateAsync(user, registerVm.Password);
        await _userManager.AddToRoleAsync(user, RoleEnums.User.ToString());
        
        return RedirectToAction(nameof(Index), "Home");
    }
    
    public IActionResult Login()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVm)
    {
        if (!ModelState.IsValid) { return View(loginVm); }
        
        AppUser? appUser = await _userManager.FindByEmailAsync(loginVm.EmailOrUserName);

        if (appUser == null)
        {
            appUser = await _userManager.FindByNameAsync(loginVm.EmailOrUserName);

            if (appUser == null)
            {
                return BadRequest();
            }
        }
        
        var result = await _signInManager.PasswordSignInAsync(appUser, loginVm.Password, loginVm.RememberMe, true);
        
        if (!result.Succeeded) { return View(loginVm); }
        
        return RedirectToAction(nameof(Index), "Home"); 
    }
    
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Index), "Home");
    }
}