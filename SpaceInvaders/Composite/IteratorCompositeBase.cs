//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class IteratorCompositeBase
    {
        abstract public Component Next();
        abstract public bool IsDone();
        abstract public Component First();
        abstract public Component Curr();
    }
    
}

// --- End of File ---
