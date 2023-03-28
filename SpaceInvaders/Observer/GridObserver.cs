//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GridObserver : ColObserver
    {
        public GridObserver()
        {

        }
        override public void Notify()
        {
            //Debug.WriteLine("Grid_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            // OK do some magic
            if(this.pSubject.pObjA.name == AlienGrid.Name.AlienGrid)
            {
                AlienGrid pGrid = (AlienGrid)this.pSubject.pObjA;
                WallCategory pWall = (WallCategory)this.pSubject.pObjB;
                if (pWall.GetCategoryType() == WallCategory.Type.Right)
                {
                    pGrid.SetDelta(-4.0f);
                    pGrid.SetMoveDown();
                }
                else if (pWall.GetCategoryType() == WallCategory.Type.Left)
                {
                    pGrid.SetDelta(4.0f);

                    pGrid.SetMoveDown();

                }
                else if (pWall.GetCategoryType() == WallCategory.Type.Bottom)
                {
                    Debug.WriteLine("Wall Bottom grid collision");
                    pGrid.SetMoveDown();
                }
                else
                {
                    Debug.Assert(false);
                }
            }
            else
            {
                BirdGrid pGrid = (BirdGrid)this.pSubject.pObjA;
                WallCategory pWall = (WallCategory)this.pSubject.pObjB;
                if (pWall.GetCategoryType() == WallCategory.Type.Right)
                {
                    pGrid.SetDelta(-4.0f);
                }
                else if (pWall.GetCategoryType() == WallCategory.Type.Left)
                {
                    pGrid.SetDelta(4.0f);
                }
                else
                {
                    Debug.Assert(false);
                }
            }
            

            

        }

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.GridObserver;
        }


    }
}

// --- End of File ---
