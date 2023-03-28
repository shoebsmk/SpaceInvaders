//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColPairMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private ColPairMan(int reserveNum, int reserveGrow)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            // no link... used in Process
            this.pActiveColPair = null;

            // initialize derived data here
            this.poNodeCompare = new ColPair();
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 1, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new ColPairMan(reserveNum, reserveGrow);
            }

        }
        public static void Destroy()
        {
            ColPairMan pMan = ColPairMan.privGetInstance();
            Debug.Assert(pMan != null);
        }
        public static ColPair Add(ColPair.Name colpairName, GameObject treeRootA, GameObject treeRootB)
        {
            // Get the instance
            ColPairMan pMan = ColPairMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Go to Man, get a node from reserve, add to active, return it
            ColPair pColPair = (ColPair)pMan.baseAdd();
            Debug.Assert(pColPair != null);

            // Initialize Image
            pColPair.Set(colpairName, treeRootA, treeRootB);

            return pColPair;
        }

        public static void Process()
        {
            // get the singleton
            ColPairMan pMan = ColPairMan.privGetInstance();

            // walk through the list and render
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            for(pIt.First();!pIt.IsDone();pIt.Next())
            { 
                ColPair pNode = (ColPair)pIt.Current();
                Debug.Assert(pNode != null);

                // set the current active  <--- Key concept: set this before
                pMan.pActiveColPair = pNode;

                pNode.Process();
            }

        }

        public static ColPair GetActiveColPair()
        {
            // get the singleton
            ColPairMan pMan = ColPairMan.privGetInstance();

            return pMan.pActiveColPair;
        }

        public static ColPair Find(ColPair.Name name)
        {
            ColPairMan pMan = ColPairMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.name = name;

            ColPair pData = (ColPair)pMan.baseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Remove(ColPair pNode)
        {
            Debug.Assert(pNode != null);

            ColPairMan pMan = ColPairMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseRemove(pNode);
        }
        public static void Dump()
        {
            ColPairMan pMan = ColPairMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static ColPairMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new ColPair();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private readonly ColPair poNodeCompare;
        private static ColPairMan pInstance = null;
        private ColPair pActiveColPair;
    }
}

// --- End of File ---
