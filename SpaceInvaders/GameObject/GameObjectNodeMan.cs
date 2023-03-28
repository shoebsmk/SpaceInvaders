//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameObjectNodeMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //-----------------------------------------------------------
        //LTN - ManagerBase
        private GameObjectNodeMan(int reserveNum, int reserveGrow)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            // initialize derived data here
            // LTN - GameObjectNodeMan
            GameObjectNodeMan.psGameObjectNodeCompare = new GameObjectNode();
            // LTN - GameObjectNodeMan
            GameObjectNodeMan.psGameObj = new GameObjectNull();
            // LTN - GameObjectNodeMan
            GameObjectNodeMan.psGameObjectNodeCompare.pGameObj = GameObjectNodeMan.psGameObj;
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 8, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(psInstance == null);

            // Do the initialization
            if (psInstance == null)
            {
                // LTN - GameObjectNodeMan
                psInstance = new GameObjectNodeMan(reserveNum, reserveGrow);
            }
        }
        public static void Destroy(bool bPrintEnable = false)
        {
            GameObjectNodeMan pMan = GameObjectNodeMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
                GameObjectNodeMan.DumpStats();
            }
        }
        public static GameObjectNode Attach(GameObject pGameObject)
        {
            GameObjectNodeMan pMan = GameObjectNodeMan.privGetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pNode = (GameObjectNode)pMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pGameObject);
            return pNode;
        }

        public static GameObject Find(GameObject.Name name)
        {
            GameObjectNodeMan pMan = GameObjectNodeMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            Debug.Assert(GameObjectNodeMan.psGameObjectNodeCompare != null);

            Debug.Assert(GameObjectNodeMan.psGameObjectNodeCompare.pGameObj != null);
            GameObjectNodeMan.psGameObjectNodeCompare.pGameObj.name = name;

            GameObjectNode pData = (GameObjectNode)pMan.baseFind(GameObjectNodeMan.psGameObjectNodeCompare);

            GameObject pGameObject = null;
            if (pData != null)
            {
                pGameObject = pData.pGameObj;
            }
            return pGameObject;
        }
        public static void Update()
        {
            GameObjectNodeMan pMan = GameObjectNodeMan.privGetInstance();
            Debug.Assert(pMan != null);

            // walk through the list and render
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                GameObjectNode pGameObjectNode = (GameObjectNode)pIt.Current();
                GameObject pRoot = pGameObjectNode.pGameObj;
                Debug.Assert(pRoot != null);

                // Debug.WriteLine("root: {0}", pRoot.GetName());

                IteratorReverseComposite pRev = new IteratorReverseComposite(pRoot);
                for (pRev.First(); !pRev.IsDone(); pRev.Next())
                {
                    GameObject pTmp = (GameObject)pRev.Curr();
                    pTmp.Update();
                    //  pTmp.Dump();
                }

            }
        }

        public static void Remove(GameObject pNode)
        {
            // Keenan(delete.E)
            Debug.Assert(pNode != null);
            GameObjectNodeMan pMan = GameObjectNodeMan.privGetInstance();

            GameObject pSafetyNode = pNode;

            // OK so we have a linked list of trees (Remember that)

            // 1) find the tree root (we already know its the most parent)

            GameObject pTmp = pNode;
            GameObject pRoot = null;
            while (pTmp != null)
            {
                pRoot = pTmp;
                pTmp = (GameObject)IteratorForwardComposite.GetParent(pTmp);
            }

            // 2) pRoot is the tree we are looking for
            // now walk the active list looking for pRoot

            Iterator pIt = pMan.baseGetIterator();
            GameObjectNode pTree = (GameObjectNode)pIt.First();

            while (!pIt.IsDone())
            {
                if (pTree.pGameObj == pRoot)
                {
                    // found it
                    break;
                }
                pTree = (GameObjectNode)pIt.Next();
            }

            // 3) pTree is the tree that holds pNode
            //  Now remove the node from that tree

            Debug.Assert(pTree != null);
            Debug.Assert(pTree.pGameObj != null);

            // Is pTree.poGameObj same as the node we are trying to delete?
            // Answer: should be no... since we always have a group (that was a good idea)

            Debug.Assert(pTree.pGameObj != pNode);

            GameObject pParent = (GameObject)IteratorForwardComposite.GetParent(pNode);
            Debug.Assert(pParent != null);

            GameObject pChild = (GameObject)IteratorForwardComposite.GetChild(pNode);
            Debug.Assert(pChild == null);

            // remove the node
            pParent.Remove(pNode);

            // TODO - Recycle pNode

        }
        public static void Dump()
        {
            Debug.WriteLine("\n   ------ GameObjectNode Man: ------");

            GameObjectNodeMan pMan = GameObjectNodeMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ GameObjectNode Man: ------");

            GameObjectNodeMan pMan = GameObjectNodeMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            // STN - Only to create GameObjectNode
            NodeBase pNodeBase = new GameObjectNode();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //------------------------------------
        // Private methods
        //------------------------------------
        private static GameObjectNodeMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static GameObjectNode psGameObjectNodeCompare;
        private static GameObjectNull psGameObj;
        private static GameObjectNodeMan psInstance = null;

    }
}

// --- End of File ---
