//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    abstract public class ColObserver : SLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            SoundObserver,
            GridObserver,
            ShipReadyObserver,
            ShipRemoveMissileObserver,
            BombObserver,
            RemoveBrickObserver,
            RemoveMissileObserver,
            RemoveAlienObserver,
            RemoveBombObserver,
            ShipMoveObserver,
            RemoveShipObserver,
            ShipRemoveMissileObserverBomb,

            Uninitialized
        }
        public abstract void Notify();

        // WHY not add a Command pattern into our Observer!
        public virtual void Execute()
        {
            // default implementation
        }

        override public void Wash()
        {
            Debug.Assert(false);
        }

        public ColSubject pSubject;
    }

    //override public void Dump()
    //{
    //    Debug.Assert(false);
    //}
    //override public System.Enum GetName()
    //{
    //    Debug.Assert(false);
    //}
}

// --- End of File ---
