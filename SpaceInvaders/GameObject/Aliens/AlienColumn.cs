//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienColumn : Composite
    {

        public AlienColumn(GameObject.Name name, SpriteGame.Name spriteName)
            : base(name, spriteName)
        {
            this.name = Name.AlienColumn;

            this.poColObj.pColSprite.SetColor(1, 0, 0);
        }

        public override void Update()
        {
            // STN - only for iteration
            base.BaseUpdateBoundingBox(this);

            //Debug.WriteLine("x:{0} y:{1} w:{2} h:{3}", pColTotal.x, pColTotal.y, pColTotal.width, pColTotal.height);

            base.Update();


        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdColumn
            // Call the appropriate collision reaction            
            other.VisitColumn(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldGrid
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        /*public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdColumn vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }*/



        /*public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }*/

        public override void Print()
        {
            Debug.WriteLine("");
            Debug.WriteLine("Column:");

            // walk through the list and render
            Iterator pIt = this.poDLinkMan.GetIterator();
            Debug.Assert(pIt != null);

           for(pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                GameObject pNode = (GameObject)pIt.Current();
                Debug.Assert(pNode != null);

                pNode.Dump();
            }
        }

        // LTN - SpriteGameProxyNull
        private static SpriteGameProxyNull psSpriteGameProxyNull = new SpriteGameProxyNull();

    }
}

// --- End of File ---
