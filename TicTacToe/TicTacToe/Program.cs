// Jason Choi
// TINFO-200B
// Cs3 - TicTacToe
// The program initializes a well known 3 x 3 tic-tac-toe game. Refer to the details on game.cs
// Two different game mode: 1. player vs player and 2. player vs computer
// The game keeps the record of the score.
////////////////////////////////////////////////////////////
// Change History
// Date         Developer       Description
// 20220302     hyunc16         creation of project
// 20220302     hyunc16         fully programmed and commented
// 20220302     hyunc16         added header
// 20220302     hyunc16         tested and created transcript file of output.txt 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game TicTacToe3x3 = new Game();
            TicTacToe3x3.Start();
        }
    }
}
