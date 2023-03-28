//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        public AlienFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxName, Composite pTree, IrrKlang.ISoundEngine soundEngine)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pBoxBatch = SpriteBatchMan.Find(boxName);
            Debug.Assert(this.pBoxBatch != null);

            Debug.Assert(pTree != null);
            this.pTree = pTree;

            Debug.Assert(soundEngine != null);
            pSoundEngine = soundEngine;
        }

        public void SetParent(GameObject pParentNode)
        {
            // OK being null
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }


                    // IrrKlang.ISoundSource a, IrrKlang.ISoundSource b, IrrKlang.ISoundSource c, IrrKlang.ISoundSource d
        public GameObject Create(GameObject.Name name, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObj = null;

            switch (name)
            {
                case GameObject.Name.Octopus:
                    // STN - Create and return , IrrKlang.ISoundSource a, IrrKlang.ISoundSource b, IrrKlang.ISoundSource c, IrrKlang.ISoundSource d
                    pGameObj = new AlienOctopus(SpriteGame.Name.Octopus, posX, posY);
                    break;

                case GameObject.Name.Crab:
                    // STN - Create and return
                    pGameObj = new AlienCrab(SpriteGame.Name.Crab, posX, posY);
                    break;

                case GameObject.Name.Squid:
                    // STN - Create and return
                    pGameObj = new AlienSquid(SpriteGame.Name.Squid, posX, posY);
                    break;

                case GameObject.Name.AlienGrid:
                    // STN - Create and return
                    pGameObj = new AlienGrid(pSoundEngine);
                    break;

                case GameObject.Name.AlienColumn:
                    // STN - Create and return
                    pGameObj = new AlienColumn(name, SpriteGame.Name.NullObject);
                    pGameObj.SetCollisionColor(1.0f, 0.0f, 0.0f);
                    break;
                
                case GameObject.Name.AlienRoot:
                    // STN - Create and return
                    pGameObj = new AlienRoot(name, SpriteGame.Name.NullObject, posX, posY);
                    pGameObj.SetCollisionColor(0.0f, 0.0f, 1.0f);
                    break;
                

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add it to the gameObjectManager
            /*Debug.Assert(pGameObj != null);
            GameObjectNodeMan.Attach(pGameObj);*/

            // add to the tree
            this.pTree.Add(pGameObj);


            // Attached to Group
            pGameObj.ActivateSprite(this.pSpriteBatch);
            pGameObj.ActivateCollisionSprite(this.pBoxBatch);
            return pGameObj;
        }

        // Data: ---------------------

        SpriteBatch pSpriteBatch;
        readonly SpriteBatch pBoxBatch;
        private Composite pTree;
        private IrrKlang.ISoundEngine pSoundEngine;
    }
}

// --- End of File ---
