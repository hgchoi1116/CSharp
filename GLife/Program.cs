// Jason Choi
// TINFO-200B
// L5Life
// The program is called The Game of Life or Life. The user will specify the number of generations
// to display. From the initial startup pattern, each cell will follow a specific rules to survive or die in each generation.
// Rule1: Any LIVE cell with two or three live neighbours survives.
// Rule2: Any DEAD cell with three live neighbours becomes a live cell.
// Rule3: All other live cells die in the next generation. Similarly, all other dead cells stay dead.
// Enjoy the game!

//////////////////////////////////////////////////////////////
// Change History
// Date     Developer       Description
// 01242022 hyunc16         creation of a new project
// 01242022 hyunc16         creation of a class Game and different methods in game
// 01262022 hyunc16         created a method ProcessGameBoard()
// 01262022 hyunc16         debugged and tested
// 01312022 hyunc16         updated header, comments, UI
// 01312022 hyunc16         collected Game of Life background and instrution
//      https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLife
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Intro message explaining the gam
            Console.WriteLine(@"
********************************************************************************************
                                 Welcome to the Game of Life
********************************************************************************************
Background - from Wikipedia
The Game of Life, also known simply as Life, is a cellular automaton devised by the British
mathematician John Horton Conway in 1970.It is a zero-player game, meaning that its evolution
is determined by its initial state, requiring no further input. One interacts with the Game
of Life by creating an initial configuration and observing how it evolves. It is Turing
complete and can simulate a universal constructor or any other Turing machine.

Rule:
1. Any LIVE cell with two or three live neighbours survives.
2. Any DEAD cell with three live neighbours becomes a live cell.
3. All other live cells die in the next generation. Similarly, all other dead cells stay dead.

Instruction:
1. The user will specify the number of generation to display (only integer).
2. Enjoy the change of each cell in each generation!
");
            // want to be able to create a game obj and call the ctor
            // to kick off the simulation

            Game game = new Game();
            game.PlayTheGame();

            // send-off signaling end of app
            Console.WriteLine("Thank you for playing Game of Life");
        }
    }
}
