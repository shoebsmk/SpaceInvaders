//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallBottom : WallCategory
    {
        public WallBottom(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, WallCategory.Type.Bottom)
        {
            this.poColObj.poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetColor(1, 1, 0);
        }

        ~WallBottom()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitWallBottom(this);
        }

        public override void VisitBomb(Bomb b)
        {
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs WallRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(b);
            ColPair.Collide(pGameObj, this);
        }


        public override void VisitGroup(AlienGrid a)
        {
            // BirdGroup vs WallRight
            /*Debug.WriteLine("\ncollide: {0} with {1}", this, a);*/
            Debug.WriteLine("               --->DONE<----");

            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }


        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        // Data: ---------------

    }
}

// --- End of File ---
