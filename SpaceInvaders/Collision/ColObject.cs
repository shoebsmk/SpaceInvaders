//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColObject
    {
        public ColObject(SpriteGameProxy pSpriteGameProxy)
        {
            Debug.Assert(pSpriteGameProxy != null);

            // Create Collision Rect
            // Use the reference sprite to set size and shape
            // need to refactor if you want it different
            SpriteGame pSprite = pSpriteGameProxy.pSprite;
            Debug.Assert(pSprite != null);

            // Origin is in the UPPER RIGHT
            // LTN - ColObject
            this.poColRect = new ColRect(pSprite.GetRect());
            Debug.Assert(this.poColRect != null);

            // Create the sprite
            this.pColSprite = SpriteBoxMan.Add(SpriteBox.Name.Box, this.poColRect.x, this.poColRect.y, this.poColRect.width, this.poColRect.height);
            Debug.Assert(this.pColSprite != null);
            this.pColSprite.SetColor(1.0f, 1.0f, 0.0f);
        }

        public void UpdatePos(float x, float y)
        {
            // Note we are not considering angle or scale at this time

            this.poColRect.x = x;
            this.poColRect.y = y;

            this.pColSprite.x = this.poColRect.x;
            this.pColSprite.y = this.poColRect.y;
            this.pColSprite.SetRect(this.poColRect.x, this.poColRect.y, this.poColRect.width, this.poColRect.height);
            this.pColSprite.Update();
        }


        public void Resurrect(SpriteGameProxy pSpriteProxy)
        {
            Debug.Assert(pSpriteProxy != null);

            // Create Collision Rect
            // Use the reference sprite to set size and shape
            // need to refactor if you want it different
            SpriteGame pSprite = pSpriteProxy.pSprite;
            Debug.Assert(pSprite != null);

            Debug.Assert(this.poColRect != null);
            this.poColRect.Set(pSprite.GetRect());

            Debug.Assert(this.pColSprite != null);
            this.pColSprite.Set(SpriteBox.Name.Box, this.poColRect.x, this.poColRect.y, this.poColRect.width, this.poColRect.height);
            this.pColSprite.SetColor(1.0f, 1.0f, 1.0f);
        }


        // -------------------------------------------------------
        // Data:
        // -------------------------------------------------------
        public SpriteBox pColSprite;  // should be SpriteBoxProxy
        public ColRect poColRect;
    }
}

// --- End of File
