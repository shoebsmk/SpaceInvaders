//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienExplosionRoot : Composite
    {
        public AlienExplosionRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetColor(0, 0, 1);
        }

        ~AlienExplosionRoot()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            //other.VisitShipRoot(this);
        }


        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }


        // Data: ---------------


    }
}

// --- End of File ---

