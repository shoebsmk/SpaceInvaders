//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipMan
    {
        public enum MissileState
        {
            Ready,
            Flying,
            Dead
        }

        public enum MoveState
        {
            MoveRight,
            MoveLeft,
            MoveBoth,
            Dead
        }

        private ShipMan()
        {
            // Store the states
            this.pStateMissileReady = new ShipMissileReady();
            this.pStateMissileFlying = new ShipMissileFlying();
            this.pStateMissileDead = new ShipMissileDead();
           
            this.pStateMoveBoth = new ShipMoveBoth();
            this.pStateMoveRight = new ShipMoveRight();
            this.pStateMoveLeft = new ShipMoveLeft();
            this.pStateMoveDead = new ShipMoveDead();
            lives = 4;
            // set active
            this.pShip = null;
            this.pMissile = null;
           
        }

        public static void Create(IrrKlang.ISoundEngine _pSndEngine, IrrKlang.ISoundSource _pSndSrc)
        {
            // make sure its the first time
            Debug.Assert(instance == null);

            pSndEngine = _pSndEngine;
            pSndSrc = _pSndSrc;
            //IrrKlang.ISoundSource pSndShoot = sndEngine.AddSoundSourceFromFile("Shoot.wav");

            
            // Do the initialization
            if (instance == null)
            {
                instance = new ShipMan();
            }

            Debug.Assert(instance != null);

            // Stuff to initialize after the instance was created
            instance.pShip = ActivateShip();
            instance.pShip.SetState(ShipMan.MoveState.MoveBoth);
            instance.pShip.SetState(ShipMan.MissileState.Ready);

        }

        public static void ResetShipLives()
        {
            lives = 4;
        }

        public static void RespawnShip()
        {
            // make sure its the first time
            //Debug.Assert(instance == null);

            // Do the initialization
            
            lives--;

            if(lives <= 0)
            {
                ScenePlay.pSceneContext.SetState(SceneContext.Scene.Over);
                return;
            }

            if(instance.pShip.MoveState != instance.pStateMoveDead )
            {
                return;
            }

            if (instance == null)
            {
                instance = new ShipMan();
            }

            Debug.Assert(instance != null);

            // Stuff to initialize after the instance was created
            instance.pShip = ActivateShip();
            instance.pShip.SetState(ShipMan.MoveState.MoveBoth);
            instance.pShip.SetState(ShipMan.MissileState.Ready);

        }

        private static ShipMan privInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Ship GetShip()
        {
            ShipMan pShipMan = ShipMan.privInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pShip != null);

            return pShipMan.pShip;
        }
        

        public static ShipMissileState GetState(MissileState state)
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            ShipMissileState pShipState = null;

            switch (state)
            {
                case ShipMan.MissileState.Ready:
                    pShipState = pShipMan.pStateMissileReady;
                    break;

                case ShipMan.MissileState.Flying:
                    pShipState = pShipMan.pStateMissileFlying;
                    break;
                case ShipMan.MissileState.Dead:
                    pShipState = pShipMan.pStateMissileDead;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }
        public static ShipMoveState GetState(MoveState state)
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            ShipMoveState pShipState = null;

            switch (state)
            {
                case ShipMan.MoveState.MoveBoth:
                    pShipState = pShipMan.pStateMoveBoth;
                    break;

                case ShipMan.MoveState.MoveLeft:
                    pShipState = pShipMan.pStateMoveLeft;
                    break;

                case ShipMan.MoveState.MoveRight:
                    pShipState = pShipMan.pStateMoveRight;
                    break;
                case ShipMan.MoveState.Dead:
                    pShipState = pShipMan.pStateMoveDead;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }

        public static Missile GetMissile()
        {
            ShipMan pShipMan = ShipMan.privInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static Missile ActivateMissile()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            pSndEngine.Play2D(pSndSrc, false, false, false);

            // No need to re-calling new()
            Missile pMissile = null;
            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.Missile);
            if (pGameObjNode == null)
            {
                pMissile = new Missile(SpriteGame.Name.Missile, 400, 100);
            }
            else
            { 
                // Recycle it.
                pMissile = (Missile)pGameObjNode.pGameObj;
                GhostMan.Remove(pGameObjNode);
               // GhostMan.Dump();
                pMissile.Resurrect(400,100);
            }

            pShipMan.pMissile = pMissile;

            // Attached to SpriteBatches
            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Birds);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            pMissile.ActivateCollisionSprite(pSB_Boxes);
            pMissile.ActivateSprite(pSB_Aliens);

            // Attach the missile to the missile root
            GameObject pMissileGroup = GameObjectNodeMan.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            // Add to GameObject Tree - {update and collisions}
            pMissileGroup.Add(pShipMan.pMissile);

            return pShipMan.pMissile;
        }


        private static Ship ActivateShip()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            // LTN - owned by ShipMan.. but needs some cleanup
            Ship pShip = new Ship(GameObject.Name.Ship, SpriteGame.Name.Ship, 336, 100);
            //pShip.ActivateSprite(SpriteBatchMan.Find(SpriteBatch.Name.Birds));
            pShip.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));

            pShipMan.pShip = pShip;

            // Attach the sprite to the correct sprite batch
            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Birds);
            pSB_Aliens.Attach(pShip);

            // Attach the missile to the missile root
            GameObject pShipRoot = GameObjectNodeMan.Find(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pShipRoot.Add(pShipMan.pShip);

            return pShipMan.pShip;
        }

        // Data: ----------------------------------------------
        private static ShipMan instance = null;
        public static int lives = 4;

        static IrrKlang.ISoundEngine pSndEngine;
        static IrrKlang.ISoundSource pSndSrc;
        // Active
        private Ship pShip;
        private Missile pMissile;

        // Reference
        private ShipMissileReady pStateMissileReady;
        private ShipMissileFlying pStateMissileFlying;
        private ShipMissileDead pStateMissileDead;

        private ShipMoveBoth pStateMoveBoth;
        private ShipMoveRight pStateMoveRight;
        private ShipMoveLeft pStateMoveLeft;
        private ShipMoveDead pStateMoveDead;

        


    }
}

// --- End of File ---
