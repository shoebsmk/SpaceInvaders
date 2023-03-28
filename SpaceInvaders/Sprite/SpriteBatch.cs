//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatch : DLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            PacMan,
            Birds,
            Misc,
            Flying,
            Boxes,
            Shields,
            Texts,

            Aliens,

            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------
        public SpriteBatch()
            : base()
        {
            this.name = SpriteBatch.Name.Uninitialized;

            // LTN - Owner - SpriteNodeMan
            this.poSpriteNodeMan = new SpriteNodeMan();
            Debug.Assert(this.poSpriteNodeMan != null);
            this.enableDraw = true;
        }

        //------------------------------------
        // Methods
        //------------------------------------
        public void Set(SpriteBatch.Name name, int reserveNum = 3, int reserveGrow = 1)
        {
            this.name = name;
            this.poSpriteNodeMan.Set(name, reserveNum, reserveGrow);
        }

        public void SetName(SpriteBatch.Name inName)
        {
            this.name = inName;
        }

        public SpriteNodeMan GetSpriteNodeMan()
        {
            return this.poSpriteNodeMan;
        }
        // Base
        /*public SpriteNode Attach(SpriteGameProxy pSpriteGameNode)
        {
            Debug.Assert(pSpriteGameNode != null);
            SpriteNode pNode = this.poSpriteNodeMan.Attach(pSpriteGameNode);
            return pNode;
        }*/
        public SpriteNode Attach(GameObject pGameObj)
        {
            Debug.Assert(pGameObj != null);
            SpriteNode pNode = this.poSpriteNodeMan.Attach(pGameObj.pSpriteProxy);

            // Initialize SpriteBatchNode
            pNode.Set(pGameObj.pSpriteProxy, this.poSpriteNodeMan);

            // Back pointer
            this.poSpriteNodeMan.SetSpriteBatch(this);

            return pNode;
        }
        public SpriteNode Attach(SpriteBase _pNode)
        {
            SpriteNode pNode = this.poSpriteNodeMan.Attach(_pNode);

            // Initialize SpriteBatchNode
            pNode.Set(_pNode, this.poSpriteNodeMan);

            // Back pointer
            this.poSpriteNodeMan.SetSpriteBatch(this);

            return pNode;
        }

        /*public SpriteNode Attach(SpriteBox pSpriteBox)
        {
            Debug.Assert(pSpriteBox != null);
            SpriteNode pNode = this.poSpriteNodeMan.Attach(pSpriteBox);
            return pNode;
        }*/

        private void privClear()
        {

        }



        //------------------------------------
        // Override
        //------------------------------------
        public override System.Enum GetName()
        {
            return this.name;
        }
        override public void Wash()
        {
            this.baseClear();
            this.privClear();
        }
        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());

            // Let the base print its contribution
            this.baseDump();
        }

        //------------------------------------
        // Data
        //------------------------------------
        public SpriteBatch.Name name;
        private readonly SpriteNodeMan poSpriteNodeMan;
        public uint priority;
        public bool enableDraw;

        
    }
}

// --- End of File ---
