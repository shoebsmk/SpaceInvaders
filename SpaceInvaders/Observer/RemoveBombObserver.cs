//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBombObserver : ColObserver
    {
        public RemoveBombObserver()
        {
            this.pBomb = null;
        }
        public RemoveBombObserver(RemoveBombObserver b)
        {
            this.pBomb = b.pBomb;
        }
        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBombObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pBomb = (Bomb)this.pSubject.pObjA;
            Debug.Assert(this.pBomb != null);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;
                //   Delay
                RemoveBombObserver pObserver = new RemoveBombObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pBomb.Remove();
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveBombObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pBomb;
    }
}

// --- End of File ---
