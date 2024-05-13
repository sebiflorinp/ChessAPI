using ChessAPI.Models;

namespace ChessAPI.Utils;

public static class Moves
{
    public static List<string> PositionsCovered(this Piece piece, List<Piece> pieces)
    {
        List<string> positions = new();

        switch (piece.Type)
        {
            case "Pawn":
                // Check the upper diagonal squares and ensure that the pawn doesn't go out of the board
                if (piece.Color == "White")
                {
                    if (piece.Rank < 8)
                    {
                        if (piece.File[0] < 'h')
                        {
                            positions.Add($"{(char)(piece.File[0] + 1)}{piece.Rank + 1}");
                        }
                        if (piece.File[0] > 'a')
                        {
                            positions.Add($"{(char)(piece.File[0] - 1)}{piece.Rank + 1}");
                        }
                    }
                }
                else
                {
                    if (piece.Rank > 1)
                    {
                        if (piece.File[0] < 'h')
                        {
                            positions.Add($"{(char)(piece.File[0] + 1)}{piece.Rank - 1}");
                        }
                        if (piece.File[0] > 'a')
                        {
                            positions.Add($"{(char)(piece.File[0] - 1)}{piece.Rank - 1}");
                        }
                    }
                }
                break;

            case "Rook":
                // Check the upper squares on the same file
                for (int i = piece.Rank + 1; i <= 8; i++)
                {
                    positions.Add($"{piece.File}{i}");
                    if (pieces.Any(p => p.File == piece.File && p.Rank == i))
                    {
                        break;
                    }
                }

                // Check the lower squares on the same file
                for (int i = piece.Rank - 1; i >= 1; i--)
                {
                    positions.Add($"{piece.File}{i}");
                    if (pieces.Any(p => p.File == piece.File && p.Rank == i))
                    {
                        break;
                    }
                }

                // Check the squares on the same rank to the right
                for (char i = (char)(piece.File[0] + 1); i <= 'h'; i++)
                {
                    positions.Add($"{i}{piece.Rank}");
                    if (pieces.Any(p => p.File[0] == i && p.Rank == piece.Rank))
                    {
                        break;
                    }
                }

                // Check the squares on the same rank to the left
                for (char i = (char)(piece.File[0] - 1); i >= 'a'; i--)
                {
                    positions.Add($"{i}{piece.Rank}");
                    if (pieces.Any(p => p.File[0] == i && p.Rank == piece.Rank))
                    {
                        break;
                    }
                }
                break;

            case "Knight":
                // Check the upper squares
                if (piece.Rank < 7)
                {
                    if (piece.File[0] < 'h')
                    {
                        positions.Add($"{piece.File[0] + 1}{piece.Rank + 2}");
                    }

                    if (piece.File[0] > 'a')
                    {
                        positions.Add($"{piece.File[0] - 1}{piece.Rank + 2}");
                    }
                }

                // Check the lower squares
                if (piece.Rank > 2)
                {
                    if (piece.File[0] < 'h')
                    {
                        positions.Add($"{piece.File[0] + 1}{piece.Rank - 2}");
                    }

                    if (piece.File[0] > 'a')
                    {
                        positions.Add($"{piece.File[0] - 1}{piece.Rank - 2}");
                    }
                }

                // Check the left squares
                if (piece.File[0] > 'b')
                {
                    if (piece.Rank < 8)
                    {
                        positions.Add($"{piece.File[0] - 2}{piece.Rank + 1}");
                    }

                    if (piece.Rank > 1)
                    {
                        positions.Add($"{piece.File[0] - 2}{piece.Rank - 1}");
                    }
                }

                // Check the right squares
                if (piece.File[0] < 'g')
                {
                    if (piece.Rank < 8)
                    {
                        positions.Add($"{piece.File[0] + 2}{piece.Rank + 1}");
                    }

                    if (piece.Rank > 1)
                    {
                        positions.Add($"{piece.File[0] + 2}{piece.Rank - 1}");
                    }
                }
                break;

            case "Bishop":
                // Check the first upper diagonal squares
                int counter = 1;
                for (int i = piece.Rank + 1; i <= 8; i++)
                {
                    if (piece.File[0] + counter <= 'h')
                    {
                        positions.Add($"{piece.File[0] + counter}{i}");
                        counter++;
                        if (pieces.Any(p => p.File[0] == piece.File[0] + counter && p.Rank == i))
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // Check the second upper diagonal squares
                counter = 1;
                for (int i = piece.Rank + 1; i <= 8; i++)
                {
                    if (piece.File[0] - counter >= 'a')
                    {
                        positions.Add($"{piece.File[0] - counter}{i}");
                        counter++;
                        if (pieces.Any(p => p.File[0] == piece.File[0] - counter && p.Rank == i))
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // Check the first lower diagonal squares
                counter = 1;
                for (int i = piece.Rank - 1; i >= 1; i--)
                {
                    if (piece.File[0] + counter <= 'h')
                    {
                        positions.Add($"{piece.File[0] + counter}{i}");
                        counter++;
                        if (pieces.Any(p => p.File[0] == piece.File[0] + counter && p.Rank == i))
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // Check the second lower diagonal squares
                counter = 1;
                for (int i = piece.Rank - 1; i >= 1; i--)
                {
                    if (piece.File[0] - counter >= 'a')
                    {
                        positions.Add($"{piece.File[0] - counter}{i}");
                        counter++;
                        if (pieces.Any(p => p.File[0] == piece.File[0] - counter && p.Rank == i))
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            case "Queen":
                // Queen covered positions = Rook covered positions + Bishop covered positions
                // Add the positions covered if the Queen were a Rook
                piece.Type = "Rook";
                positions.AddRange(piece.PositionsCovered(pieces));

                // Add the positions covered if the Queen were a Bishop
                piece.Type = "Bishop";
                positions.AddRange(piece.PositionsCovered(pieces));
                break;

            case "King":
                // Check the upper squares
                if (piece.Rank < 8)
                {
                    positions.Add($"{piece.File}{piece.Rank + 1}");

                    if (piece.File[0] < 'h')
                    {
                        positions.Add($"{(char)(piece.File[0] + 1)}{piece.Rank + 1}");
                    }

                    if (piece.File[0] > 'a')
                    {
                        positions.Add($"{(char)(piece.File[0] - 1)}{piece.Rank + 1}");
                    }
                }

                // Check the lower squares
                if (piece.Rank > 1)
                {
                    positions.Add($"{piece.File}{piece.Rank - 1}");

                    if (piece.File[0] < 'h')
                    {
                        positions.Add($"{(char)(piece.File[0] + 1)}{piece.Rank - 1}");
                    }

                    if (piece.File[0] > 'a')
                    {
                        positions.Add($"{(char)(piece.File[0] - 1)}{piece.Rank - 1}");
                    }
                }

                // Check the remaining left square
                if (piece.File[0] > 'a')
                {
                    positions.Add($"{(char)(piece.File[0] - 1)}{piece.Rank}");
                }

                // Check the remaining right square
                if (piece.File[0] < 'h')
                {
                    positions.Add($"{(char)(piece.File[0] + 1)}{piece.Rank}");
                }
                break;
        }

        return positions;
    }
}