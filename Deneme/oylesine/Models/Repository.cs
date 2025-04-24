namespace oylesine.Models;

public class Repository
{
    private static readonly List<Player> _players = new();

    static Repository()
    {
        _players.Add(new Player {PlayerName="Dzeko",PlayerAge = 39, PlayerPrice = 10000,PlayerCountry="Bosna",PlayerImage = "dzeko.jpg"});
        _players.Add(new Player {PlayerName="İrfan",PlayerAge = 27, PlayerPrice = 9000,PlayerCountry="Türkiye",PlayerImage="irfan.jpg"});
    }

    public static List<Player> Players
    {
        get
        {
            return _players;
        }
    }
}