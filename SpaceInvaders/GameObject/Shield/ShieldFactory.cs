//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldFactory
    {
        private ShieldFactory()
        {
            this.pSpriteBatch = null;
            this.pCollisionSpriteBatch = null;
            this.pTree = null;
        }
        private void privSet(SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch, Composite pTree)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = SpriteBatchMan.Find(collisionSpriteBatch);
            Debug.Assert(this.pCollisionSpriteBatch != null);

            Debug.Assert(pTree != null);
            this.pTree = pTree;
        }
        private void privSetParent(GameObject pParentNode)
        {
            // OK being null
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }
        ~ShieldFactory()
        {
        }
        private GameObject privCreate(ShieldCategory.Type type, GameObject.Name gameName, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pShield = null;

            GameObjectNode pGameObjNode = GhostMan.Find(gameName);
            if (pGameObjNode != null)
            {
                pShield = pGameObjNode.pGameObj;
                GhostMan.Remove(pGameObjNode);

                //GhostMan.Dump();

                switch (type)
                {
                    case ShieldCategory.Type.Brick:
                    case ShieldCategory.Type.LeftTop1:
                    case ShieldCategory.Type.LeftTop0:
                    case ShieldCategory.Type.LeftBottom:
                    case ShieldCategory.Type.RightTop1:
                    case ShieldCategory.Type.RightTop0:
                    case ShieldCategory.Type.RightBottom:
                        ((ShieldBrick)pShield).Resurrect(posX,posY);
                        break;

                    case ShieldCategory.Type.Root:
                        Debug.Assert(false);
                        break;

                    case ShieldCategory.Type.Grid:
                        ((ShieldGrid)pShield).Resurrect(posX, posY);
                        break;

                    case ShieldCategory.Type.Column:
                        ((ShieldColumn)pShield).Resurrect(posX, posY); ;
                        break;

                    default:
                        // something is wrong
                        Debug.Assert(false);
                        break;
                }
            }
            else
            {
            switch (type)
            {
                case ShieldCategory.Type.Brick:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick, posX, posY);
                    break;

                case ShieldCategory.Type.LeftTop1:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftTop1, posX, posY);
                    break;

                case ShieldCategory.Type.LeftTop0:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftTop0, posX, posY);
                    break;

                case ShieldCategory.Type.LeftBottom:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftBottom, posX, posY);
                    break;

                case ShieldCategory.Type.RightTop1:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightTop1, posX, posY);
                    break;

                case ShieldCategory.Type.RightTop0:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightTop0, posX, posY);
                    break;

                case ShieldCategory.Type.RightBottom:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightBottom, posX, posY);
                    break;

                case ShieldCategory.Type.Root:
                    Debug.Assert(false);
                    break;

                case ShieldCategory.Type.Grid:
                    pShield = new ShieldGrid(gameName, SpriteGame.Name.NullObject, posX, posY);
                    break;

                case ShieldCategory.Type.Column:
                    pShield = new ShieldColumn(gameName, SpriteGame.Name.NullObject, posX, posY);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }
            }

            // add to the tree
            this.pTree.Add(pShield);

            // Attached to Group
            pShield.ActivateSprite(this.pSpriteBatch);
            pShield.ActivateCollisionSprite(this.pCollisionSpriteBatch);

            return pShield;
        }

        public static GameObject CreateSingleShield(float x, float y)
        {
            ShieldFactory pFactory = ShieldFactory.privInstance();

            ShieldRoot pShieldRoot = (ShieldRoot)GameObjectNodeMan.Find(GameObject.Name.ShieldRoot);
           
            if(pShieldRoot == null)
            {
                pShieldRoot = new ShieldRoot(GameObject.Name.ShieldRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            }

            GameObjectNodeMan.Attach(pShieldRoot);

            pFactory.privSet(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pShieldRoot);

            // create a grid
            GameObject pGrid = pFactory.privCreate(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);
        

            int j = 0;

            GameObject pColumn;

         
            pFactory.privSetParent(pGrid);
            pColumn = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

            pFactory.privSetParent(pColumn);

            float start_x = x;
            float start_y = y;
            float off_x = 0;
            float brickWidth = 20.0f;
            float brickHeight = 10.0f;

            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 2 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 3 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 4 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, start_x, start_y + 5 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, start_x, start_y + 6 * brickHeight);

            /*pFactory.privSetParent(pGrid);
            pColumn = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

            pFactory.privSetParent(pColumn);

            off_x += brickWidth;
            *//*pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + brickHeight);*//*
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);*/

            pFactory.privSetParent(pGrid);
            pColumn = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

            pFactory.privSetParent(pColumn);

            off_x += brickWidth;
            pFactory.privCreate(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);

            pFactory.privSetParent(pGrid);
            pColumn = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

            pFactory.privSetParent(pColumn);

            off_x += brickWidth;
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);

            pFactory.privSetParent(pGrid);
            pColumn = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

            pFactory.privSetParent(pColumn);

            off_x += brickWidth;
            pFactory.privCreate(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);

            /*pFactory.privSetParent(pGrid);
            pColumn = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

            pFactory.privSetParent(pColumn);

            off_x += brickWidth;
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);*/

            pFactory.privSetParent(pGrid);
            pColumn = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

            pFactory.privSetParent(pColumn);

            off_x += brickWidth;
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
            pFactory.privCreate(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);

            return pShieldRoot;
        }

        private static ShieldFactory privInstance()
        {
            if(pInstance == null)
            {
                ShieldFactory.pInstance = new ShieldFactory();
            }

            Debug.Assert(pInstance != null);

            return pInstance;
        }

        // Data: ---------------------
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pCollisionSpriteBatch;
        private Composite pTree;

        private static ShieldFactory pInstance = null;
    }
}

// --- End of File ---
