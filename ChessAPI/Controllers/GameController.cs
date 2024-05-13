using ChessAPI.DbContext;
using ChessAPI.DTOs;
using ChessAPI.Mappers;
using ChessAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChessAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController: ControllerBase
{
    private readonly ChessAPIDbContext _context = new();

    // Used for getting information about a game
    // GET: api/game/id
    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetGame(int id)
    {
        var game = await _context.Games.FindAsync(id);

        if (game == null)
        {
            return NotFound();
        }

        return game;
    }

    // Used for creating a new game
    // Single Responsibility Principle is broken because this endpoint also creates the pieces required to start a game of chess
    // POST: api/game
    [HttpPost]
    public async Task<IActionResult> StartGame([FromBody] GameDTO gameDto)
    {
        // Check if the model is valid
        var game = GameMapper.ToGame(gameDto);

        // Add the game to the database
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();

        // Add the pieces required to start a game of chess
        // White Pawns
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Pawn", Rank = 2, File = "a", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Pawn", Rank = 2, File = "b", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Pawn", Rank = 2, File = "c", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Pawn", Rank = 2, File = "d", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Pawn", Rank = 2, File = "e", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Pawn", Rank = 2, File = "f", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Pawn", Rank = 2, File = "g", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Pawn", Rank = 2, File = "h", GameId = game.Id});

        // White Rooks
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Rook", Rank = 1, File = "a", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Rook", Rank = 1, File = "h", GameId = game.Id});

        // White Knights
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Knight", Rank = 1, File = "b", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Knight", Rank = 1, File = "g", GameId = game.Id});

        // White Bishops
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Bishop", Rank = 1, File = "c", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Bishop", Rank = 1, File = "f", GameId = game.Id});

        // White Queen
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "Queen", Rank = 1, File = "d", GameId = game.Id});

        // White King
        await _context.Pieces.AddAsync(new Piece() {Color = "White", Type = "King", Rank = 1, File = "e", GameId = game.Id});

        // Black Pawns
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Pawn", Rank = 7, File = "a", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Pawn", Rank = 7, File = "b", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Pawn", Rank = 7, File = "c", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Pawn", Rank = 7, File = "d", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Pawn", Rank = 7, File = "e", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Pawn", Rank = 7, File = "f", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Pawn", Rank = 7, File = "g", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Pawn", Rank = 7, File = "h", GameId = game.Id});

        // Black Rooks
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Rook", Rank = 8, File = "a", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Rook", Rank = 8, File = "h", GameId = game.Id});

        // Black Knights
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Knight", Rank = 8, File = "b", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Knight", Rank = 8, File = "g", GameId = game.Id});

        // Black Bishops
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Bishop", Rank = 8, File = "c", GameId = game.Id});
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Bishop", Rank = 8, File = "f", GameId = game.Id});

        // Black Queen
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "Queen", Rank = 8, File = "d", GameId = game.Id});

        // Black King
        await _context.Pieces.AddAsync(new Piece() {Color = "Black", Type = "King", Rank = 8, File = "e", GameId = game.Id});
        _context.SaveChangesAsync();

        return Ok();
    }
}