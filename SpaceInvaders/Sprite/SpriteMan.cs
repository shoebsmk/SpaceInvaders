//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpriteMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        // Changes added getprivIns, made instance private

        private SpriteMan( int reserveNum = 3, int reserveGrow = 1)
                : base(new SLinkMan(), new SLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            // initialize derived data here
            poSpriteCompare = new Sprite();
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        private static SpriteMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(psInstance == null);

            // Do the initialization
            if (psInstance == null)
            {
                psInstance = new SpriteMan(reserveNum, reserveGrow);
            }
        }
        public static void Destroy()
        {
            SpriteMan pMan = SpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton

            ImageMan.DumpStats();
        }

        public static Sprite Add(Sprite.Name name, Image pImage, Azul.Rect pScreenRect)
        {
            SpriteMan pMan = SpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pImage != null);
            Debug.Assert(pScreenRect != null);

            Sprite pSprite = (Sprite)pMan.baseAdd();
            Debug.Assert(pSprite != null);

            // Initialize the data
            pSprite.Set(name, pImage, pScreenRect);
            return pSprite;
        }

        public static Sprite Find(Sprite.Name name)
        {
            SpriteMan pMan = SpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Sprites

            // So:  Use the Compare Sprite - as a reference
            //      use in the Compare() function

            SpriteMan.poSpriteCompare.name = name;

            Sprite pData = (Sprite) pMan.baseFind(SpriteMan.poSpriteCompare);
            return pData;
        }
        public static void Remove(Sprite pSprite)
        {
            SpriteMan pMan = SpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pSprite != null);
            pMan.baseRemove(pSprite);
        }
        public static void Dump()
        {
            Debug.WriteLine("\n ----- Sprite Man: -----");

            SpriteMan pMan = SpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ Image Man: ------");

            SpriteMan pMan = SpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new Sprite();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }
        override protected bool derivedCompare(NodeBase pSpriteBaseA, NodeBase pSpriteBaseB)
        {
            // This is used in baseFind() 
            Debug.Assert(pSpriteBaseA != null);
            Debug.Assert(pSpriteBaseB != null);

            Sprite pDataA = (Sprite)pSpriteBaseA;
            Sprite pDataB = (Sprite)pSpriteBaseB;

            bool status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }
        override protected void derivedWash(NodeBase pSpriteBase)
        {
            Debug.Assert(pSpriteBase != null);
            Sprite pSprite = (Sprite)pSpriteBase;
            pSprite.Wash();
        }
        override protected void derivedDumpNode(NodeBase pSpriteBase)
        {
            Debug.Assert(pSpriteBase != null);
            Sprite pData = (Sprite)pSpriteBase;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private static Sprite poSpriteCompare;
        private static SpriteMan psInstance = null;
    }
}

// --- End of File ---
