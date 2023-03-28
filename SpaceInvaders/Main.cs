//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the instance
            // LTN - SpaceInvaders 
            SpaceInvaders game = new SpaceInvaders();
            Debug.Assert(game != null);

            // Start the game
            game.Run();
        }
    }
}

// --- End of File ---
