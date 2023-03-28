//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SLink : NodeBase
    {
        // ------------------------------
        // Add CODE/REFACTOR here
        // ------------------------------
        protected SLink() : base()
        {
            this.baseClear();
        }

        public void Clear()
        {
            this.baseClear();
        }

        protected void baseClear()
        {
            this.pNext = null;
        }

        /*override public object GetName()
        {
            return null;
        }*/

        protected void baseDump()
        {
           if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                NodeBase pTmp = (NodeBase)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.GetName(), pTmp.GetHashCode());
            }
        }

        // ------------------------------
        // Data:
        // ------------------------------
        public SLink pNext;

    }
}

// --- End of File ---
