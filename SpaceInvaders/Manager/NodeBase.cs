//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class NodeBase
    {
        // these are needed to make the ManagerBase work
        abstract public void Wash();
        abstract public void Dump();
        abstract public System.Enum GetName();

        virtual public bool Compare(NodeBase pNodeBaseB)
        {
            // This is used in baseFind() 
            Debug.Assert(pNodeBaseB != null);
            bool status = false;

            // Why doesn't GetName() work without GetHashCode?
            // Debug.WriteLine("cmp {0} {1} \n", this.GetName().GetHashCode(), pNodeBaseB.GetName().GetHashCode());
            if (this.GetName().GetHashCode() == pNodeBaseB.GetName().GetHashCode())
            {
                status = true;
            }

            return status;
        }
        //abstract public void Clear();
    }
}

// --- End of File ---

