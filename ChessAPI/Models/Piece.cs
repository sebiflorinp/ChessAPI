using System.ComponentModel.DataAnnotations;

namespace ChessAPI.Models;

public class Piece
{
    [Key]
    public int Id { get; set; }

    // Position of the piece on the board
    public string File { get; set; }

    public int Rank { get; set; }

    // Color of the piece
    public string Color { get; set; }

    // Type of the piece
    public string Type { get; set; }

    // Navigation property
    public int GameId { get; set; }

    public Game Game { get; set; }
}