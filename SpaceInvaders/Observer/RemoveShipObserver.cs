//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveShipObserver : ColObserver
    {
        public RemoveShipObserver()
        {
            this.pShip = null;
        }
        public RemoveShipObserver(RemoveShipObserver b)
        {
            Debug.Assert(b != null);
            this.pShip = b.pShip;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pShip = (Ship)this.pSubject.pObjB;


            Debug.Assert(this.pShip != null);

            pShip.pSpriteProxy.Set(SpriteGame.Name.PlayerExplosionB);


            if (pShip.bMarkForDeath == false)
            {
                pShip.bMarkForDeath = true;
                //   Delay
                RemoveShipObserver pObserver = new RemoveShipObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pShip;

            if(pShip != null)
            {

                TimerEventMan.Add(TimerEvent.Name.ShipExplosion, new ShipExplosionCommand(SpriteGame.Name.PlayerExplosionB, pShip), 0.4f);
            }


/*
            // Brick
            pA.Dump();

            if (pA.GetNumChildren() == 0)
            {
                pA.Remove();
            }
*/
            
        }
        private bool privCheckParent(GameObject pObj)
        {
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(pObj);
            if (pGameObj == null)
            {
                return true;
            }

            return false;
        }
        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveShipObserver;
        }



        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pShip;
    }
}

// --- End of File ---
