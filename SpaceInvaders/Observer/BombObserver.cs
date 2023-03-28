//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombObserver : ColObserver
    {
        public BombObserver()
        {
            this.pBomb = null;
        }
        public BombObserver(BombObserver b)
        {
            Debug.Assert(b != null);
            this.pBomb = b.pBomb;
        }

        public override void Notify()
        {
            //Debug.WriteLine("BombObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            
            pBomb = (Bomb)this.pSubject.pObjA;
            Debug.Assert(this.pBomb != null);
            pBomb.pSpriteProxy.Set(SpriteGame.Name.AlienShotExplosion);
            pBomb.SetActive(false);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;
                //   Delay
                BombObserver pObserver = new BombObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
            //pBomb.Remove();
        }
        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);

            TimerEventMan.Add(TimerEvent.Name.AlienShotExplosion, new BombExplosionCommand(pBomb), 0.2f);

        }
        // ------------------------------------
        // Data
        // ------------------------------------

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.BombObserver;
        }
        private Bomb pBomb;

    }
}

// --- End of File ---
