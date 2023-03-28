//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BirdWhite : BirdCategory
    {
        public BirdWhite(SpriteGame.Name spriteName, float posX, float posY)
        : base(GameObject.Name.WhiteBird, spriteName, posX, posY)
        {

        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an WhiteBird
            // Call the appropriate collision reaction            
            other.VisitWhiteBird(this);
        }

        
        /*public override void Update()
        {
            base.Update();
        }*/

        public override void Update()
        {
            /*this.y -= this.delta;
            this.x += this.delta;

            if (this.y > 500.0f || this.y < 100.0f)
            {
                this.delta *= -1.0f;
            }*/

            base.Update();


        }


    }
}

// --- End of File ---
