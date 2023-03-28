/*using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Scene
{
    class SceneMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private ImageMan(int reserveNum = 3, int reserveGrow = 1)
                // LTN - ImageManager reserves
                : base(new SLinkMan(), new SLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            // initialize derived data here
            // LTN - Static - ImageMan
            psImageCompare = new Image();
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
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
                // LTN - ImageMan - Singleton
                psInstance = new ImageMan(reserveNum, reserveGrow);
            }
            ImageMan.Add(Image.Name.HotPink, Texture.Name.HotPink, 0, 0, 128, 128);
            ImageMan.Add(Image.Name.NullObject, Texture.Name.HotPink, 0, 0, 0, 0);
        }
        public static void Destroy()
        {
            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton

            ImageMan.DumpStats();
        }
        public static Image Add(Image.Name name, Texture.Name _TextName, float x, float y, float w, float h)
        {
            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            Texture pTexture = TextureMan.Find(_TextName);
            Debug.Assert(pTexture != null);

            Image pImage = (Image)pMan.baseAdd();
            Debug.Assert(pImage != null);

            // Initialize the data
            pImage.Set(name, pTexture, x, y, w, h);
            return pImage;
        }
        public static Image Find(Image.Name name)
        {
            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Images

            // So:  Use the Compare Image - as a reference
            //      use in the Compare() function
            ImageMan.psImageCompare.name = name;

            Image pData = (Image)pMan.baseFind(ImageMan.psImageCompare);
            return pData;
        }
        public static void Remove(Image pImage)
        {
            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pImage != null);
            pMan.baseRemove(pImage);
        }
        public static void Dump()
        {
            Debug.WriteLine("\n   ------ Image Man: ------");

            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();

        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ Image Man: ------");

            ImageMan pMan = ImageMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }


        //------------------------------------
        // Override Abstract methods
        //------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            // STN - Create and return node
            NodeBase pNodeBase = new SceneContext();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }


        *//*override protected void derivedDumpNode(NodeBase pImageBase)
        {
            Debug.Assert(pImageBase != null);
            Image pData = (Image)pImageBase;
            pData.Dump();
        }*//*

        //------------------------------------
        // Private methods
        //------------------------------------
        private static ImageMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static SceneContext psImageCompare;
        private static ImageMan psInstance = null;
    }

}
}
*/