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

    public static bool IsCheckmate(List<Piece> pieces, string color)
    {
        // Get the pieces of the given color
        List<Piece> piecesOfACertainColor = pieces.Where(p => p.Color == color).ToList();

        // Find the valid moves of the pieces
        List<Move> validMoves = new();
        foreach (Piece piece in piecesOfACertainColor)
        {
            validMoves.AddRange(piece.GetPossibleMoves(pieces));
        }

        // If there are no valid moves then it is a checkmate
        return validMoves.Count == 0;
    }
}