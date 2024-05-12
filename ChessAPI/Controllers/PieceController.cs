using ChessAPI.DbContext;
using ChessAPI.DTOs;
using ChessAPI.Mappers;
using ChessAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChessAPI.Controllers;

[ApiController]
[Route("api/game/{gameId}/[controller]")]
public class PieceController: ControllerBase
{

    private readonly ChessAPIDbContext _context = new();
    private readonly List<string> _typesOfPieces = new List<string>(){"Pawn", "Rook", "Knight", "Bishop", "Queen", "King"};

    [HttpGet("{pieceId}")]
    public async Task<ActionResult<Piece>> GetPiece(int gameId,int pieceId)
    {
        var piece = _context.Pieces
            .Where(p => p.GameId == gameId)
            .FirstOrDefault(p => p.Id == pieceId);

        if (piece == null)
        {
            return NotFound();
        }

        return piece;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Piece>>> GetPieces(int gameId)
    {
        var pieces = _context.Pieces
            .Where(p => p.GameId == gameId)
            .ToList();

        // Check if the game exists
        if (!_context.Games.Any(g => g.Id == gameId))
        {
            return NotFound("Game does not exits");
        }

        if (pieces == null)
        {
            return NotFound();
        }

        return pieces;
    }

    [HttpPost]
    public async Task<ActionResult<Piece>> CreatePiece([FromBody] PieceDTO pieceDto, int gameId)
    {
        // Convert the DTO to a piece
        var newPiece = pieceDto.ToPiece(gameId);

        // Get all pieces of the game separately in order to avoid excessive calls to the db
        var pieces = _context.Pieces
            .Where(p => p.GameId == gameId)
            .ToList();

        // Check if the game exists
        if (!_context.Games.Any(g => g.Id == gameId))
        {
            return NotFound("Game does not exits");
        }

        // Check if the piece type is valid
        if (!_typesOfPieces.Contains(newPiece.Type))
        {
            return BadRequest("Invalid piece type");
        }

        // Check if the color is valid
        if (newPiece.Color != "White" && newPiece.Color != "Black")
        {
            return BadRequest("Invalid color");
        }

        // Check if there are not too many pieces of the same type and color

        var playerPieces = pieces
            .Where(p => p.GameId == gameId)
            .Where(p => p.Color == newPiece.Color)
            .Where(p => p.Type == newPiece.Type)
            .ToList();

        // There can be only one King and Queen of a certain color
        if (newPiece.Type is "King" or "Queen" && playerPieces.Count != 0)
        {
            return BadRequest("There can only be one king and one queen of each color");
        }

        // There can be only 2 Rooks, Knights and Bishops of a certain color
        if (newPiece.Type is "Rook" or "Knight" or "Bishop" && playerPieces.Count >= 2)
        {
            return BadRequest("There can only be 2 rooks, knights and bishops of each color");
        }

        // There can be only 8 Pawns of a certain color
        if (newPiece.Type is "Pawn" && playerPieces.Count >= 8)
        {
            return BadRequest("There can only be 8 pawns of each color");
        }

        // Check if the position is within the board
        if (!(
                newPiece != null &&
                string.Compare(newPiece.File.ToLower(), "a") >= 0 &&
                string.Compare(newPiece.File.ToLower(), "h") <= 0 &&
                newPiece.Rank >= 1 &&
                newPiece.Rank <= 8
                ))
        {
            return BadRequest("Invalid position");
        }

        // Check if the position is not already occupied
        if (pieces.Any(p => p.File == newPiece.File && p.Rank == newPiece.Rank))
        {
            return BadRequest("Position already occupied");
        }

        // Add the piece
        await _context.Pieces.AddAsync(newPiece);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPiece", new { gameId = newPiece.GameId, pieceId = newPiece.Id }, newPiece);
    }

    [HttpDelete("{pieceId}")]
    public async Task<ActionResult<Piece>> DeletePiece(int gameId, int pieceId)
    {
        var piece = _context.Pieces
            .Where(p => p.GameId == gameId)
            .FirstOrDefault(p => p.Id == pieceId);

        if (piece == null)
        {
            return NotFound();
        }

        _context.Pieces.Remove(piece);
        await _context.SaveChangesAsync();

        return piece;
    }
}