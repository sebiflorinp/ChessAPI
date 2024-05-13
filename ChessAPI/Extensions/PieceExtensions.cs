using ChessAPI.Models;

namespace ChessAPI.Extensions;

public static class PieceExtensions
{
   public static List<string> GetCoveredSquares(this Piece piece, List<Piece> pieces)
    {
        List<string> squares = new List<string>();

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
                            squares.Add($"{(char)(piece.File[0] + 1)}{piece.Rank + 1}");
                        }
                        if (piece.File[0] > 'a')
                        {
                            squares.Add($"{(char)(piece.File[0] - 1)}{piece.Rank + 1}");
                        }
                    }
                }
                else
                {
                    if (piece.Rank > 1)
                    {
                        if (piece.File[0] < 'h')
                        {
                            squares.Add($"{(char)(piece.File[0] + 1)}{piece.Rank - 1}");
                        }
                        if (piece.File[0] > 'a')
                        {
                            squares.Add($"{(char)(piece.File[0] - 1)}{piece.Rank - 1}");
                        }
                    }
                }
                break;

            case "Rook":
                // Check the upper squares on the same file
                for (int i = piece.Rank + 1; i <= 8; i++)
                {
                    squares.Add($"{piece.File}{i}");
                    if (pieces.Any(p => p.File == piece.File && p.Rank == i))
                    {
                        break;
                    }
                }

                // Check the lower squares on the same file
                for (int i = piece.Rank - 1; i >= 1; i--)
                {
                    squares.Add($"{piece.File}{i}");
                    if (pieces.Any(p => p.File == piece.File && p.Rank == i))
                    {
                        break;
                    }
                }

                // Check the squares on the same rank to the right
                for (char i = (char)(piece.File[0] + 1); i <= 'h'; i++)
                {
                    squares.Add($"{i}{piece.Rank}");
                    if (pieces.Any(p => p.File[0] == i && p.Rank == piece.Rank))
                    {
                        break;
                    }
                }

                // Check the squares on the same rank to the left
                for (char i = (char)(piece.File[0] - 1); i >= 'a'; i--)
                {
                    squares.Add($"{i}{piece.Rank}");
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
                        squares.Add($"{(char)(piece.File[0] + 1)}{piece.Rank + 2}");
                    }

                    if (piece.File[0] > 'a')
                    {
                        squares.Add($"{(char)(piece.File[0] - 1)}{piece.Rank + 2}");
                    }
                }

                // Check the lower squares
                if (piece.Rank > 2)
                {
                    if (piece.File[0] < 'h')
                    {
                        squares.Add($"{(char)(piece.File[0] + 1)}{piece.Rank - 2}");
                    }

                    if (piece.File[0] > 'a')
                    {
                        squares.Add($"{(char)(piece.File[0] - 1)}{piece.Rank - 2}");
                    }
                }

                // Check the left squares
                if (piece.File[0] > 'b')
                {
                    if (piece.Rank < 8)
                    {
                        squares.Add($"{(char)(piece.File[0] - 2)}{piece.Rank + 1}");
                    }

                    if (piece.Rank > 1)
                    {
                        squares.Add($"{(char)(piece.File[0] - 2)}{piece.Rank - 1}");
                    }
                }

                // Check the right squares
                if (piece.File[0] < 'g')
                {
                    if (piece.Rank < 8)
                    {
                        squares.Add($"{(char)(piece.File[0] + 2)}{piece.Rank + 1}");
                    }

                    if (piece.Rank > 1)
                    {
                        squares.Add($"{(char)(piece.File[0] + 2)}{piece.Rank - 1}");
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
                        squares.Add($"{(char)(piece.File[0] + counter)}{i}");
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
                        squares.Add($"{(char)(piece.File[0] - counter)}{i}");
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
                        squares.Add($"{(char)(piece.File[0] + counter)}{i}");
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
                        squares.Add($"{(char)(piece.File[0] - counter)}{i}");
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
                squares.AddRange(piece.GetCoveredSquares(pieces));

                // Add the positions covered if the Queen were a Bishop
                piece.Type = "Bishop";
                squares.AddRange(piece.GetCoveredSquares(pieces));
                break;

            case "King":
                // Check the upper squares
                if (piece.Rank < 8)
                {
                    squares.Add($"{piece.File}{piece.Rank + 1}");

                    if (piece.File[0] < 'h')
                    {
                        squares.Add($"{(char)(piece.File[0] + 1)}{piece.Rank + 1}");
                    }

                    if (piece.File[0] > 'a')
                    {
                        squares.Add($"{(char)(piece.File[0] - 1)}{piece.Rank + 1}");
                    }
                }

                // Check the lower squares
                if (piece.Rank > 1)
                {
                    squares.Add($"{piece.File}{piece.Rank - 1}");

                    if (piece.File[0] < 'h')
                    {
                        squares.Add($"{(char)(piece.File[0] + 1)}{piece.Rank - 1}");
                    }

                    if (piece.File[0] > 'a')
                    {
                        squares.Add($"{(char)(piece.File[0] - 1)}{piece.Rank - 1}");
                    }
                }

                // Check the remaining left square
                if (piece.File[0] > 'a')
                {
                    squares.Add($"{(char)(piece.File[0] - 1)}{piece.Rank}");
                }

                // Check the remaining right square
                if (piece.File[0] < 'h')
                {
                    squares.Add($"{(char)(piece.File[0] + 1)}{piece.Rank}");
                }
                break;
        }
        return squares;
    }

   public static List<Move> GetPossibleMoves(this Piece piece, List<Piece> pieces)
    {
        // Special case a pawn, covered squares cannot be converted into moves unless there is a piece to capture
        List<string> coveredSquares;
        List<Move> potentialMoves;
        List<Move> moves;
        if (piece.Type == "Pawn")
        {
            coveredSquares = piece.GetCoveredSquares(pieces);

            // Delete the squares that are not occupied by pieces of the opposite color
            coveredSquares = coveredSquares.Where(square => pieces.Any(p => p.File == square[0].ToString() && p.Rank == Convert.ToInt32(square[1].ToString()) && p.Color != piece.Color)).ToList();

            // Convert the covered squares into a list of potential moves
            potentialMoves = coveredSquares.Select(square => new Move(piece.File, piece.Rank, square[0].ToString(), Convert.ToInt32(square[1].ToString()))).ToList();

            // Depending on the color check if the pawn can be moved 2 squares forward
            if (piece.Color == "White")
            {
                if (piece.Rank == 2)
                {
                    if (!pieces.Any(p => p.File == piece.File && p.Rank == 3) && !pieces.Any(p => p.File == piece.File && p.Rank == 4))
                    {
                        potentialMoves.Add(new Move(piece.File, piece.Rank, piece.File, 4));
                    }
                }
            }
            else
            {
                if (piece.Rank == 7)
                {
                    if (!pieces.Any(p => p.File == piece.File && p.Rank == 6) && !pieces.Any(p => p.File == piece.File && p.Rank == 5))
                    {
                        potentialMoves.Add(new Move(piece.File, piece.Rank, piece.File, 5));
                    }
                }
            }

            // Depending on the color check if the pawn can be moved one square forward
            if (piece.Color == "White")
            {
                if (piece.Rank < 8)
                {
                    if (!pieces.Any(p => p.File == piece.File && p.Rank == piece.Rank + 1))
                    {
                        potentialMoves.Add(new Move(piece.File, piece.Rank, piece.File, piece.Rank + 1));
                    }
                }
            }
            else
            {
                if (piece.Rank > 1)
                {
                    if (!pieces.Any(p => p.File == piece.File && p.Rank == piece.Rank - 1))
                    {
                        potentialMoves.Add(new Move(piece.File, piece.Rank, piece.File, piece.Rank - 1));
                    }
                }
            }

            moves = new List<Move>();

            // Check what potential moves do not create a check
            foreach (var move in potentialMoves)
            {
                // Make the move
                pieces.First(p => p.File == piece.File && p.Rank == piece.Rank).File = move.FinalPosition[0].ToString();
                pieces.First(p => p.File == move.FinalPosition[0].ToString() && p.Rank == piece.Rank).Rank = Convert.ToInt32(move.FinalPosition[1].ToString());

                // Check if the move creates a check
                if(GameExtensions.IsCheck(pieces, piece.Color == "Black" ? "White" : "Black") == false)
                {
                    moves.Add(move);
                }

                // Undo the move
                pieces.First(p => p.File == move.FinalPosition[0].ToString() && p.Rank == Convert.ToInt32(move.FinalPosition[1].ToString())).File = move.InitialPosition[0].ToString();
                pieces.First(p => p.File == move.InitialPosition[0].ToString() && p.Rank == Convert.ToInt32(move.FinalPosition[1].ToString())).Rank = Convert.ToInt32(move.InitialPosition[1].ToString());
            }

            return moves;
        }
        // Get the covered squares by the piece
        coveredSquares = piece.GetCoveredSquares(pieces);

        // Delete the squares that are occupied by pieces of the same color
        coveredSquares = coveredSquares.Where(square => !pieces.Any(p => p.File == square[0].ToString() && p.Rank == Convert.ToInt32(square[1].ToString()) && p.Color == piece.Color)).ToList();

        // Convert the covered squares into a list of potential moves
        potentialMoves = coveredSquares.Select(square => new Move(piece.File, piece.Rank, square[0].ToString(), Convert.ToInt32(square[1].ToString()))).ToList();

        moves = new List<Move>();

        // Check what potential moves do not create a check
        foreach (var move in potentialMoves)
        {
            // Make the move
            pieces.First(p => p.File == piece.File && p.Rank == piece.Rank).File = move.FinalPosition[0].ToString();
            pieces.First(p => p.File == move.FinalPosition[0].ToString() && p.Rank == piece.Rank).Rank = Convert.ToInt32(move.FinalPosition[1].ToString());

            // Check if the move creates a check
            if(GameExtensions.IsCheck(pieces, piece.Color == "Black" ? "White" : "Black") == false)
            {
                moves.Add(move);
            }

            // Undo the move
            pieces.First(p => p.File == move.FinalPosition[0].ToString() && p.Rank == Convert.ToInt32(move.FinalPosition[1].ToString())).File = move.InitialPosition[0].ToString();
            pieces.First(p => p.File == move.InitialPosition[0].ToString() && p.Rank == Convert.ToInt32(move.FinalPosition[1].ToString())).Rank = Convert.ToInt32(move.InitialPosition[1].ToString());
        }

        return moves;
    }
}