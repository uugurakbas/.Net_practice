using Microsoft.AspNetCore.Mvc;

namespace FootballGame.Controllers;

public class GameController : Controller
{
    public IActionResult Index(){
        return View();
    }
}