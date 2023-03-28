//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameObjectNode : DLink
    {

        //------------------------------------
        // Constructor
        //------------------------------------

        public GameObjectNode()
            :base()
        {
            this.privClear();
        }

        //------------------------------------
        // Methods
        //------------------------------------

        public void Set(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            this.pGameObj = pGameObject;
        }

        private void privClear()
        {
            this.pGameObj = null;
        }

        //------------------------------------
        // Override
        //------------------------------------

        public override System.Enum GetName()
        {
            System.Enum name;
            if (this.pGameObj == null)
            {
                name = null;
            }
            else
            {
                name = this.pGameObj.GetName();
            }
            return name;
        }

        override public void Wash()
        {
            this.privClear();
        }

        override public bool Compare(NodeBase pTarget)
        {
            // This is used in ManBase.Find() 
            Debug.Assert(pTarget != null);

            GameObjectNode pDataB = (GameObjectNode)pTarget;

            bool status = false;

            Debug.Assert(pDataB.pGameObj != null);
            Debug.Assert(this.pGameObj != null);

            if (this.pGameObj.GetName().GetHashCode() == pDataB.pGameObj.GetName().GetHashCode())
            {
                status = true;
            }

            return status;
        }

        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   GameObjectNode: ({0})", this.GetHashCode());

            // Data:
            if (this.pGameObj != null)
            {
                Debug.WriteLine("      GameObject.name: {0} ({1})", this.pGameObj.GetName(), this.pGameObj.GetHashCode());
            }
            else
            {
                Debug.WriteLine("      GameObject.name: null");
            }

            // Let the base print its contribution
            this.baseDump();
        }

        //----------------------------------
        // Data
        //----------------------------------
        public GameObject pGameObj;
    }
}

// --- End of File ---
