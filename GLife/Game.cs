using System;

namespace GLife
{
    internal class Game
    {
        //2D arrays for the game board
        private char[,] gameBoard;
        private char[,] buffBoard;

        // char constants for the alive, dead, and empty WS chars
        public const char LIVE = '@';
        public const char DEAD = '-';
        public const char SPACE = ' ';

        // size of the board
        public readonly int ROW_SIZE = 30;
        public readonly int COL_SIZE = 50;


        public Game() //ctor tab tab to create a constructor
        {
            //initialize major objects here
            gameBoard = new char[ROW_SIZE, COL_SIZE];
            buffBoard = new char[ROW_SIZE, COL_SIZE];

            // initialize the game boards
            InitializeGameBoards();

            // insert the startup pattern or arrangement of lives
            InsertStartupPatterns(5, 5);
            InsertStartupPatterns(15, 5);
            InsertStartupPatterns(25, 5);
        }

        private void InsertStartupPatterns(int r, int c)
        {
            // after 1 Dead cell to start
            // insert 8 LIVE cells
            gameBoard[r, c + 1] = LIVE;
            gameBoard[r, c + 2] = LIVE;
            gameBoard[r, c + 3] = LIVE;
            gameBoard[r, c + 4] = LIVE;
            gameBoard[r, c + 5] = LIVE;
            gameBoard[r, c + 6] = LIVE;
            gameBoard[r, c + 7] = LIVE;
            gameBoard[r, c + 8] = LIVE;

            // leave 1 DEAD
            // insert 5 LIVE
            gameBoard[r, c + 10] = LIVE;
            gameBoard[r, c + 11] = LIVE;
            gameBoard[r, c + 12] = LIVE;
            gameBoard[r, c + 13] = LIVE;
            gameBoard[r, c + 14] = LIVE;

            // leave 3 DEAD
            // insert 3 LIVE
            gameBoard[r, c + 18] = LIVE;
            gameBoard[r, c + 19] = LIVE;
            gameBoard[r, c + 20] = LIVE;


            // leave 7 DEAD
            // insert 7 LIVE
            gameBoard[r, c + 28] = LIVE;
            gameBoard[r, c + 29] = LIVE;
            gameBoard[r, c + 30] = LIVE;
            gameBoard[r, c + 31] = LIVE;
            gameBoard[r, c + 32] = LIVE;
            gameBoard[r, c + 33] = LIVE;
            gameBoard[r, c + 34] = LIVE;
        }

        // set every cell to DEAD
        private void InitializeGameBoards()
        {
            for (int r = 0; r < ROW_SIZE; r++)
            {
                for (int c = 0; c < COL_SIZE; c++)
                {
                    gameBoard[r, c] = DEAD;
                    buffBoard[r, c] = DEAD;
                }
            }
        }

        internal void PlayTheGame()
        {
            Console.Write("Enter the number of generations to display: ");
            int numGenerations = int.Parse(Console.ReadLine());

            for (int generation = 1; generation <= numGenerations; generation++)
            {
                //display the game board
                DisplayCurrentGameBoard(generation);

                //process the game board - prepare the next generation
                ProcessGameBoard();

                //swap the two boards
                SwapTheGameBoard();
            }
        }

        // SwapTheGameBoard explanation
        // gameBoard (gen1) registers cells on buffBoard (gen2) based on standard B3/S23
        // board swaps when the next generation displays => buffBoard (gen2) becomes gameBoard (gen2)
        // gameBoard (gen2) registers cells on buffBoard (gen3) =.... cycle continues 
        private void SwapTheGameBoard()
        {
            // tmp = A
            char[,] tmp = gameBoard;
            gameBoard = buffBoard;
            buffBoard = tmp;
        }

        private void ProcessGameBoard()
        {
            //iterate through the entire 2 dimensional array
            for (int r = 0; r < ROW_SIZE; r++)
            {
                for (int c = 0; c < COL_SIZE; c++)
                {
                    // consider the current cell (location r,c); determine if this cell
                    // will be live or dead in the next generation (on the buffer board)
                    // calculate which state to store in the results
                    buffBoard[r, c] = DetermineLifeOrDeath(r, c);
                }
            }


        }

        // now we are considering an individual cell in the game board
        // how do we determine life or death
        private char DetermineLifeOrDeath(int r, int c)
        {
            // 1 - count the number of neighbor cells that currently hold LIVE organism
            int count = GetNeighborCount(r, c);

            // 2 - apply the rule of the game (standard B3/S23)

            //Any live cell with fewer than two lives neighbours dies, as if by underpopulation.
            //if (gameBoard[r, c] == LIVE && count < 2) return DEAD;

            //Any live cell with two or three live neighbour a lives on to the next generation.
            //if (gameBoard[r,c] == LIVE && (count ==2 || count == 3)) return LIVE;

            //Any live cell with more than three live neighbours dies, as if by over population.
            //if (gameBoard[r, c] == LIVE && (count > 3)) return DEAD;

            //Any dead cell with exactly three live neighbours beomes a live cell, as if by reproduction.
            //if (gameBoard[r, c] == DEAD && (count == 3)) return LIVE;

            ///////////////////////////////////////////////////
            //same as above comments but concise
            if (count == 2) return gameBoard[r, c];
            else if (count == 3) return LIVE;
            else return DEAD;
        }

        private int GetNeighborCount(int r, int c)
        {
            int neighborCount = 0;
            if (r == 0 && c == 0)
            {
                // TOP LEFT corner
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighborCount++;
            }

            else if (r == 0 && c == COL_SIZE - 1)
            {
                // TOP RIGHT corner
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;

            }

            else if (r == ROW_SIZE - 1 && c == COL_SIZE - 1)
            {
                // BOTTOM RIGHT corner
                if (gameBoard[r - 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
            }

            else if (r == ROW_SIZE - 1 && c == 0)
            {
                // BOTTOM LEFT corner
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
            }

            else if (r==0)
            {
                //top edge (not corner)
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighborCount++;
            }

            else if (c == COL_SIZE -1)
            {
                //right edge (not corner)
                if (gameBoard[r - 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
            }

            else if (r == ROW_SIZE -1)
            {
                //bottom edge (not corner)
                if (gameBoard[r - 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
            }

            else if (c == 0)
            {
                //left edge (not corner)
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighborCount++;
            }

            else
            {
                //nominal case
                if (gameBoard[r - 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
                //[r,c]
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighborCount++;
            }

            return neighborCount;
        }

        private void DisplayCurrentGameBoard(int gen)
        {
            Console.WriteLine($"Displaying generation #{gen}");
            
            // nested for loops to go through every element in the board
            for (int r = 0; r < ROW_SIZE; r++)
            {
                for (int c = 0; c < COL_SIZE; c++)
                {
                    Console.Write($"{SPACE}{gameBoard[r, c]}");
                }
                Console.WriteLine();
            }
        }
    }
}