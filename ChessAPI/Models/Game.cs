using System.ComponentModel.DataAnnotations;

namespace ChessAPI.Models;

public class Game
{
    [Key]
    public int Id { get; set; }

    // Players
    public string WhitePlayer { get; set; } = null!;

    public string BlackPlayer { get; set; } = null!;

    // Game information
    public bool WhiteCheck { get; set; } = false;

    public bool BlackCheck { get; set; } = false;

    public bool WhiteCheckmate { get; set; } = false;

    public bool BlackCheckmate { get; set; } = false;

    // If it is odd then it is white's turn, otherwise black's turn
    public int TurnCount { get; set; }

    // Navigation
    public ICollection<Piece> Pieces { get; set; } = new List<Piece>();

}