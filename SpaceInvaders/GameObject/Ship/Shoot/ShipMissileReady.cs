//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMissileReady : ShipMissileState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipMan.MissileState.Flying);
        }

        public override void ShootMissile(Ship pShip)
        {
            Missile pMissile = ShipMan.ActivateMissile();
            pMissile.SetPos(pShip.x, pShip.y + 20);

            this.Handle(pShip);
        }

    }
}// --- End of File ---
