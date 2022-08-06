using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppinglistApp.Data;
using ShoppinglistApp.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ShoppinglistApp.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly UserDbContext _userContext;
    private readonly SignInManager<IdentityUser> _signInManager;



    public HomeController(ILogger<HomeController> logger, UserDbContext ucontext, SignInManager<IdentityUser> signInManager)
    {
       _logger = logger;
      _userContext = ucontext;
      _signInManager = signInManager;

    }

    public async Task<IActionResult> Index()
    {
      if (_signInManager.IsSignedIn(User))
      {
        var curUser = await _userContext.Users.FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
        var friendRequests = await _userContext.FriendRequests.ToListAsync();
        HttpContext.Session.SetString("FriendRequests", friendRequests.FindAll(x => x.TargetEmail == curUser.Email).Count.ToString());
      }

      return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
       return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
