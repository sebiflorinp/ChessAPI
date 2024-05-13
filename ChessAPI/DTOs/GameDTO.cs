namespace ChessAPI.DTOs;

public class GameDTO
{
    public string WhitePlayer { get; set; } = null!;
    public string BlackPlayer { get; set; } = null!;
}

public class GameUpdateDTO
{
    public int TurnCount { get; set; }
}