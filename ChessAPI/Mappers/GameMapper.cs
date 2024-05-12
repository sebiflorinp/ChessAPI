using ChessAPI.DTOs;
using ChessAPI.Models;

namespace ChessAPI.Mappers;

public static class GameMapper
{
    public static GameDTO ToGameDto(this Game game)
    {
        return new GameDTO { BlackPlayer = game.BlackPlayer, WhitePlayer = game.WhitePlayer };
    }

    public static Game ToGame(this GameDTO gameDto)
    {
        return new Game { BlackPlayer = gameDto.BlackPlayer, WhitePlayer = gameDto.WhitePlayer, TurnCount = 1 };
    }
}