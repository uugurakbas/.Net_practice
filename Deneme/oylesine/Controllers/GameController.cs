using Microsoft.AspNetCore.Mvc;
using oylesine.Models;

namespace oylesine.Controllers;

public class GameController : Controller
{
    public IActionResult GameOne()
    {
            var allPlayers = Repository.Players;
            var random = new Random();

            if (allPlayers.Count < 2)
            {
                return View("Error"); // Yeterli oyuncu yoksa
            }

            int firstIndex = random.Next(allPlayers.Count);
            int secondIndex;

            do
            {
                secondIndex = random.Next(allPlayers.Count);
            } while (secondIndex == firstIndex);

            var selectedPlayers = new List<Player>
            {
                allPlayers[firstIndex],
                allPlayers[secondIndex]
            };

            var viewModel = new PlayersViewModel
            {
                Players = selectedPlayers
            };

            return View(viewModel);
    }

    [HttpPost]
public IActionResult Compare(int selectedPlayerId, Player player1, Player player2)
{
    string result;

    if(player1.PlayerPrice == player2.PlayerPrice){
            result = $"Eşit";
    }
    else if (player1.ID == selectedPlayerId)
    {

        result = player1.PlayerPrice > player2.PlayerPrice
            ? $"{player1.PlayerName} daha pahalı!"
            : $"{player1.PlayerName} daha ucuz!";
    }
    else
    {
        result = player2.PlayerPrice > player1.PlayerPrice
            ? $"{player2.PlayerName} daha pahalı!"
            : $"{player2.PlayerName} daha ucuz!";
    }

    var viewModel = new PlayersViewModel
    {
        Players = new List<Player> { player1, player2 }
    };

    ViewBag.ComparisonResult = result;

    return View("GameOne", viewModel); // Game1.cshtml sayfasını yükle ama aynı oyuncularla
}
}