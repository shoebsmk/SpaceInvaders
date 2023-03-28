//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStateDead : ShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipMan.State.Ready);
        }
        public override void MoveRight(Ship pShip)
        {

        }
        public override void MoveLeft(Ship pShip)
        {

        }
        public override void ShootMissile(Ship pShip)
        {

        }
    }
}

// --- End of File ---
