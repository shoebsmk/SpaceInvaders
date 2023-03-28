//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BombCategory : Leaf
    {
        public enum Type
        {
            Bomb,
            BombRoot,
            Unitialized
        }

        protected BombCategory(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, BombCategory.Type bombType)
            : base(name, spriteName, posX, posY)
        {
            this.BombType = bombType;
        }

        // Data: ---------------
        ~BombCategory()
        {
        }

        // this is just a placeholder, who knows what data will be stored here
        protected BombCategory.Type BombType;

    }
}

// --- End of File ---
