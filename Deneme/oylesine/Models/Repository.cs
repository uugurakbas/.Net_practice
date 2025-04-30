namespace oylesine.Models;

public class Repository
{
    private static readonly List<Player> _players = new();

    static Repository()
    {
        _players.Add(new Player {ID=0, PlayerName="Dzeko",PlayerAge = 39, PlayerPrice = 10000,PlayerCountry="ba",PlayerImage = "dzeko.jpg",PlayerPosition="forvet"});
        _players.Add(new Player {ID=1, PlayerName="İrfan",PlayerAge = 27, PlayerPrice = 9000,PlayerCountry="tr",PlayerImage="irfan.jpg",PlayerPosition="orta saha"});
        _players.Add(new Player {ID=2, PlayerName="zrfan",PlayerAge = 27, PlayerPrice = 9000,PlayerCountry="tr",PlayerImage="irfan.jpg",PlayerPosition="defans"});
        _players.Add(new Player {ID=3, PlayerName="krfan",PlayerAge = 27, PlayerPrice = 9000,PlayerCountry="tr",PlayerImage="irfan.jpg",PlayerPosition="kanat"});
        _players.Add(new Player {ID=4, PlayerName="lrfan",PlayerAge = 27, PlayerPrice = 9000,PlayerCountry="tr",PlayerImage="irfan.jpg",PlayerPosition="forvet"});
    }

    public static List<Player> Players
    {
        get
        {
            return _players;
        }
    }
}