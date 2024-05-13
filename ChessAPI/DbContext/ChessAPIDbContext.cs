using ChessAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChessAPI.DbContext;

public class ChessAPIDbContext: Microsoft.EntityFrameworkCore.DbContext
{
    // Define the tables
    public DbSet<Game> Games { get; set; }
    public DbSet<Piece> Pieces { get; set; }

    // Connect to the database
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Server=monorail.proxy.rlwy.net;Port=53090;Database=railway;Uid=root;Pwd=pOrsYrQUgzEqdoBNXsQaaZtGwXPrdLNd;");
    }

    // Define the relationships between the tables
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One to many relationship
        // A game can have many pieces
        modelBuilder.Entity<Game>()
            .HasMany(g => g.Pieces)
            .WithOne(p => p.Game)
            .HasForeignKey(p => p.GameId)
            .OnDelete(DeleteBehavior.Cascade) // When a game is deleted the pieces are deleted as well.
            .IsRequired();

        // A piece belongs to a single game
        modelBuilder.Entity<Piece>()
            .HasOne(p => p.Game)
            .WithMany(g => g.Pieces)
            .HasForeignKey(p => p.GameId)
            .IsRequired();
    }

}