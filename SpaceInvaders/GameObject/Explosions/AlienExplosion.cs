//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienExplosion : Leaf
    {
        public AlienExplosion(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.AlienExplosion, spriteName, posX, posY)
        {
            this.x = posX;
            this.y = posY;

            this.pSpriteBatch = SpriteBatchMan.Find(SpriteBatch.Name.Birds);
            this.ActivateSprite(pSpriteBatch);
            //this.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
            Debug.Assert(this.pSpriteBatch != null);

            // Attach the missile to the missile root
            GameObject pAlienExplosionRoot = GameObjectNodeMan.Find(GameObject.Name.AlienExplosionRoot);
            Debug.Assert(pAlienExplosionRoot != null);
            pAlienExplosionRoot.Add(this);


            Debug.WriteLine("Boom");
        }

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            base.Resurrect();
        }

        public override void Update()
        {
            base.Update();
            //base.BaseUpdateBoundingBox(this);
            //SetPos(x,y);
            //this.y += delta;

            
        }

        ~AlienExplosion()
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



        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Missile
            // Call the appropriate collision reaction            
            //other.VisitMissile(this);
        }
        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }
        SpriteBatch pSpriteBatch;

    }
}

// --- End of File ---
