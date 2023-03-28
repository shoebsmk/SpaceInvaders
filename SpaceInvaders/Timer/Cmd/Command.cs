//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Command
    {
        // define this in concrete
        abstract public void Execute(float deltaTime);
    }
}

// --- End of File ---
