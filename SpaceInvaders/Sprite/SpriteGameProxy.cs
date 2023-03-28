//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteGameProxy : SpriteBase
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            Proxy,
            NullObject,

            Compare,
            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------

        // Create a single sprite and all dynamic objects ONCE and ONLY ONCE (OOO- tm)
        public SpriteGameProxy()
        : base()   // <--- Delegate (kick the can)
        {
            this.privClear();
        }

        protected SpriteGameProxy(SpriteGameProxy.Name _name)
        : base()
        {
            this.name = _name;
            this.privClear();
        }

        //------------------------------------
        // Methods
        //------------------------------------

        public void Set(SpriteGame.Name _name)
        {
            this.name = SpriteGameProxy.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;

            this.pSprite = SpriteGameMan.Find(_name);
            Debug.Assert(this.pSprite != null);
        }
        

        private void privPushToReal()
        {
            // push the data from proxy to Real GameSprite
            Debug.Assert(this.pSprite != null);

            this.pSprite.x = this.x;
            this.pSprite.y = this.y;

            this.pSprite.sx = this.sx;
            this.pSprite.sy = this.sy;
        }
        private void privClear()
        {
            this.name = SpriteGameProxy.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;

            this.sx = 1.0f;
            this.sy = 1.0f;

            this.pSprite = null;
        }

        public override bool Compare(NodeBase pNodeBaseB)
        {
            // This is used in baseFind() 
            Debug.Assert(pNodeBaseB != null);
            SpriteGameProxy pNodeB = (SpriteGameProxy)pNodeBaseB;

            bool status = false;

            Debug.Assert(this.pSprite != null);
            Debug.Assert(pNodeB.pSprite != null);

            // Why doesn't GetName() work without GetHashCode?
            // Debug.WriteLine("cmp {0} {1} \n", this.GetName().GetHashCode(), pNodeBaseB.GetName().GetHashCode());
            if (this.pSprite.GetName().GetHashCode() == pNodeB.pSprite.GetName().GetHashCode())
            {
                status = true;
            }

            return status;
        }

        //------------------------------------
        // Override
        //------------------------------------

        public override void Render()
        {
            // move the values over to Real GameSprite
            this.privPushToReal();

            // update and draw real sprite 
            // Seems redundant - Real Sprite might be stale
            this.pSprite.Update();
            this.pSprite.Render();
        }
        
        public override void Update()
        {
            // push the data from proxy to Real GameSprite
            this.privPushToReal();
            this.pSprite.Update();
        }
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
            if (pSprite != null)
            {
                Debug.WriteLine("       Sprite:{0} ({1})", this.pSprite.GetName(), this.pSprite.GetHashCode());
            }
            else
            {
                Debug.WriteLine("       Sprite: null");
            }
            Debug.WriteLine("        (x,y): {0},{1}", this.x, this.y);

            // Let the base print its contribution
            this.baseDump();
        }

        //------------------------------------
        // Data
        //------------------------------------
        public Name name;
        public float x;
        public float y;
        public float sx;
        public float sy;
        public SpriteGame pSprite;
    }
}

// --- End of File ---

