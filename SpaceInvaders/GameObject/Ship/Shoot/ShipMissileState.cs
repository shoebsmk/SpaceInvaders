//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShipMissileState
    {
        // Transitions to correct state
        public abstract void Handle(Ship pShip);

        public abstract void ShootMissile(Ship pShip);
    }
}// --- End of File ---
