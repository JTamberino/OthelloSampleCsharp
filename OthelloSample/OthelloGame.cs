using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloSample
{

/// <summary>
/// This class is the main class to handle the logic of the Othello Game, keeping track of player moves, score,
/// user input and other related data.
/// </summary
/// <author>Joseph Tamberino</author>
/// <date>7/6/2016</date>
    class OthelloGame
    {

        Piece currentPlayer; //Piece value that's used to determine which player is playing the game.
        Piece computerPlayer; //Piece value that tells the program which one is the computer
        int theScore; // value for the score of the game, negative score means white is in the lead, positive score means black is in the lead
        List<OthelloMove> validMoves;
        OthelloBoard gameBoard;
        bool gameOver;

        OthelloGame()
        {
            gameBoard = new OthelloBoard();
            validMoves = new List<OthelloMove>();
            theScore = 0;
            currentPlayer = Piece.B; //in Othello Black always goes first
            gameOver = false;
        }


        /// <summary>
        /// This method initializes the game after the game is won.
        /// </summary>
        void InitializeGame()
        {
            gameBoard.InitializeBoard();
            theScore = 0;
            gameOver = false;
            currentPlayer = Piece.B; //in Othello Black always goes first
        
    }
        /// <summary>
        /// Prints the board to the console to allow the Player the user to see the current state of the game
        /// board.
        /// </summary>
        void PrintBoard()
        {
            Console.WriteLine(gameBoard);
        }

        /// <summary>
        /// Updates the score of the game to determine the winner.
        /// </summary>
        void updateScore()
        {
            gameBoard.updateScore();
            theScore = gameBoard.theScore;
        }

        /// <summary>
        /// Helper method to add a move to the validMoves list as long as the move is a legal move in Othello.
        /// </summary>
        /// <param name="row">int value to denote the row of the move, only 1-8 is a valid number here.</param>
        /// <param name="col">int value to denote the column of the move, only 1-8 is a valid number here.</param>
        /// <param name="checkingCurrent">Piece value so the method knows which player to check the moves for.</param>
        /// <param name="theMoves">MoveList to add moves too.</param>
        private void AddMoveIfValid(int row, int col, Piece checkingCurrent, List<OthelloMove> theMoves) {
            bool theMoveIsVaild = false;
           
            int distance; // this variable contains the distance that the board keeps traversing through
            // until it determines if the move is valid or invalid
            for (int i = -1; i<=1; i++)
            { //i is equal to the row direction to check
                if (theMoveIsVaild) break; // breaks out of loop if move already valid
                for (int j=-1; j<=1; j++)
                {// j is equal to colum direction to check
                    if (i == 0 && j == 0) continue; //goes to next loop iteration 
                                                    //if both values are 0, since you don't need to check
                                                    // the space you're looking at
                    if (theMoveIsVaild) break; //breaks out of loop if move already valid
                    distance = 1; //initalizes distance to 1
                    while ((int)gameBoard.theBoard[row+(i*distance),col+(j*distance)] == (int)checkingCurrent*-1)
                    { // as long as the space being looked at is equal to the opposite player's color
                        distance++;
                        
                    }
                    if (gameBoard.theBoard[row + (i * distance), col + (j * distance)] == checkingCurrent
                        && distance > 1)
                    {
                        //if after the loop the space contains a piece containing the current players color,
                        //and there's at least one piece of the opposite color
                        theMoveIsVaild = true; //this is a valid move
                        
                    }

                }
            }
            if (theMoveIsVaild)
            {
                //if the move is valid
                
                OthelloMove newMove = new OthelloMove(row,col); // creates the new move
                theMoves.Add(newMove); //adds the move to the current moveList
            }


        }
        /// <summary>
        /// Checks the board for all valid moes for the current player.
        /// </summary>
        /// <param name="aBoard">OthelloBoard to check</param>
        /// <param name="theMoves">List to add the moves to.</param>
        void CheckMoves(OthelloBoard aBoard, List<OthelloMove> theMoves, Piece player)
        {
            for (int i=1; i<9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (aBoard.theBoard[i, j] == Piece._)
                        AddMoveIfValid(i, j, player,theMoves); //check if the move is valid if the space is empty
                }
            }
        }
        /// <summary>
        /// Applies the move that the current player selects to the board.
        /// </summary>
        /// <param name="aBoard">The OthelloBoard to have the move applied to.</param>
        /// <param name="theMove">Move value to be applied to the board.</param>\
        /// <param name="checkingCurrent">Piece value that denotes the current player</paran>
        void ApplyMove(OthelloBoard aBoard, OthelloMove theMove, Piece checkingCurrent)
        {
            OthelloMove internalMove = theMove;
            int distance; //distance variable for changing the value of the piece on the board

            
            for (int i = -1; i <= 1; i++)
            { //i is equal to the row direction to check
                
                for (int j = -1; j <= 1; j++)
                {// j is equal to colum direction to check
                    if (i == 0 && j == 0) continue;
                    distance = 1; //initalizes distance to 1
                    while ((int)gameBoard.theBoard[theMove.row + (i * distance), theMove.col + (j * distance)] == (int)checkingCurrent * -1)
                    { // as long as the space being looked at is equal to the opposite player's color
                        distance++;             

                    }
                    if ((int)gameBoard.theBoard[theMove.row + (i * distance), theMove.col + (j * distance)] == (int)checkingCurrent)
                    {
                        //if at the end the space is the same color as the current player
                        while (distance >= 1)
                        {
                            aBoard.UpdateBoard(theMove.row + (i * distance), theMove.col + (j * distance), checkingCurrent);
                            distance--;
                        }
                    }
                    
                }

              }
            
            aBoard.UpdateBoard(theMove.row,theMove.col, checkingCurrent); //applies update to actual move space

        }
        /// <summary>
        /// Finds the move in the movelist.
        /// </summary>
        /// <param name="moveID">An OrderedPair value that corresponds to the id of the move in the validMoves list.</param>
        /// <returns>Returns the move corresponding to the OrderedPair provided, or a illegal move if
        /// there isn't a move with that id.</returns>
        public OthelloMove FindMove(int row, int col)
        {
            foreach (OthelloMove lookMove in validMoves)
            {
                if (lookMove.Equals(new OthelloMove(row,col)))
                {
                    return lookMove;
                }
            }
            return new OthelloMove(0,0); //returns a move that is
            //never valid and in the move list, making it easier to check to see if the player entered an invalid move
        }
        /// <summary>
        /// Switches the players turn at the end of the move.
        /// </summary>
        public void SwitchPlayer()
        {
            currentPlayer = (Piece)((int)currentPlayer * -1); //changes current player from white to black or vice versa
            validMoves.Clear(); //removes all moves from the list of moves
        }
        /// <summary>
        /// Calculates the index of the move in the validMoves list that the computer player decides to use. 
        /// This particular variation of the program uses the simple Othello strategy of having the most
        /// pieces on the board, and looks 6 moves ahead.
        /// </summary>
        /// <param name="Player">Piece value representing the current players turn.</param>
        /// <param name="currentBoard">OthelloBoard of the current board that is being examined.</param>
        /// <param name="noTurnCount">Integer value to count the number of turns it's been for the program looking
        /// ahead where there wasn't a valid move to use.</param>
        /// <param name="turnCount">Integer counter for the number of turns the program has been searching through
        /// future boards for.</param>
        /// <returns></returns>
        int CompMove(Piece Player, OthelloBoard currentBoard, int noTurnCount=0, int turnCount=0)
        {
            List<OthelloBoard> boardList = new List<OthelloBoard>();
            List<OthelloMove> moveList = new List<OthelloMove>();
            CheckMoves(currentBoard, moveList, Player);

            int counterOfNoTurns = noTurnCount;
            int counterOfTurns = turnCount;
            if (moveList.Count == 0)
                counterOfNoTurns++; //increments the counter that says there's no move if there isn't a valid move to use
            if (counterOfNoTurns == 2)
            { //if both players have no move, then this is the final board of the 'future' game.
                currentBoard.updateScore();
                return currentBoard.theScore;
            }
            
            if (counterOfTurns > 5)
            {//if the program has looked ahead five turns for this move, return. (More for the interest of time)
                currentBoard.updateScore();
                return currentBoard.theScore;
            }
            
            boardList.Add(new OthelloBoard(currentBoard)); //ensures there's always at least one board in the boardlist.

            for (int i=1; i< moveList.Count; i++)
            {
                boardList.Add(new OthelloBoard(currentBoard)); //makes a copy of the current board for each
                //move in the move list

            }
            int[] boardScores = new int[boardList.Count];
            
            int j = 0;
            foreach (OthelloBoard board in boardList)
            {
                if (moveList.Count > 0)
                    ApplyMove(board, moveList.ElementAt(j), Player);
                board.updateScore();
                boardScores[j] = CompMove((Piece)((int)Player*-1),board,counterOfNoTurns, counterOfTurns+1);
                j++;

            }
            //after all the recursive score gathering is done
            int index = 0;
            for (int i=1; i < boardScores.Length; i++)
            {
                if (Player == Piece.W) //if computer player is white, look for the lowest score
                {
                    if (boardScores[i] < boardScores[index])
                        index = i;
                }
                else if (Player == Piece.B) //if computer player is black, look for the highest score
                {
                    if (boardScores[i] > boardScores[index])
                        index = i;
                }
            }

            return index;
           
        }

        static void Main(string[] args)
        {
            OthelloGame theGame = new OthelloGame();
            Random rng = new Random(); //rng for computer to choose moves before AI is implemented
        Beginning: //label to get back here if needed at the end of the game
            
            Console.WriteLine("It's Time to play Othello!");
            Console.WriteLine("Are you going to play as the Black Pieces or the White Pieces. Type B or W," +
               " then press ENTER to choose.");
            String choosePlayer = Console.ReadLine().ToUpper();
            while (choosePlayer != "B" && choosePlayer != "W")
            {
                Console.WriteLine("Invalid answer. Type B or W, then press ENTER to choose which side you're playing as.");
                choosePlayer = Console.ReadLine().ToUpper();
            }
            if (choosePlayer == "B")
                theGame.computerPlayer = Piece.W;
            else
                theGame.computerPlayer = Piece.B;
            int noTurnCount = 0; //checks to see how many times there's been no turn
            while (!theGame.gameOver){ //while the game isn't over, play the game
                
                string currentTurnName = "";
                if (theGame.currentPlayer == Piece.B)
                    currentTurnName = "Black";
                else
                    currentTurnName = "White";
                theGame.CheckMoves(theGame.gameBoard, theGame.validMoves, theGame.currentPlayer); // check the moves for the current player
                OthelloMove theMove; //the chosen move to check
                if (theGame.validMoves.Count > 0)
                {
                    noTurnCount = 0; //resets the count of no one making a move
                    Console.WriteLine("It is " + currentTurnName + "'s turn.");
                    theMove = new OthelloMove(0,0); //sets id to a invalid move
                    theGame.PrintBoard();
                    if (theGame.currentPlayer != theGame.computerPlayer)
                    {
                        
                        foreach (OthelloMove themove in theGame.validMoves)
                        {
                            Console.Write(themove + " ");
                        }
                        Console.WriteLine();
                        while (theMove.Equals(new OthelloMove(0,0))) { 
                            Console.WriteLine("Choose your move: Enter move in format of row number, column number");
                            string moveString = Console.ReadLine();
                            string[] stringForMove = moveString.Split(',');
                            if (stringForMove.Length != 2)
                            {//if the string for the move doesn't contain just two entries
                                Console.WriteLine("Invalid Move Entry!");
                                continue;
                            }
                            try {
                                theMove = theGame.FindMove(Int32.Parse(stringForMove[0]), Int32.Parse(stringForMove[1]));
                            }
                            catch
                            {//if the values for the move don't correspond to integer values
                                Console.WriteLine("Invalid Move Entry!");
                                continue;
                            }
                            if (theMove.Equals(new OthelloMove(0, 0))) //if the move chosen wasn't in the move list
                                Console.WriteLine("Invalid Move Entry!");
                        }
                    }
                    else
                    {
                        foreach (OthelloMove themove in theGame.validMoves)
                        {
                            Console.Write(themove + " ");
                        }
                        Console.WriteLine();
                        int moveIndex = theGame.CompMove(theGame.currentPlayer, theGame.gameBoard);
                        theMove = theGame.validMoves.ElementAt(moveIndex);
                        
                        Console.WriteLine(theMove);
                      
                       
                    }
                    theGame.ApplyMove(theGame.gameBoard,theMove, theGame.currentPlayer);
                 }               
                else
                    noTurnCount++;
                if (noTurnCount < 2) { //if one player can still make a move
                    theGame.SwitchPlayer(); //switches the current Player
                }
                else
                    theGame.gameOver = true;
            }
            theGame.updateScore();
            theGame.PrintBoard();
            if (theGame.theScore > 0)
                Console.WriteLine("Black Wins!");
            else if (theGame.theScore < 0)
                Console.WriteLine("White Wins!");
            else
                Console.WriteLine("It's a tie!");
            Console.WriteLine("Would you like to play again? Enter Y or N.");
            string restartString = Console.ReadLine().ToUpper();
            while (restartString != "Y" && restartString != "N")
            {
                Console.WriteLine("Invalid answer. Type Y or N, then press ENTER to choose to play again or not.");
                restartString = Console.ReadLine().ToUpper();
            }
            if (restartString == "Y") {
                theGame.InitializeGame();
                goto Beginning;
            }

        }
     }
}
