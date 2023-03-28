//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienOctopus : AlienBase
    {
        public AlienOctopus(SpriteGame.Name spriteName, float posX, float posY)
        : base(GameObject.Name.Octopus, spriteName, posX, posY)
        {
            this.delta = 1.0f;
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an GreenBird
            // Call the appropriate collision reaction            
            other.VisitAlienOctopus(this);
        }

        /*public override void VisitMissileGroup(MissileGroup m)
        {
            // Bird vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Bird
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }*/

        

        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
            ScenePlay.score += 10;

        }

        public override void Update()
        {

            /*this.y += this.delta;

            if (this.y > 500.0f || this.y < 100.0f)
            {
                this.delta *= -1.0f;
            }*/

            base.Update();


        }


        // Data: ---------------
        private float delta;

    }
}

// --- End of File ---
