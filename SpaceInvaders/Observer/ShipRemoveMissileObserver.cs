//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipRemoveMissileObserver : ColObserver
    {
        public ShipRemoveMissileObserver()
        {
            this.pMissile = null;
        }

        public ShipRemoveMissileObserver(ShipRemoveMissileObserver m)
        {
            Debug.Assert(m.pMissile != null);
            this.pMissile = m.pMissile;
        }

        public override void Notify()
        {
            // Delete missile
            // Debug.WriteLine("ShipRemoveMissileObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            // At this point we have two game objects
            // Actually we can control the objects in the visitor
            // Alphabetical ordering... A is missile,  B is wall

            // This cast will throw an exception if I'm wrong
            this.pMissile = (Missile)this.pSubject.pObjA;
            pMissile.pSpriteProxy.Set(SpriteGame.Name.PlayerShotExplosion);
            pMissile.SetActive(false);


            // Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", pMissile);

            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                ShipRemoveMissileObserver pObserver = new ShipRemoveMissileObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }

        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            TimerEventMan.Add(TimerEvent.Name.AlienExplosion, new MissileExplosionCommand(SpriteGame.Name.MissileShotExplosion, pMissile), 0.08f);

            //this.pMissile.Remove();
        }


        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.ShipRemoveMissileObserver;
        }

        // data
        private Missile pMissile;


    }
}

// --- End of File ---
