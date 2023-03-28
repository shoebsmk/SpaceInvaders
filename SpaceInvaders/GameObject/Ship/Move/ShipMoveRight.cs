//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMoveRight : ShipMoveState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipMan.MoveState.MoveBoth);
        }

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
            this.Handle(pShip);
        }
        public override void MoveLeft(Ship pShip)
        {
            
        }

    }
}// --- End of File ---
