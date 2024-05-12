using ChessAPI.DTOs;
using ChessAPI.Models;

namespace ChessAPI.Mappers;

public static class PieceMapper
{
    public static PieceDTO ToPieceDto(this Piece piece)
    {
        return new PieceDTO { Position = $"{piece.File}{piece.Rank}", Color = piece.Color, Type = piece.Type };
    }

    public static Piece ToPiece(this PieceDTO pieceDto, int gameId)
    {
        return new Piece
        {
            Color = pieceDto.Color,
            File = pieceDto.Position[0].ToString(),
            Rank = (int)Convert.ToUInt32(pieceDto.Position[1].ToString()),
            Type = pieceDto.Type,
            GameId = gameId
        };
    }
}