﻿//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveLeftObserver : InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("Move Left");
            Ship pShip = ShipMan.GetShip();
            pShip.MoveLeft();
        }


        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.MoveLeftObserver;
        }

    }
}

// --- End of File ---
