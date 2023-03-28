//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipRemoveMissileObserverBomb : ColObserver
    {
        public ShipRemoveMissileObserverBomb()
        {
            this.pMissile = null;
        }

        public ShipRemoveMissileObserverBomb(ShipRemoveMissileObserverBomb m)
        {
            Debug.Assert(m.pMissile != null);
            this.pMissile = m.pMissile;
            pMissile.pSpriteProxy.Set(SpriteGame.Name.PlayerShotExplosion);
            pMissile.SetActive(false);
        }

        public override void Notify()
        {
            // Delete missile
            // Debug.WriteLine("ShipRemoveMissileObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            // At this point we have two game objects
            // Actually we can control the objects in the visitor
            // Alphabetical ordering... A is missile,  B is wall

            // This cast will throw an exception if I'm wrong
            this.pMissile = (Missile)this.pSubject.pObjB;

            // Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", pMissile);
            /*this.pMissile.Remove();*/
            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                ShipRemoveMissileObserverBomb pObserver = new ShipRemoveMissileObserverBomb(this);
                DelayedObjectMan.Attach(pObserver);
            }

        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            //            this.pMissile.Remove();
            TimerEventMan.Add(TimerEvent.Name.AlienExplosion, new MissileExplosionCommand(SpriteGame.Name.MissileShotExplosion, pMissile), 0.08f);

        }


        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.ShipRemoveMissileObserverBomb;
        }

        // data
        private Missile pMissile;


    }
}

// --- End of File ---
