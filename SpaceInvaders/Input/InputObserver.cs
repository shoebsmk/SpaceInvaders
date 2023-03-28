//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class InputObserver : SLink
    {
        public enum Name
        {
            MoveLeftObserver,
            MoveRightObserver,
            ShootObserver,
            Uninitialized
        }

        // define this in concrete
        abstract public void Notify();

        override public void Wash()
        {
            Debug.Assert(false);
        }

        public InputSubject pSubject;
    }
}

// --- End of File ---
