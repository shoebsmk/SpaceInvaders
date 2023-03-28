//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatchMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public SpriteBatchMan(int reserveNum = 1, int reserveGrow = 1)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            SpriteBatchMan.psActiveSBMan = null;
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create()
        {
           

            // initialize the singleton here
            Debug.Assert(psInstance == null);

            // Do the initialization
            if (psInstance == null)
            {
                // LTN - SpriteBatchMan - Singleton
                psInstance = new SpriteBatchMan();
            }
        }
        public static void Destroy(bool bPrintEnable = false)
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
                SpriteBatchMan.DumpStats();
            }
        }

        public static SpriteBatch Add(SpriteBatch.Name name, int reserveNum = 3, int reserveGrow = 1)
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.baseAdd();
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.priority = 1;

            // Initialize the data
            pSpriteBatch.Set(name, reserveNum, reserveGrow);
            return pSpriteBatch;
        }

        public static SpriteBatch AddPriority(SpriteBatch.Name name, uint _priority = uint.MaxValue, int reserveNum = 3, int reserveGrow = 1)
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            //if no SB inserted yet -- create batch - add to front p set priority
            //SpriteBatch pSpriteBatch = (SpriteBatch) pMan.baseAdd();
            
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);
            if(pIt.First() == null)
            {
                SpriteBatch pSpriteBatchF = (SpriteBatch)pMan.baseAdd();
                Debug.Assert(pSpriteBatchF != null);

                // Initialize the data
                pSpriteBatchF.priority = _priority;
                pSpriteBatchF.Set(name, reserveNum, reserveGrow);
                return pSpriteBatchF;
            } else {
                // iterate through the nodes
                for (pIt.First(); !pIt.IsDone(); pIt.Next())
                {
                    // Downcast (its OK - homogeneous list)
                    // Assumes someone before here called update() on each sprite

                    SpriteBatch pTSpriteBatch = (SpriteBatch)pIt.Current();
                    if(pTSpriteBatch.priority > _priority)
                    {
                        SpriteBatch pSpriteBatchN = (SpriteBatch)pMan.baseAddPriority(pTSpriteBatch);
                        pSpriteBatchN.priority = _priority;
                        Debug.Assert(pSpriteBatchN != null);
                        // Initialize the data
                        pSpriteBatchN.Set(name, reserveNum, reserveGrow);
                        return pSpriteBatchN;
                    } else if (pTSpriteBatch.pNext == null)
                    {
                        SpriteBatch pSpriteBatchN = (SpriteBatch)pMan.baseAddToEnd();
                        pSpriteBatchN.priority = _priority;
                        Debug.Assert(pSpriteBatchN != null);
                        // Initialize the data
                        pSpriteBatchN.Set(name, reserveNum, reserveGrow);
                        return pSpriteBatchN;
                    }
                }

            }
            return null;



            
        }

        public static void Draw()
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            // walk through the list and render
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            // iterate through the nodes
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                // Downcast (its OK - homogeneous list)
                // Assumes someone before here called update() on each sprite
                SpriteBatch pSpriteBatch = (SpriteBatch)pIt.Current();
                if(pSpriteBatch.enableDraw == true)
                {
                    pSpriteBatch.GetSpriteNodeMan().Draw();
                }
            }
        }
        public static SpriteBatch Find(SpriteBatch.Name name)
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            // Compare functions only compares two SpriteBatchs

            // So:  Use the Compare SpriteBatch - as a reference
            //      use in the Compare() function
            SpriteBatchMan.psSpriteBatchCompare.name = name;

            SpriteBatch pData = (SpriteBatch)pMan.baseFind(SpriteBatchMan.psSpriteBatchCompare);
            return pData;
        }
        public static void Remove(SpriteBatch pSpriteBatch)
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            Debug.Assert(pSpriteBatch != null);
            pMan.baseRemove(pSpriteBatch);
        }

        public static void Remove(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            SpriteNodeMan pSpriteNodeMan = pSpriteBatchNode.GetSBNodeMan();

            Debug.Assert(pSpriteNodeMan != null);
            pSpriteNodeMan.Remove(pSpriteBatchNode);
        }
        public static void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteBatch Man: ------");

            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            pMan.baseDump();

        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteBatch Man: ------");

            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }

        public static void SetActive(SpriteBatchMan pSBMan)
        {
            SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pSBMan != null);
            SpriteBatchMan.psActiveSBMan = pSBMan;
        }

        public static void toggleBoxesDisable()
        {
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            pSB_Boxes.enableDraw = false;
            Debug.WriteLine("Disabled Box Sprites");

        }

        public static void toggleBoxesEnable()
        {
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            pSB_Boxes.enableDraw = true;
            Debug.WriteLine("Enabled Box Sprites");
        }

        //------------------------------------
        // Override Abstract methods
        //------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            // STN create and retun 
            NodeBase pNodeBase = new SpriteBatch();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }
        
        
        
        

        //------------------------------------
        // Private methods
        //------------------------------------
        private static SpriteBatchMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }
        


        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static SpriteBatch psSpriteBatchCompare = new SpriteBatch();
        private static SpriteBatchMan psActiveSBMan = null;
        private static SpriteBatchMan psInstance = null;
    }
}

// --- End of File ---
