//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FontMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public FontMan(int reserveNum = 0, int reserveGrow = 1)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            // initialize derived data here
            FontMan.psActiveFontMan = null;
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
                psInstance = new FontMan();
            }
        }
        public static void Destroy()
        {
        }

        public static Font Add(Font.Name name, SpriteBatch.Name SB_Name, string pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            FontMan pMan = FontMan.psActiveFontMan;

            Font pNode = (Font)pMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, pMessage.ToUpper(), glyphName, xStart, yStart);

            // Add to sprite batch
            SpriteBatch pSB = SpriteBatchMan.Find(SB_Name);
            Debug.Assert(pSB != null);
            Debug.Assert(pNode.poSpriteFont != null);
            pSB.Attach(pNode.poSpriteFont);

            return pNode;
        }
        public static void SetActive(FontMan pFontMan)
        {
            FontMan pMan = FontMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pFontMan != null);
            FontMan.psActiveFontMan = pFontMan;
        }

        public static void AddXml(Glyph.Name glyphName, string assetName, Texture.Name textName)
        {
            GlyphMan.AddXml(assetName, glyphName, textName);
        }

        public static Font Find(Font.Name name)
        {
            FontMan pMan = FontMan.psActiveFontMan;

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            FontMan.psNodeCompare.name = name;

            Font pData = (Font)pMan.baseFind(FontMan.psNodeCompare);
            return pData;
        }

        public static void Remove(Font pNode)
        {
            Debug.Assert(pNode != null);

            FontMan pMan = FontMan.psActiveFontMan;

            // Remove it from the manager
            SpriteNode pSpriteNode = pNode.poSpriteFont.GetSpriteNode();
            Debug.Assert(pSpriteNode != null);
            SpriteBatchMan.Remove(pSpriteNode);

            pMan.baseRemove(pNode);
        }

        public static void Dump()
        {
            FontMan pMan = FontMan.psActiveFontMan;
            pMan.baseDump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static FontMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new Font();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private static FontMan psActiveFontMan = null;
        private static FontMan psInstance = null;
        private static Font psNodeCompare = new Font();

    }
}

// --- End of File ---
