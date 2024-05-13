namespace ChessAPI.Models;

public class Move
{
    public string InitialPosition { get; set; }

    public string FinalPosition { get; set; }

    public Move(string initialPosition, string finalPosition)
    {
        InitialPosition = initialPosition;
        FinalPosition = finalPosition;
    }

    public Move(string initialFile, int initialRank, string finalFile, int finalRank)
    {
        InitialPosition = initialFile + initialRank;
        FinalPosition = finalFile + finalRank;
    }
    public Move()
    {

    }

}