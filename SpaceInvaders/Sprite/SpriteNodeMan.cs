//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteNodeMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        
        public SpriteNodeMan(int reserveNum = 3, int reserveGrow = 1)
                //LTN - SpriteNodeMan
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {

            this.pBackSpriteBatch = null;
            // initialize derived data here
            //LTN - SpriteNodeMan
            psSpriteNodeCompare = new SpriteNode();
        }

        public void Set(SpriteBatch.Name name, int reserveNum, int reserveGrow)
        {
            this.name = name;

            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            this.baseSetReserve(reserveNum, reserveGrow);
        }
        //Changed to base
        public SpriteNode Attach(SpriteBase pNode)
        {
            /*SpriteNodeMan pMan = SpriteNodeMan.privGetInstance();
            Debug.Assert(pMan != null);*/

            SpriteNode pSpriteNode = (SpriteNode) this.baseAdd();
            Debug.Assert(pSpriteNode != null);

            // Initialize the data
            pSpriteNode.Set(pNode);
            return pSpriteNode;
        }

        public SpriteNode Attach(SpriteGameProxy pNode)
        {
            // Go to Man, get a node from reserve, add to active, return it
            SpriteNode pSpriteNode = (SpriteNode)this.baseAdd();
            Debug.Assert(pSpriteNode != null);

            // Initialize SpriteBatchNode
            pSpriteNode.Set(pNode);

            return pSpriteNode;
        }

        public SpriteNode Attach(SpriteBox.Name name)
        {
            /*SpriteNodeMan pMan = SpriteNodeMan.privGetInstance();
            Debug.Assert(pMan != null);*/

            SpriteNode pSpriteNode = (SpriteNode) this.baseAdd();
            Debug.Assert(pSpriteNode != null);

            // Initialize the data
            pSpriteNode.Set(name);
            return pSpriteNode;
        }

        public void Draw()
        {
            
            // walk through the list and render
            Iterator pIt = this.baseGetIterator();
            Debug.Assert(pIt != null);

            // iterate through the nodes
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                // Downcast (its OK - homogeneous list)
                // Assumes someone before here called update() on each sprite
                SpriteNode pNode = (SpriteNode)pIt.Current();
                pNode.pSpriteBase.Render();
            }
        }
        public void Remove(SpriteNode pSpriteNode)
        {
            /*SpriteNodeMan pMan = SpriteNodeMan.privGetInstance();
            Debug.Assert(pMan != null);*/

            Debug.Assert(pSpriteNode != null);
            this.baseRemove(pSpriteNode);
        }
        public void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteNode Man: ------");

            this.baseDump();

        }
        public void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteNode Man: ------");


            this.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }



        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }
        public void SetSpriteBatch(SpriteBatch _pSpriteBatch)
        {
            this.pBackSpriteBatch = _pSpriteBatch;
        }

        //------------------------------------
        // Override Abstract methods
        //------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            //STN - Create and return node
            NodeBase pNodeBase = new SpriteNode();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }
        
       /* override protected void derivedDumpNode(NodeBase pSpriteNodeBase)
        {
            Debug.Assert(pSpriteNodeBase != null);
            SpriteNode pData = (SpriteNode)pSpriteNodeBase;
            pData.Dump();
        }*/

        //------------------------------------
        // Private methods
        //------------------------------------
       
        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static SpriteNode psSpriteNodeCompare;
        private SpriteBatch.Name name;
        // Keenan(delete.D)
        private SpriteBatch pBackSpriteBatch;

    }
}

// --- End of File ---
