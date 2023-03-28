//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveAlienObserver : ColObserver
    {
        public RemoveAlienObserver()
        {
            this.pAlien = null;
        }
        public RemoveAlienObserver(RemoveAlienObserver b)
        {
            Debug.Assert(b != null);
            this.pAlien = b.pAlien;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pAlien = (AlienBase)this.pSubject.pObjB;
            Debug.Assert(this.pAlien != null);
            pAlien.pSpriteProxy.Set(SpriteGame.Name.AlienExplosion);

            

            if (pAlien.bMarkForDeath == false)
            {
                pAlien.bMarkForDeath = true;
                //   Delay
                RemoveAlienObserver pObserver = new RemoveAlienObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
            //Add spalt
        }
        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);

            TimerEventMan.Add(TimerEvent.Name.AlienExplosion, new ExplosionCommand(SpriteGame.Name.AlienExplosion, pAlien), 0.2f);
/*

            GameObject pA = (GameObject)this.pAlien;
            GameObject pB = (GameObject)IteratorForwardComposite.GetParent(pA);
            GameObject pC = (GameObject)IteratorForwardComposite.GetParent(pB);

            // Root - do not delete
            //GameObject pD = (GameObject)IteratorForwardComposite.GetParent(pC);

            // Brick
            if (pA.GetNumChildren() == 0)
            {
                pA.Remove();
            }

            // Column 
            if (pB.GetNumChildren() == 0)
            {
                pB.Remove();
            }

            // Grid 
            if (pC.GetNumChildren() == 0)
            {
                pC.Remove();
            }*/
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
            return ColObserver.Name.RemoveAlienObserver;
        }



        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pAlien;
        private GameObject pExplosion;
    }
}

// --- End of File ---
