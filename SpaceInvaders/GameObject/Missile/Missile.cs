//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : MissileCategory
    {
        public Missile(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.Missile, spriteName, posX, posY)
        {
            this.x = posX;
            this.y = posY;
            this.bHit = false;
            this.delta = 12.0f;
            this.alive = true;
        }

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 12.0f;
            this.poColObj.pColSprite.SetColor(0, 0, 0);
            this.pSpriteProxy.Set(SpriteGame.Name.Missile);
            this.alive = true;



            base.Resurrect();
        }

        public override void Update()
        {
            base.Update();
            if (alive)
            {
                this.y += delta;

            }

            
        }

        ~Missile()
        {

        }
        public override void Remove()
        {
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent (missile root)
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            // Now remove it
            base.Remove();
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
            // BombRoot vs Ship
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(b);
            ColPair.Collide(pGameObj, this);
        }


        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Missile
            // Call the appropriate collision reaction            
            other.VisitMissile(this);
        }
        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        public void SetActive(bool state)
        {
            this.alive = state;
        }
        // Data
        public bool bHit;
        public bool alive;
        private bool enable;
        public float delta;
    }
}

// --- End of File ---
