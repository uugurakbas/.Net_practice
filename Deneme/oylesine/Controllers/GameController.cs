using Microsoft.AspNetCore.Mvc;
using oylesine.Models;

namespace oylesine.Controllers;

public class GameController : Controller
{
    public IActionResult GameOne()
    {
        var model = new PlayersViewModel
        {
            Players = Repository.Players
        };

        return View(model);
    }

        [HttpPost]
        public IActionResult Compare(int selectedPlayerId)
        {
            var players = Repository.Players;
            var selected = players.FirstOrDefault(p => p.ID == selectedPlayerId);
            var other = players.FirstOrDefault(p => p.ID != selectedPlayerId);

            if (selected == null || other == null)
            {
                Console.WriteLine("ss");
                return RedirectToAction("GameOne");

            }

            string resultMessage = selected.PlayerPrice > other.PlayerPrice
                ? $"{selected.PlayerName} daha pahalı!"
                : $"{other.PlayerName} daha pahalı!";

            ViewBag.ComparisonResult = resultMessage;

            var model = new PlayersViewModel
            {
                Players = players
            };
            Console.WriteLine("KK");
            return View("GameOne", model); // Index sayfasına yönlendirir
        }

}