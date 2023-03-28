//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteNode : DLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            RedBird,
            YellowBird,
            GreenBird,
            WhiteBird,

            Octopus,
            Crab,
            Squid,

            RedGhost,
            PinkGhost,
            BlueGhost,
            OrangeGhost,
            MsPacMan,
            PowerUpGhost,
            Prezel,

            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------

        public SpriteNode()
        : base()
        {
            this.pSpriteBase = null;
            this.pBackSpriteNodeMan = null;
        }

        //------------------------------------
        // Methods
        //------------------------------------
        override public bool Compare(NodeBase pSpriteNodeBaseB)
        {
            // This is used in baseFind() 
            Debug.Assert(pSpriteNodeBaseB != null);

            SpriteNode pDataB = (SpriteNode)pSpriteNodeBaseB;

            bool status = false;

            if (this.pSpriteBase.GetName().GetHashCode() == pDataB.pSpriteBase.GetName().GetHashCode())
            {
                status = true;
            }

            return status;
        }
        override public System.Enum GetName()
        {
            return null;
        }

        public void Set(SpriteBase pNode)
        {
            // associate it
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;
        }

        public void Set(SpriteGame.Name name)
        {
            // Go find it
            this.pSpriteBase = SpriteGameMan.Find(name);
            Debug.Assert(this.pSpriteBase != null);
        }

        public void Set(SpriteBox.Name name)
        {
            // Go find it
            this.pSpriteBase = SpriteBoxMan.Find(name);
            Debug.Assert(this.pSpriteBase != null);
        }

        public void Set(SpriteGameProxy pNode)
        {
            // associate it
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;
        }
        private void privClear()
        {
            this.pSpriteBase = null;
        }
        public void Set(SpriteBase pNode, SpriteNodeMan _pSpriteNodeMan)
        {
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;

            // Set the back pointer
            // Allows easier deletion in the future
            Debug.Assert(pSpriteBase != null);
            this.pSpriteBase.SetSpriteNode(this);

            Debug.Assert(_pSpriteNodeMan != null);
            this.pBackSpriteNodeMan = _pSpriteNodeMan;
        }
        public SpriteBase GetSpriteBase()
        {
            return this.pSpriteBase;
        }
        public SpriteNodeMan GetSBNodeMan()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan;
        }
        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan.GetSpriteBatch();
        }
        //------------------------------------
        // Override
        //------------------------------------

        override public void Wash()
        {
            this.baseClear();
            this.privClear();
        }
        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   ({0}) node", this.GetHashCode());

            // Data:
            Debug.WriteLine("   pSprite: {0} ({1})", this.pSpriteBase.GetName(), this.pSpriteBase.GetHashCode());

            // Let the base print its contribution
            this.baseDump();
        }

        //------------------------------------
        // Data
        //------------------------------------
        // Make it Abstract
        public SpriteBase pSpriteBase;

        // Keenan(delete.C)
        private SpriteNodeMan pBackSpriteNodeMan;
    }
}

// --- End of File ---

