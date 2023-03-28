//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallTop : WallCategory
    {
        public WallTop(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, WallCategory.Type.Top)
        {
            this.poColObj.poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetColor(1, 1, 0);
        }

        ~WallTop()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitWallTop(this);
        }
        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs WallTop
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs WallTop
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }
        public override void VisitBomb(Bomb b)
        {
            //Debug.WriteLine(" ---> Done");
            /*ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();*/
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs WallRoot
            /*GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(b);
            ColPair.Collide(pGameObj, this);*/
        }

        // Data: ---------------


    }
}

// --- End of File ---
