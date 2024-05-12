namespace ChessAPI.DTOs;

public class PieceDTO
{
    public string Position { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Type { get; set; } = null!;
}