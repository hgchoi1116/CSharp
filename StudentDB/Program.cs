// header

// description

// change history

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    internal class Program
    {
        // main entry point 
        static void Main(string[] args)
        {
            // make a singleton object for the databse app itself
            DbApp database = new DbApp();

            // execute the application main loop
            database.Run();

        }

    }
}
