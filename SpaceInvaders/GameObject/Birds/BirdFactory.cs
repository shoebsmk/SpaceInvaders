//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BirdFactory
    {
        public BirdFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxName)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);
            
            this.pBoxBatch = SpriteBatchMan.Find(boxName);
            Debug.Assert(this.pBoxBatch != null);
        }


        public GameObject Create(GameObject.Name name, BirdCategory.Type type, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case BirdCategory.Type.Green:
                    pGameObj = new BirdGreen(SpriteGame.Name.GreenBird, posX, posY);
                    break;

                case BirdCategory.Type.Red:
                    pGameObj = new BirdRed(SpriteGame.Name.RedBird, posX, posY);
                    break;

                case BirdCategory.Type.White:
                    pGameObj = new BirdWhite(SpriteGame.Name.WhiteBird, posX, posY);
                    break;

                case BirdCategory.Type.Yellow:
                    pGameObj = new BirdYellow(SpriteGame.Name.YellowBird, posX, posY);
                    break;

                case BirdCategory.Type.Grid:
                    pGameObj = new BirdGrid();
                    break;

                case BirdCategory.Type.Column:
                    pGameObj = new BirdColumn();
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add it to the gameObjectManager
            /*Debug.Assert(pGameObj != null);
            GameObjectNodeMan.Attach(pGameObj);*/

            // Attached to Group
            pGameObj.ActivateSprite(this.pSpriteBatch);
            pGameObj.ActivateCollisionSprite(this.pBoxBatch);
            return pGameObj;
        }

        // Data: ---------------------

        SpriteBatch pSpriteBatch;
        SpriteBatch pBoxBatch;
    }
}

// --- End of File ---
