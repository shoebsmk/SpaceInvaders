﻿//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShipMoveState
    {
        public abstract void Handle(Ship pShip);
        public abstract void MoveRight(Ship pShip);
        public abstract void MoveLeft(Ship pShip);
    }
}
// --- End of File ---
