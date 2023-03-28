//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombExplosionCommand : Command
    {
        public BombExplosionCommand(Bomb _pExplosionObj)
        {

            // string only for testing
            Debug.Assert(_pExplosionObj != null);
            pExplosionObj = _pExplosionObj;

            /*pAlienExplosion = new AlienExplosion(spriteName, pAlien.x, pAlien.y);
            Debug.Assert(this.pSprite != null);*/


        }

        public override void Execute(float deltaTime)
        {


            GameObject pA = null, pB = null, pC = null;

            pA = (GameObject)this.pExplosionObj;

            if(pA != null)
            {
                pB = (GameObject)IteratorForwardComposite.GetParent(pA);

                if(pB != null)
                {
                    pC = (GameObject)IteratorForwardComposite.GetParent(pB);

                }


            }
            // Root - do not delete
            //GameObject pD = (GameObject)IteratorForwardComposite.GetParent(pC);

            // Alien
            if (pA != null && pA.GetNumChildren() == 0)
            {
                pA.Remove();
            }

            
        }

        private Bomb pExplosionObj;
    }
}

// --- End of File ---
