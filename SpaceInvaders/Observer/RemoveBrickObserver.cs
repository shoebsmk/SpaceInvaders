//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBrickObserver : ColObserver
    {
        public RemoveBrickObserver()
        {
            this.pBrick = null;
        }
        public RemoveBrickObserver(RemoveBrickObserver b)
        {
            Debug.Assert(b != null);
            this.pBrick = b.pBrick;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pBrick = (ShieldBrick)this.pSubject.pObjB;
            Debug.Assert(this.pBrick != null);

            if (pBrick.bMarkForDeath == false)
            {
                pBrick.bMarkForDeath = true;
                //   Delay
                RemoveBrickObserver pObserver = new RemoveBrickObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pBrick;
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
            //Add later
            // Grid 
            if (pC.GetNumChildren() == 0)
            {
                pC.Remove();

            }
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
            return ColObserver.Name.RemoveBrickObserver;
        }



        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pBrick;
    }
}

// --- End of File ---
