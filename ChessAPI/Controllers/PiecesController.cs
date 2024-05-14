using ChessAPI.DbContext;
using ChessAPI.DTOs;
using ChessAPI.Extensions;
using ChessAPI.Mappers;
using ChessAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChessAPI.Controllers;

[ApiController]
[Route("api/Games/{gameId}/[controller]")]
public class PiecesController: ControllerBase
{

    private readonly ChessAPIDbContext _context = new();
    private readonly List<string> _typesOfPieces = new List<string>(){"Pawn", "Rook", "Knight", "Bishop", "Queen", "King"};

    // Get a piece by id
    // Used to get the information about a certain piece
    // GET: api/Games/{gameId}/Pieces/{pieceId}
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

    // Get all the pieces of a game
    // Used to draw the board by knowing the position of each piece
    // GET: api/Games/{gameId}/Pieces
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

    // Create a new piece
    // Is supposed to be used by the FE to create the pieces at the beginning of the game in order to preserve SRP,
    // it doesn't allow the user to add too many pieces of a certain type and color
    // POST: api/Games/{gameId}/Pieces
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

    // Delete a piece
    // Used to delete a piece when it is captured (it is already done by the UpdatePiece endpoint but this endpoint is needed if SRP is to be respected)
    // DELETE: api/Games/{gameId}/Pieces/{pieceId}
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

        return NoContent();
    }

    // Update a piece
    // Used for moving a piece, it also checks that the move is possible, doesn't result in a check and is moved by the correct player
    // Single Responsibility Principle is broken because the endpoint also deletes the captured piece.
    // It is the responsibility of the FE to update the turn counter
    // PUT: api/Games/{gameId}/Pieces/{pieceId}
    [HttpPut("{pieceId}")]
    public async Task<ActionResult<Piece>> UpdatePiece([FromBody] Move move, int gameId, int pieceId)
    {
        // Check if the game exists
        if (!_context.Games.Any(g => g.Id == gameId))
        {
            return NotFound("Game does not exits");
        }

        // Get all the pieces of the game to avoid excessive calls to the db
        var pieces = _context.Pieces
            .Where(p => p.GameId == gameId)
            .ToList();

        // Check if the piece exists in the game
        var piece = pieces
            .Where(p => p.GameId == gameId)
            .FirstOrDefault(p => p.Id == pieceId);

        if (piece == null)
        {
            return NotFound("Piece does not exist");
        }

        // Check if the correct player is moving depending on the turn count
        var game = _context.Games
            .FirstOrDefault(g => g.Id == gameId);
        var movedPiece = pieces
            .FirstOrDefault(p => p.Id == pieceId);
        if (game.TurnCount % 2 == 0 && movedPiece.Color != "Black" || game.TurnCount % 2 == 1 && movedPiece.Color != "White")
        {
            return BadRequest("It is not your turn");
        }

        // Get all the valid moves of the piece and check if the move is in the list of valid moves
        var validMoves = piece.GetPossibleMoves(pieces);

        // Check if there is any piece (should be the opposite color) in the final position
        var pieceInTheFinalPosition = pieces
            .FirstOrDefault(p => p.File == move.FinalPosition[0].ToString() && p.Rank == Convert.ToInt32(move.FinalPosition[1].ToString()));

        // Delete the piece if there is a piece in the final position
        if (pieceInTheFinalPosition != null)
        {
            _context.Pieces.Remove(pieceInTheFinalPosition);
        }

        // Update the piece if the move is in the list of valid moves
        piece.File = move.FinalPosition[0].ToString();
        piece.Rank = Convert.ToInt32(move.FinalPosition[1].ToString());

        await _context.SaveChangesAsync();

        return NoContent();
    }
}