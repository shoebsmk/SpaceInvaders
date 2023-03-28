//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BirdGrid : Composite
    {
        public BirdGrid()
            : base()
        {
            this.name = Name.BirdGrid;
            this.poColObj.pColSprite.SetColor(0, 0, 1);
            this.delta = 1.0f;
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitGroup(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdGroup vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public void MoveGrid()
        {

            IteratorForwardComposite pFor = new IteratorForwardComposite(this);

            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.x += this.delta;

                pNode = pFor.Next();
            }
        }

        public void SetDelta(float inDelta)
        {
            this.delta = inDelta;
        }

        // Data: ---------------
        private float delta;
    }
}

// --- End of File ---
