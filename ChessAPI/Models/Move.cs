namespace ChessAPI.Models;

public class Move(string initialFile, int initialRank, string finalFile, int finalRank)
{
    public string InitialPosition { get; set; } = initialFile + initialRank;

    public string FinalPosition { get; set; } = finalFile + finalRank;
}