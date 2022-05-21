// Jason Choi
// TINFO-200B
// Cs3 - TicTacToe - Game.cs
// 3 x 3 Tic Tac Toe
// The player can choose to play against another player or a computer.
// Before each round of the game starts, the user can select who will go first.
// If the player wants to forfeit, enter 0 on the player's turn.
// Enjoy the game!
////////////////////////////////////////////////////////////
// Referred the link https://en.wikipedia.org/wiki/Tic-tac-toe on game instruction
//
// Change History
// Date         Developer       Description
// 20220302     hyunc16         creation of project
// 20220302     hyunc16         completed [1] Player1 vs Player2 coding
// 20220303     hyunc16         completed [2] Player1 vs Computer coding
// 20220307     hyunc16         tested

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Game
    {
        // instance variables
        private int[,] gameBoard = new int[3, 3];
        private char[,] gameBoardOX = new char[3, 3];
        private int player;

        // constructor
        public Game()
        {
            InitializeGameBoard();  //initializing the gmaeBoard to 'O'
        }

        // main body of the game.cs
        internal void Start()
        {
            DisplayInstruction(); // explains the game rule and instruction

            // initialize score related variables
            int scoreForOne = 0; // player 1
            int scoreForTwo = 0; // player 2
            int scoreForTie = 0; // number of ties
            int score = 0;
            while (true)
            {
                char gameMode = DisplayGameMode(); // display and select the game mode
                InitializeGameBoard(); // resets the board
                switch (gameMode)
                {
                    case '1': // player 1 vs player 2
                        score = PersonVsPerson(); // returns the winner either 1 (player1), 2 (player2), or 0 (tie)
                        break;
                    case '2': // player 1 vs computer
                        score = PersonVsComputer(); // returns the winner either 1 (player1), 2 (player2), or 0 (tie)
                        break;
                    case '3': // exit
                              // send-off message
                        Console.WriteLine("Thank you for playing Tic Tac Toe!! ");
                        Console.Write("");
                        System.Environment.Exit(0);
                        break;
                    default:
                        break;
                }

                // score adder
                if (score == 1)
                {
                    scoreForOne++;
                }
                else if (score == 2)
                {
                    scoreForTwo++;
                }
                else
                {
                    scoreForTie++;
                }
                DisplayScore(scoreForOne, scoreForTwo, scoreForTie); // displays the score after the round is over
            }
        }

        private int PersonVsPerson()
        {
            // mode 1 - player1 vs player2
            Console.WriteLine("------------ Player 1 vs Player2 ------------");

            // decides which player is making the first move
            char firstMoveSelection;
            do
            {
                Console.Write("Who is going first (1 or 2)? ");
                firstMoveSelection = char.Parse(Console.ReadLine());
                if ((firstMoveSelection != '1') && (firstMoveSelection != '2'))
                {
                    Console.WriteLine("Please select either 1 or 2");
                }
            } while ((firstMoveSelection != '1') && (firstMoveSelection != '2'));
            DisplayGameBoardOX();
            // count to end the game when all boxes are occupied
            int count = 0;
            int decision = 0;
            // player 1 is making the first move
            if (firstMoveSelection == '1')
            {
                for (int i = 0; i < 5; i++) // maximum 5 rounds; each player makes one move; total 9 boxes; 5 rounds = 10 moves
                {
                    for (player = 1; player <= 2; player++) // player 1 plays then player 2
                    {
                        // loop in case the user selects preoccupied block
                        bool preoccupied;
                        do
                        {
                            Console.Write($"-- Player {player} -- Mark a block: ");
                            int block = int.Parse(Console.ReadLine());
                            if (block == 0) // if the user enters "0", the player forfeits the match
                            {
                                if (player == 1) // if player 1 types "0" then player 2 wins
                                {
                                    Console.WriteLine("Player 1 forfeits.");
                                    Console.WriteLine("---Player 2 Wins---");
                                    return decision = 2;
                                }
                                else
                                {
                                    Console.WriteLine("Player 2 forfeits.");
                                    Console.WriteLine("---Player 1 Wins---");
                                    return decision = 1;
                                }
                            }
                            preoccupied = MarkBlock(player, block); // marks a block with player # ( 1 or 2) and block location
                            if (preoccupied) // preoccupied: true = already taken; false = not taken
                            {
                                Console.WriteLine($"Block: {block} is already occupied. Select a different block.");
                            }
                        } while (preoccupied);
                        DisplayGameBoardOX();
                        decision = CheckForWin();
                        if ((decision == 1) || (decision == 2))
                        {
                            return decision;
                        }
                        else
                        {
                            count++;
                            if (count == 9)
                            {
                                Console.WriteLine("Game Result: Tie");
                                return decision;
                            }
                        }
                    }
                }
            }
            else
            {
                // same code as previous just player order is different.
                for (int i = 0; i < 9; i++)
                {
                    for (player = 2; player >= 1; player--) //player 2 plays then player 1
                    {
                        // loop in case the user selects preoccupied block
                        bool preoccupied;
                        do
                        {
                            Console.Write($"-- Player {player} -- Mark a block: ");
                            int block = int.Parse(Console.ReadLine());
                            if (block == 0) // if the user enters "0", the player forfeits the match
                            {
                                if (player == 1) // if player 1 types "0" then player 2 wins
                                {
                                    Console.WriteLine("Player 1 foreits.");
                                    Console.WriteLine("---Player 2 Wins---");
                                    return decision = 2;
                                }
                                else
                                {
                                    Console.WriteLine("Player 2 forfeits.");
                                    Console.WriteLine("---Player 1 Wins---");
                                    return decision = 1;
                                }
                            }
                            preoccupied = MarkBlock(player, block); // marks a block with player # ( 1 or 2)
                            if (preoccupied)
                            {
                                Console.WriteLine($"Block: {block} is already occupied. Select a different block.");
                            }
                        } while (preoccupied);

                        decision = CheckForWin();
                        if ((decision == 1) || (decision == 2))
                        {
                            return decision;
                        }
                        else
                        {
                            count++;
                            if (count == 9)
                            {
                                Console.WriteLine("Game Result: Tie");
                                return decision;
                            }
                        }
                    }
                }
            }
            return decision;
        }
        private int PersonVsComputer()
        {
            Console.WriteLine("------------ Player 1 vs Computer ------------");
            // For internal code purpose, Player2 = Computer
            // Anything visible to the user will show as Computer or 'C'

            // decides which player is making the first move
            char firstMoveSelection;
            do
            {
                Console.Write("Who is going first (Player [1] or [C]omputer)? ");
                firstMoveSelection = char.Parse(Console.ReadLine());
                if ((firstMoveSelection != '1') && (char.ToUpper(firstMoveSelection) != 'C'))
                {
                    Console.WriteLine("Please select either 1 or C");
                }
            } while ((firstMoveSelection != '1') && (char.ToUpper(firstMoveSelection) != 'C'));
            DisplayGameBoardOX();
            // count to end the game when all boxes are occupied
            int count = 0;
            int decision = 0;
            int block;
            // player 1 is making the first move
            if (firstMoveSelection == '1')
            {
                for (int i = 0; i < 5; i++) // maximum 5 rounds; each player makes one move; total 9 boxes; 5 rounds = 10 moves
                {
                    for (player = 1; player <= 2; player++) // player 1 plays then player 2 (Computer)
                    {
                        // loop in case the user selects preoccupied block
                        bool preoccupied;

                        // player 1 turn to make a move
                        if (player == 1)
                        {
                            do
                            {
                                Console.Write($"-- Player {player} -- Mark a block: ");
                                block = int.Parse(Console.ReadLine());
                                if (block == 0) // if the user enters "0", the player forfeits the match
                                {
                                    Console.WriteLine("Player 1 forfeits.");
                                    Console.WriteLine("---Player 2 Wins---");
                                }
                                preoccupied = MarkBlock(player, block); // marks a block with player # ( 1 or 2)
                                if (preoccupied)
                                {
                                    Console.WriteLine($"Block: {block} is already occupied. Select a different block.");
                                }
                            } while (preoccupied);

                        }

                        // computer turn to make a move
                        else
                        {
                            do
                            {
                                Console.Write($"-- Computer -- Thinking to make a next move.\n");
                                block = ComputerTakeTurn();
                                preoccupied = MarkBlock(player, block); // marks a block with player # ( 1 or 2)
                                Console.WriteLine($"-- Computer -- Marked the block {block}.");
                                if (preoccupied)
                                {
                                    Console.WriteLine($"Block: {block} is already occupied. Select a different block.");
                                }
                            } while (preoccupied);
                        }
                        DisplayGameBoardOX();
                        decision = CheckForWin();
                        if ((decision == 1) || (decision == 2))
                        {
                            return decision;
                        }
                        else
                        {
                            count++;
                            if (count == 9)
                            {
                                Console.WriteLine("Game Result: Tie");
                                return decision;
                            }
                        }
                    }
                }
            }
            else
            {
                // same code as previous if statement just player order is different.
                for (int i = 0; i < 5; i++)
                {
                    for (player = 2; player >= 1; player--) //player 2 plays then player 1
                    {
                        // loop in case the user selects preoccupied block
                        bool preoccupied;

                        // player 1 turn to make a move
                        if (player == 1)
                        {
                            do
                            {
                                Console.Write($"-- Player {player} -- Mark a block: ");
                                block = int.Parse(Console.ReadLine());
                                if (block == 0) // if the user enters "0", the player forfeits the match
                                {
                                    Console.WriteLine("Player1 forfeits.");
                                    Console.WriteLine("---Player 2 Wins---");
                                    return decision = 2;
                                }
                                preoccupied = MarkBlock(player, block); // marks a block with player # ( 1 or 2)
                                if (preoccupied)
                                {
                                    Console.WriteLine($"Block: {block} is already occupied. Select a different block.");
                                }
                            } while (preoccupied);

                        }

                        // computer turn to make a move
                        else
                        {
                            do
                            {
                                Console.Write($"-- Computer -- Thinking to make a next move.\n");
                                block = ComputerTakeTurn();
                                preoccupied = MarkBlock(player, block); // marks a block with player # ( 1 or 2)
                                Console.WriteLine($"-- Computer -- Marked the block {block}.");
                                if (preoccupied)
                                {
                                    Console.WriteLine($"Block: {block} is already occupied. Select a different block.");
                                }
                            } while (preoccupied);
                        }
                        decision = CheckForWin();
                        DisplayGameBoardOX();
                        if ((decision == 1) || (decision == 2))
                        {
                            return decision;
                        }
                        else
                        {
                            count++;
                            if (count == 9)
                            {
                                Console.WriteLine("Game Result: Tie");
                                return decision;
                            }
                        }
                    }
                }
            }
            return decision;
        }
        private int ComputerTakeTurn()
        {
            // strategy
            // priority 1 - attack when guaranteed win - FinishMove()
            // priority 2 - defend when next opponent move leads to lose - DefensiveMove()
            // priority 3 - if 1,2 is not possible, mark a random block - RandomMove()

            int block;
            block = FinishMove();
            // testing purpose
            // Console.WriteLine($"Finish:{block}");
            if (block == 0)
            {
                block = DefensiveMove();
                //testing purpose
                // Console.WriteLine($"Defensive:{block}");
            }
            if (block == 0)
            {
                block = RandomMove();
                // testing purpose
                // Console.WriteLine($"Random:{block}");
            }
            return block;
        }
        private int FinishMove()
        {
            // when two blocks are placed, last move decides the win. 
            // same as DefensiveMove() except if and else-if condition "== 2" instead of "== 1"
            int block = 0;

            // hard coding finish move possibilities
            // finish move - horizontal attack
            if ((gameBoard[0, 1] == 2) && (gameBoard[0, 2] == 2))
            {
                block = 1;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 0] == 2) && (gameBoard[0, 2] == 2))
            {
                block = 2;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 0] == 2) && (gameBoard[0, 1] == 2))
            {
                block = 3;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 1] == 2) && (gameBoard[1, 2] == 2))
            {
                block = 4;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 0] == 2) && (gameBoard[1, 2] == 2))
            {
                block = 5;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 0] == 2) && (gameBoard[1, 1] == 2))
            {
                block = 6;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[2, 1] == 2) && (gameBoard[2, 2] == 2))
            {
                block = 7;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[2, 0] == 2) && (gameBoard[2, 2] == 2))
            {
                block = 8;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[2, 0] == 2) && (gameBoard[2, 1] == 2))
            {
                block = 9;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }

            // finish move - vertical attack
            if ((gameBoard[1, 0] == 2) && (gameBoard[2, 0] == 2))
            {
                block = 1;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 1] == 2) && (gameBoard[2, 1] == 2))
            {
                block = 2;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 2] == 2) && (gameBoard[2, 2] == 2))
            {
                block = 3;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 0] == 2) && (gameBoard[2, 0] == 2))
            {
                block = 4;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 1] == 2) && (gameBoard[2, 1] == 2))
            {
                block = 5;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 2] == 2) && (gameBoard[2, 2] == 2))
            {
                block = 6;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 1] == 2) && (gameBoard[2, 0] == 2))
            {
                block = 7;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 1] == 2) && (gameBoard[1, 1] == 2))
            {
                block = 8;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 2] == 2) && (gameBoard[1, 2] == 2))
            {
                block = 9;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }

            // finish move - diagonal attack
            if ((gameBoard[1, 1] == 2) && (gameBoard[2, 2] == 2))
            {
                block = 1;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 1] == 2) && (gameBoard[2, 0] == 2))
            {
                block = 3;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 0] == 2) && (gameBoard[2, 2] == 2))
            {
                block = 5;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 2] == 2) && (gameBoard[2, 0] == 2))
            {
                block = 5;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 1] == 2) && (gameBoard[0, 2] == 2))
            {
                block = 7;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 0] == 2) && (gameBoard[1, 1] == 2))
            {
                block = 9;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            // if none of the possibilities above returns, return 0 
            return block = 0;
        }
        private int DefensiveMove()
        {
            int block = 0;
            // hard coding defensive move possibilities
            // defense move - horizontal attack
            if ((gameBoard[0, 1] == 1) && (gameBoard[0, 2] == 1))
            {
                block = 1;
                // check to see if the block is occupied. If not, assign that number as a block.
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 0] == 1) && (gameBoard[0, 2] == 1))
            {
                block = 2;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 0] == 1) && (gameBoard[0, 1] == 1))
            {
                block = 3;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 1] == 1) && (gameBoard[1, 2] == 1))
            {
                block = 4;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 0] == 1) && (gameBoard[1, 2] == 1))
            {
                block = 5;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 0] == 1) && (gameBoard[1, 1] == 1))
            {
                block = 6;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[2, 1] == 1) && (gameBoard[2, 2] == 1))
            {
                block = 7;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[2, 0] == 1) && (gameBoard[2, 2] == 1))
            {
                block = 8;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[2, 0] == 1) && (gameBoard[2, 1] == 1))
            {
                block = 9;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            // defense move - vertical attack
            if ((gameBoard[1, 0] == 1) && (gameBoard[2, 0] == 1))
            {
                block = 1;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 1] == 1) && (gameBoard[2, 1] == 1))
            {
                block = 2;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 2] == 1) && (gameBoard[2, 2] == 1))
            {
                block = 3;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 0] == 1) && (gameBoard[2, 0] == 1))
            {
                block = 4;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 1] == 1) && (gameBoard[2, 1] == 1))
            {
                block = 5;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 2] == 1) && (gameBoard[2, 2] == 1))
            {
                block = 6;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 1] == 1) && (gameBoard[2, 0] == 1))
            {
                block = 7;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 1] == 1) && (gameBoard[1, 1] == 1))
            {
                block = 8;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 2] == 1) && (gameBoard[1, 2] == 1))
            {
                block = 9;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            // defense move - diagonal attack
            if ((gameBoard[1, 1] == 1) && (gameBoard[2, 2] == 1))
            {
                block = 1;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 1] == 1) && (gameBoard[2, 0] == 1))
            {
                block = 3;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 0] == 1) && (gameBoard[2, 2] == 1))
            {
                block = 5;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 2] == 1) && (gameBoard[2, 0] == 1))
            {
                block = 5;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[1, 1] == 1) && (gameBoard[0, 2] == 1))
            {
                block = 7;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            if ((gameBoard[0, 0] == 1) && (gameBoard[1, 1] == 1))
            {
                block = 9;
                if ((!CheckBlock(block)))
                {
                    return block;
                }
            }
            // if the method did not return up until this point, that means no possible defensive move. Return 0.
            return block = 0;
        }
        private int RandomMove()
        {
            // declaration/initialization statements
            Random rand = new Random();
            int block = 0;
            bool condition = true;
            while (condition)
            {
                block = rand.Next(1, 10);
                condition = CheckBlock(block);
            }
            return block;
        }
        private int CheckForWin()
        {
            int winner;
            // Player 1 wins
            if ((gameBoard[0, 0] == 1) && (gameBoard[1, 0] == 1) && (gameBoard[2, 0] == 1)) //horizontal win 1 2 3 
            {
                Console.WriteLine("Winner: Player 1");
                winner = 1;
            }
            else if ((gameBoard[0, 1] == 1) && (gameBoard[1, 1] == 1) && (gameBoard[2, 1] == 1)) // horizontal win 4 5 6 
            {
                Console.WriteLine("Winner: Player 1");
                winner = 1;
            }
            else if ((gameBoard[0, 2] == 1) && (gameBoard[1, 2] == 1) && (gameBoard[2, 2] == 1)) // horizontal win 7 8 9
            {
                Console.WriteLine("Winner: Player 1");
                winner = 1;
            }
            else if ((gameBoard[0, 0] == 1) && (gameBoard[0, 1] == 1) && (gameBoard[0, 2] == 1)) // vertical win 1 4 7
            {
                Console.WriteLine("Winner: Player 1");
                winner = 1;
            }
            else if ((gameBoard[1, 0] == 1) && (gameBoard[1, 1] == 1) && (gameBoard[1, 2] == 1)) // vertical win 2 5 8
            {
                Console.WriteLine("Winner: Player 1");
                winner = 1;
            }
            else if ((gameBoard[2, 0] == 1) && (gameBoard[2, 1] == 1) && (gameBoard[2, 2] == 1)) // vertical win 3 6 9
            {
                Console.WriteLine("Winner: Player 1");
                winner = 1;
            }
            else if ((gameBoard[0, 0] == 1) && (gameBoard[1, 1] == 1) && (gameBoard[2, 2] == 1)) //diagonal win 1 5 9
            {
                Console.WriteLine("Winner: Player 1");
                winner = 1;
            }
            else if ((gameBoard[2, 0] == 1) && (gameBoard[1, 1] == 1) && (gameBoard[0, 2] == 1)) // diagonal win 3 5 7
            {
                Console.WriteLine("Winner: Player 1");
                winner = 1;
            }
            // Player 2 wins
            else if ((gameBoard[0, 0] == 2) && (gameBoard[1, 0] == 2) && (gameBoard[2, 0] == 2)) //horizontal win 1 2 3 
            {
                Console.WriteLine("Winner: Player 2");
                winner = 2;
            }
            else if ((gameBoard[0, 1] == 2) && (gameBoard[1, 1] == 2) && (gameBoard[2, 1] == 2)) // horizontal win 4 5 6 
            {
                Console.WriteLine("Winner: Player 2");
                winner = 2;
            }
            else if ((gameBoard[0, 2] == 2) && (gameBoard[1, 2] == 2) && (gameBoard[2, 2] == 2)) // horizontal win 7 8 9
            {
                Console.WriteLine("Winner: Player 2");
                winner = 2;
            }
            else if ((gameBoard[0, 0] == 2) && (gameBoard[0, 1] == 2) && (gameBoard[0, 2] == 2)) // vertical win 1 4 7
            {
                Console.WriteLine("Winner: Player 2");
                winner = 2;
            }
            else if ((gameBoard[1, 0] == 1) && (gameBoard[1, 1] == 1) && (gameBoard[1, 2] == 1)) // vertical win 2 5 8
            {
                Console.WriteLine("Winner: Player 2");
                winner = 2;
            }
            else if ((gameBoard[2, 0] == 2) && (gameBoard[2, 1] == 2) && (gameBoard[2, 2] == 2)) // vertical win 3 6 9
            {
                Console.WriteLine("Winner: Player 2");
                winner = 2;
            }
            else if ((gameBoard[0, 0] == 2) && (gameBoard[1, 1] == 2) && (gameBoard[2, 2] == 2)) //diagonal win 1 5 9
            {
                Console.WriteLine("Winner: Player 2");
                winner = 2;
            }
            else if ((gameBoard[2, 0] == 2) && (gameBoard[1, 1] == 2) && (gameBoard[0, 2] == 2)) // diagonal win 3 5 7
            {
                Console.WriteLine("Winner: Player 2");
                winner = 2;
            }
            else
            {
                winner = 0; // tie
            }
            return winner;
        }
        private bool CheckBlock(int block)
        {
            // Check a block to see if the block is occupied by player 1 or player 2/computer

            // true = occupied
            // false = empty
            bool preoccupied = true;
            switch (block)
            {
                case 1:
                    if ((gameBoard[0, 0] != 1) && (gameBoard[0, 0] != 2))   // check to see if block 1 has been previosly occupied
                    {
                        preoccupied = false;
                    }
                    break;
                case 2:
                    if ((gameBoard[0, 1] != 1) && (gameBoard[0, 1] != 2))
                    {
                        preoccupied = false;

                    }
                    break;
                case 3:
                    if ((gameBoard[0, 2] != 1) && (gameBoard[0, 2] != 2))
                    {
                        preoccupied = false;
                    }
                    break;
                case 4:
                    if ((gameBoard[1, 0] != 1) && (gameBoard[1, 0] != 2))
                    {
                        preoccupied = false;
                    }
                    break;
                case 5:
                    if ((gameBoard[1, 1] != 1) && (gameBoard[1, 1] != 2))
                    {
                        preoccupied = false;
                    }
                    break;
                case 6:
                    if ((gameBoard[1, 2] != 1) && (gameBoard[1, 2] != 2))
                    {
                        preoccupied = false;
                    }
                    break;
                case 7:
                    if ((gameBoard[2, 0] != 1) && (gameBoard[2, 0] != 2))
                    {
                        preoccupied = false;
                    }
                    break;
                case 8:
                    if ((gameBoard[2, 1] != 1) && (gameBoard[2, 1] != 2))
                    {
                        preoccupied = false;
                    }
                    break;
                case 9:
                    if ((gameBoard[2, 2] != 1) && (gameBoard[2, 2] != 2))
                    {
                        preoccupied = false;
                    }
                    break;
                default:
                    break;
            }
            return preoccupied;
        }
        private bool MarkBlock(int player, int block)
        {
            // block size is the 3 x 3 (1,2,3,4,5,6,7,8,9) refer back to the DisplayInstruction()

            // true = failed to mark
            // false = newly marked
            bool preoccupied = true;
            switch (block)
            {
                case 1:
                    if ((gameBoard[0, 0] != 1) && (gameBoard[0, 0] != 2))   // check to see if block 1 has been previosly occupied
                    {
                        gameBoard[0, 0] = player;  // mark the block with player number
                        preoccupied = false;
                    }
                    break;
                case 2:
                    if ((gameBoard[0, 1] != 1) && (gameBoard[0, 1] != 2))
                    {
                        gameBoard[0, 1] = player;
                        preoccupied = false;

                    }
                    break;
                case 3:
                    if ((gameBoard[0, 2] != 1) && (gameBoard[0, 2] != 2))
                    {
                        gameBoard[0, 2] = player;
                        preoccupied = false;
                    }
                    break;
                case 4:
                    if ((gameBoard[1, 0] != 1) && (gameBoard[1, 0] != 2))
                    {
                        gameBoard[1, 0] = player;
                        preoccupied = false;
                    }
                    break;
                case 5:
                    if ((gameBoard[1, 1] != 1) && (gameBoard[1, 1] != 2))
                    {
                        gameBoard[1, 1] = player;
                        preoccupied = false;
                    }
                    break;
                case 6:
                    if ((gameBoard[1, 2] != 1) && (gameBoard[1, 2] != 2))
                    {
                        gameBoard[1, 2] = player;
                        preoccupied = false;
                    }
                    break;
                case 7:
                    if ((gameBoard[2, 0] != 1) && (gameBoard[2, 0] != 2))
                    {
                        gameBoard[2, 0] = player;
                        preoccupied = false;
                    }
                    break;
                case 8:
                    if ((gameBoard[2, 1] != 1) && (gameBoard[2, 1] != 2))
                    {
                        gameBoard[2, 1] = player;
                        preoccupied = false;
                    }
                    break;
                case 9:
                    if ((gameBoard[2, 2] != 1) && (gameBoard[2, 2] != 2))
                    {
                        gameBoard[2, 2] = player;
                        preoccupied = false;
                    }
                    break;
                default:
                    break;
            }
            return preoccupied;
        }
        private void DisplayScore(int one, int two, int tie)
        {
            Console.WriteLine($@"
-----------Score-----------
Player 1            : {one}
Player 2/Computer   : {two}
Tie                 : {tie}");
        }
        private void DisplayGameBoardOX()
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (gameBoard[i, j] == 1)
                    {
                        gameBoardOX[i, j] = 'O';
                    }
                    else if (gameBoard[i, j] == 2)
                    {
                        gameBoardOX[i, j] = 'X';
                    }
                    else
                    {
                        // no change if not occupied by player 1 or 2
                    }


                }
            }

            // display the gameboard on the console
            Console.WriteLine("-----GameBoard-----");
            Console.WriteLine($"     {gameBoardOX[0, 0]} | {gameBoardOX[0, 1]} | {gameBoardOX[0, 2]}");
            Console.WriteLine("     ---------");
            Console.WriteLine($"     {gameBoardOX[1, 0]} | {gameBoardOX[1, 1]} | {gameBoardOX[1, 2]}");
            Console.WriteLine("     ---------");
            Console.WriteLine($"     {gameBoardOX[2, 0]} | {gameBoardOX[2, 1]} | {gameBoardOX[2, 2]}");
        }
        private char DisplayGameMode()
        {
            // Allow user to select two different modes
            char selection;
            do
            {
                Console.WriteLine("[1] Player 1 vs Player 2\n" +
                                    "[2] Player 1 vs Computer\n" +
                                    "[3] Exit the game");
                Console.Write("****Game Mode:");
                selection = char.Parse(Console.ReadLine());
                if ((selection != '1') && (selection != '2') && (selection != '3'))
                {
                    Console.WriteLine("You entered invalid");
                }
            } while ((selection != '1') && (selection != '2') && (selection != '3'));
            return selection;
        }
        private void DisplayInstruction()
        {
            Console.WriteLine(@"
OXOXOXOXOXOXOXOXOXOXOXOXO TIC - TAC - TOE XOXOXOXOXOXOXOXOXOXOXOXOXOXOXO

Game Instruction:
On a 3 X 3 Block, players/computer will take turn to mark a block. 
Make a complete row of same mark horizontally, vertically, or 
diagonally to win the game. 

Refer to https://en.wikipedia.org/wiki/Tic-tac-toe for more game info.

Before each round begins, the player can select who will take the first
turn. Enter value that is in the bracket.
ex. Player [1], Player [2], [C]omputer depending on the situation.

The Game Board Layout is illustrated below. To mark the block, 
type corresponding block # on your turn.  If you want to
forfeit, type '0' anytime on your turn.

The scoreboard will be posted every time a round is up.

Game Board Layout
-----------------                
   1  | 2 |  3                   Legend        
   ---|---|----         ------------------------
   4  | 5 |  6          O - Player 1          
   ---|---|----         X - Player 2 / Computer
   7  | 8 |  9

    0 = forfeit

Select a block by entering a digit based on the game board
layout.

Good Luck to you!
");
        }
        private void InitializeGameBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameBoard[i, j] = 0;
                }
            }
            // hard code to display the gameBoard to the user
            // each block box will show block #
            gameBoardOX[0, 0] = '1';
            gameBoardOX[0, 1] = '2';
            gameBoardOX[0, 2] = '3';
            gameBoardOX[1, 0] = '4';
            gameBoardOX[1, 1] = '5';
            gameBoardOX[1, 2] = '6';
            gameBoardOX[2, 0] = '7';
            gameBoardOX[2, 1] = '8';
            gameBoardOX[2, 2] = '9';







        }
    }
}
