namespace oylesine.Models;

public class Player{
    
    public int ID { get; set; }
    public string? PlayerName { get; set; }
    public int PlayerAge { get; set; }

    public int PlayerPrice { get; set; }

    public string? PlayerCountry { get; set; }
    public string? PlayerPosition { get; set; }

    public string PlayerImage { get; set; } = string.Empty;
}