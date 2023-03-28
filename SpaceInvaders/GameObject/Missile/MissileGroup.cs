//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MissileGroup : Composite
    {
        public MissileGroup()
            : base()
        {
            this.name = Name.MissileGroup;

            //this.poColObj.pColSprite.SetColor(0, 0, 1);
        }

        ~MissileGroup()
        {

        }
        public override void VisitGroup(AlienGrid a)
        {
            // BirdGroup vs WallRight
            Debug.WriteLine("\ncollide: {0} with {1}", this, a);
            Debug.WriteLine("               --->DONE<----");

            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }
        public override void VisitBomb(Bomb b)
        {
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(b, pGameObj);
        }


        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs ShipRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(b);
            ColPair.Collide(pGameObj, this);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an MissileGroup
            // Call the appropriate collision reaction            
            other.VisitMissileGroup(this);
        }


        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }



        // Data: ---------------


    }
}

// --- End of File ---

