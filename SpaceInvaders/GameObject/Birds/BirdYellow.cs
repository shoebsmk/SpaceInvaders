//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BirdYellow : BirdCategory
    {
        public BirdYellow(SpriteGame.Name spriteName, float posX, float posY)
        : base(GameObject.Name.YellowBird, spriteName, posX, posY)
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an YellowBird
            // Call the appropriate collision reaction            
            other.VisitYellowBird(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // Bird vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Bird
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }
        

        /*public override void Update()
        {
            //  Debug.WriteLine("update: {0}", this);
            base.Update();
        }*/

        public override void Update()
        {
            /*this.y -= this.delta;
            this.x -= this.delta;

            if (this.y > 500.0f || this.y < 100.0f)
            {
                this.delta *= -1.0f;
            }*/

            base.Update();


        }

        // Data: ---------------
    }
}

// --- End of File ---
