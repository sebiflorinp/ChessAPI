using ChessAPI.Models;

namespace ChessAPI.Extensions;

public static class GameExtensions
{
    public static bool IsCheck(List<Piece> pieces, string color)
    {
        // Check the covered squares of the pieces with the given color
        List<string> coveredSquares = new();
        foreach (Piece piece in pieces.Where(p => p.Color == color))
        {
            // Get the covered squares of the piece
            coveredSquares.AddRange(piece.GetCoveredSquares(pieces));
        }

        // Get the opponent's king
        Piece king = pieces.FirstOrDefault(p => p.Color != color && p.Type == "King");

        // Check if the king is in the covered squares
        return coveredSquares.Contains($"{king.File}{king.Rank}");
    }
}