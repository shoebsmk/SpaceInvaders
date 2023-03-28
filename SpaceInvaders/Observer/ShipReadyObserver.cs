﻿//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipReadyObserver : ColObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipMan.GetShip();

            pShip.SetState(ShipMan.MissileState.Ready);

        }

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.ShipReadyObserver;
        }
    }

    // data
}

// --- End of File ---
