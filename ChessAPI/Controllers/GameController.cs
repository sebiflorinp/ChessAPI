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

    // POST: api/game
    [HttpPost]
    public async Task<ActionResult<Game>> StartGame([FromBody] GameDTO gameDto)
    {
        // Check if the model is valid
        var game = GameMapper.ToGame(gameDto);

        // Add the game to the database
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetGame", new { id = game.Id }, game);
    }
}