//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombSpawnEvent : Command
    {
        public BombSpawnEvent(AlienGrid _pAlienGrid)
        {
            if(_pAlienGrid == null)
            {
                return;
            }
            this.pBombRoot = GameObjectNodeMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(this.pBombRoot != null);

            this.pSB_Birds = SpriteBatchMan.Find(SpriteBatch.Name.Birds);
            Debug.Assert(this.pSB_Birds != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(this.pSB_Boxes != null);

            Debug.Assert(_pAlienGrid != null);
            this.pAlienGrid = _pAlienGrid;
        }

        override public void Execute(float deltaTime)
        {
            if (pAlienGrid == null)
            {
                return;
            }

            GameObject bottomAlien = pAlienGrid.GetBottomAlien();

            if(bottomAlien == null)
            {
                return;
            }
            /*Debug.WriteLine("EVENT:"); 
            bottomAlien.Dump();
            Debug.WriteLine("EVENT:");*/
            // Create Bomb
            Bomb pBomb = null;

            float value = new Random().Next(0,3);
            if(value == 0)
            {
                pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombZigZag, new FallZigZag(), bottomAlien.x, bottomAlien.y - 15);
                
            } else if(value == 1)
            {
                pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombDagger, new FallDagger(), bottomAlien.x, bottomAlien.y - 15);
                
            }
            else if (value == 2)
            {
                pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombStraight, new FallStraight(), bottomAlien.x, bottomAlien.y - 15);
                
            }
            else
            {
                Debug.Assert(false);
            }


            

            pBomb.ActivateCollisionSprite(this.pSB_Boxes);
            pBomb.ActivateSprite(this.pSB_Birds);

            // Attach the missile to the Bomb root
            GameObject pBombRoot = GameObjectNodeMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(pBombRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pBombRoot.Add(pBomb);

            TimerEventMan.Add(TimerEvent.Name.BombSpawn, new BombSpawnEvent((AlienGrid)pAlienGrid), 1.0f);


        }

        GameObject pBombRoot;
        SpriteBatch pSB_Birds;
        SpriteBatch pSB_Boxes;
        AlienGrid pAlienGrid;
    }
}

// --- End of File ---
