using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloSample
{
    /// <summary>
    /// Emun for use in the OthelloBoard class to keep track of which space has which piece.
    /// //X is the boarder spaces and equal to -2, W is White and equal to -1, _ is empty and equal to 0, 
    /// and B is Black and equal to 1.
    /// </summary>
    /// <author>Joseph Tamberino</author>
    /// <date>7/8/2016</date
    enum Piece {X = -2, W = -1, _, B }; 
    /// <summary>
    /// The class for the representation of the board in a game of Othello.
    /// </summary>
    class OthelloBoard
    {
        public Piece[,] theBoard
        {
            get;
        }
        public int theScore { get; private set; }

       public OthelloBoard()
        {
            theBoard = new Piece[10, 10];
            InitializeBoard();
        }
        /// <summary>
        /// Initializes the board to the beginning of the game.
        /// </summary>
        public void InitializeBoard()
        {
            

            for (int i=0; i<=9; i++)
            {
                for (int j = 0; j <= 9; j++)
                    theBoard[i, j] = Piece._; //sets all pieces to Empty
            }
            for (int i = 0; i <= 9; i++)
            {
                theBoard[0, i] = Piece.X;
                theBoard[i, 0] = Piece.X;
                theBoard[9, i] = Piece.X;
                theBoard[i, 9] = Piece.X;
                
            }
             theBoard[4, 4] = Piece.W;
             theBoard[5, 5] = Piece.W;
             theBoard[4, 5] = Piece.B;
             theBoard[5, 4] = Piece.B;
            
        }
        public OthelloBoard(OthelloBoard boardToCopy)
        {
            theBoard = (Piece[,])boardToCopy.theBoard.Clone(); //Clone works since Piece is a value type
            theScore = boardToCopy.theScore;
        }
        /// <summary>
        /// Updates the score for the representation in the board. Negative score means white is winning, positive
        /// score means black is winning.
        /// </summary>
        public void updateScore()
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (theBoard[i, j] == Piece.W || theBoard[i, j] == Piece.B)
                        theScore = theScore + (int)theBoard[i, j];
                }
            }
        }
        /// <summary>
        /// Method to create a string representation of the OthelloBoard
        /// </summary>
        /// <returns>A string representing each space on the board and what Pieces is there.</returns>
        public override string ToString()
        {
            string boardString ="  a b c d e f g h\n";
            for (int i = 1; i<9; i++)
            {
                boardString = boardString + i + " ";
                for (int j = 1; j<9; j++)
                {
                    boardString = boardString + theBoard[i, j] + " "; //builds the string using the enum values
                }
                boardString = boardString + "\n";
            }
            return boardString;
        }
        /// <summary>
        /// Updates the board. Used in applying a move.
        /// </summary>
        /// <param name="row">Int value of the row of the square to update.</param>
        /// <param name="col">Int value of the column of the square to update.</param>
        /// <param name="updateColor">Piece value that denotes which color the square is updated to.</param>
        public void UpdateBoard(int row, int col, Piece updateColor)
        {
            theBoard[row, col] = updateColor;
        }

    }
}
