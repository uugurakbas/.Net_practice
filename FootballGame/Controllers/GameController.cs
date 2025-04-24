using Microsoft.AspNetCore.Mvc;

namespace FootballGame.Controllers;

public class GameController : Controller
{

        public IActionResult HGame()
    {
        return View();
    }

    public JsonResult GetTeams(string league)
    {
        List<string> teams = new List<string>();

        switch (league)
        {
            case "superlig":
                teams = new List<string> { "Galatasaray", "Fenerbahçe", "Beşiktaş", "Trabzonspor" };
                break;
            case "premierleague":
                teams = new List<string> { "Manchester City", "Arsenal", "Liverpool", "Chelsea" };
                break;
            case "laliga":
                teams = new List<string> { "Real Madrid", "Barcelona", "Atletico Madrid", "Sevilla" };
                break;
        }

        return Json(teams);
    }
}