//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissileExplosionCommand : Command
    {
        public MissileExplosionCommand(SpriteGame.Name spriteName, GameObject _pExplosionObj)
        {

            // string only for testing
            pExplosionObj = _pExplosionObj;

            /*pAlienExplosion = new AlienExplosion(spriteName, pAlien.x, pAlien.y);
            Debug.Assert(this.pSprite != null);*/


        }

        public override void Execute(float deltaTime)
        {


            GameObject pA = null, pB = null, pC = null;

            pA = (GameObject)this.pExplosionObj;


            // Root - do not delete
            //GameObject pD = (GameObject)IteratorForwardComposite.GetParent(pC);

            // Alien
            if (pA != null && pA.GetNumChildren() == 0)
            {
                pA.Remove();
            }


        }

        private GameObject pExplosionObj;
    }
}

// --- End of File ---
