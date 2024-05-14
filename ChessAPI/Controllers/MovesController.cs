using ChessAPI.DbContext;
using ChessAPI.Extensions;
using ChessAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChessAPI.Controllers;

[ApiController]
[Route("api/Games/{gameId}/piece/{pieceId}/[controller]")]
public class MovesController: ControllerBase
{
    private readonly ChessAPIDbContext _context = new();

    // Get all the possible moves of a piece, it is assumed that the moves are played on the player's turn
    // GET: api/Games/{gameId}/Pieces/{pieceId}/Moves
    [HttpGet]
    public async Task<ActionResult<List<Move>>> GetMovesByPieceId(int gameId, int pieceId)
    {
        // Get all the pieces of the given game
        List<Piece> pieces = await _context.Pieces.Where(p => p.GameId == gameId).ToListAsync();

        // Check if the game exists
        if (pieces.Count == 0)
        {
            return NotFound();
        }

        // Get the piece with the given id
        Piece piece = pieces.FirstOrDefault(p => p.Id == pieceId);

        // Check if the piece exists
        if (piece == null)
        {
            return NotFound();
        }

        // Get the moves of the piece
        List<Move> moves = piece.GetPossibleMoves(pieces);

        return Ok(moves);
    }

}