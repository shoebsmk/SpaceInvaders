//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienCrab : AlienBase
    {
        public AlienCrab(SpriteGame.Name spriteName, float posX, float posY)
        : base(GameObject.Name.Crab, spriteName, posX, posY)
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an GreenBird
            // Call the appropriate collision reaction            
            other.VisitAlienCrab(this);
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
            ScenePlay.score += 20;
        }

        /*public override void VisitMissile(Missile m)
        {
            // Bird vs Missile
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Bird
            Debug.WriteLine("-------> Done  <--------");

            m.Hit();
        }*/

        public override void Update()
        {
            /*this.y += 1.0f;
            if (this.y > 600.0f)
            {
                this.y = 0.0f;
            }
*/
            base.Update();
        }


        // Data: ---------------

    }
}

// --- End of File ---
