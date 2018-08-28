using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Checkers.Models;

namespace Checkers.Services
{
    public static class AvailableMoveService
    {
        /// <summary>
        /// Returns all available moves for a player, including jumps
        /// </summary>
        /// <param name="board">Game board</param>
        /// <param name="player">Player whos turn it is</param>
        /// <returns></returns>
        public static List<Move> GetMoves(Board board, Player player)
        {
            var moves = new List<Move>();

            //----For Each Piece----
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = board.Get(x, y);
                    if (piece != null && piece.Player == player)
                    {
                        //Check jumps and move spaces
                        for (int xi = -2; xi <= 2; xi++)
                        {
                            for (int yi = -1; yi <= 1; yi += 2)
                            {
                                moves.Add(new Move()
                                {
                                    Piece = piece,
                                    MoveTo = Tuple.Create(x + xi, y + (xi * yi))
                                });
                            }
                        }
                    }
                }
            }

            return moves.FindAll(x => isValidMove(board, x));
        }

        /// <summary>
        /// Returns true if the given move is valid. Supports regular moves and jump operations.
        /// </summary>
        /// <param name="board">Game Board</param>
        /// <param name="move">Move to be applied</param>
        /// <returns></returns>
        public static bool isValidMove(Board board, Move move)
        {
            var curLoc = move.Piece.Location;
            //Check all are in bounds
            if (curLoc.Item1 < 0 || curLoc.Item1 > 7 ||
                curLoc.Item2 < 0 || curLoc.Item2 > 7 ||
                move.MoveTo.Item1 < 0 || move.MoveTo.Item1 > 7 ||
                move.MoveTo.Item2 < 0 || move.MoveTo.Item2 > 7
                )
            {
                return false;
            }

            //If the piece on the board is empty, false
            if (board.Get(curLoc.Item1, curLoc.Item2) == null)
            {
                return false;
            }

            //If the move doesn't match the board, false
            if (!board.Get(curLoc.Item1, curLoc.Item2).Equals(move.Piece))
            {
                return false;
            }

            //If the moveTo is not empty, false
            if (board.Get(move.MoveTo.Item1, move.MoveTo.Item2) != null)
            {
                return false;
            }

            //Build a list of possible moves
            //We already checked that the move was on the board and a valid square, so we don't 
            //   have to check if each of the following are on the board.
            List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();

            if (move.Piece.IsKing)
            {
                for (int x = -1; x <= 1; x += 2)
                {
                    for (int y = -1; y <= 1; y += 2)
                    {
                        //Regular Moves
                        possibleMoves.Add(Tuple.Create(curLoc.Item1 + x, curLoc.Item2 + y));

                        //Jumps
                        if (curLoc.Item1 + x > 0 && curLoc.Item1 + x < 7 && //If jump piece is in bounds
                            curLoc.Item2 + y > 0 && curLoc.Item2 + y < 7)
                        {
                            var jumpPiece = board.Get(curLoc.Item1 + x, curLoc.Item2 + y);
                            if (jumpPiece != null && jumpPiece.Player != move.Piece.Player) //and we already know moveTo is empty
                            {
                                possibleMoves.Add(Tuple.Create(curLoc.Item1 + (2 * x), curLoc.Item2 + (2 * y)));
                            }
                        }
                    }
                }
            }
            else
            {
                int ydir = move.Piece.Player == Player.BLACK ? 1 : -1;
                for (int x = -1; x <= 1; x += 2)
                {
                    //Regular Moves
                    possibleMoves.Add(Tuple.Create(curLoc.Item1 + x, curLoc.Item2 + ydir));
                    //Jumps
                    if (curLoc.Item1 + x > 0 && curLoc.Item1 + x < 7 && //If jump piece is in bounds
                        curLoc.Item2 + ydir > 0 && curLoc.Item2 + ydir < 7)
                    {
                        var jumpPiece = board.Get(curLoc.Item1 + x, curLoc.Item2 + ydir);
                        if (jumpPiece != null && jumpPiece.Player != move.Piece.Player) //and we already know moveTo is empty
                        {
                            possibleMoves.Add(Tuple.Create(curLoc.Item1 + (2 * x), curLoc.Item2 + (2 * ydir)));
                        }
                    }
                }
            }

            return possibleMoves.Contains(move.MoveTo);
        }

        public static List<Move> GetAllJumpMoves(Board board, Player player)
        {
            return board.Pieces
                .Where(x => x?.Player == player)
                .SelectMany(x => GetJumpMoves(board, x))
                .ToList();
        }


        /// <summary>
        /// Returns the mandatory jump moves for a piece
        /// </summary>
        /// <param name="board">GameBoard</param>
        /// <param name="piece">Piece to check for jumps on</param>
        /// <returns></returns>
        public static List<Move> GetJumpMoves(Board board, Piece piece)
        {
            var moves = new List<Move>();

            for (int x = -2; x <= 2; x += 4)
            {
                for (int y = -2; y <= 2; y += 4)
                {
                    moves.Add(new Move()
                    {
                        MoveTo = Tuple.Create(piece.Location.Item1 + x, piece.Location.Item2 + y),
                        Piece = piece
                    });
                }
            }

            return moves.FindAll(x => isValidMove(board, x));
        }

        public static bool IsGameOver(Board board)
        {
            return board.Pieces.FindAll(x => x?.Player == Player.RED).Count == 0 ||
                board.Pieces.FindAll(x => x?.Player == Player.BLACK).Count == 0;
        }

        /// <summary>
        /// Returns the winner if there is one. If there is no winner it returns black.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static Player GetWinner(Board board)
        {
            return board.Pieces.FindAll(x => x?.Player == Player.RED).Count == 0 ? Player.BLACK : Player.RED;
        }

    }
}
