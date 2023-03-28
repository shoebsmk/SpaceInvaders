using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBase : DLink
    {
        public SpriteBase()
        : base()
        {
            this.pBackSpriteNode = null;
        }

        abstract public void Render();
        abstract public void Update();

        public SpriteNode GetSpriteNode()
        {
             Debug.Assert(this.pBackSpriteNode != null);
            return this.pBackSpriteNode;
        }
        public void SetSpriteNode(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            this.pBackSpriteNode = pSpriteBatchNode;
        }

        // Data: -------------------------------------------

        // Keenan(delete.B)
        // If you remove a SpriteBase initiated by gameObject... 
        //     its hard to get the spriteBatchNode
        //     so have a back pointer to it
        private SpriteNode pBackSpriteNode;
    }
}
