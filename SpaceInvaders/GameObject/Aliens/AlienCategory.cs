//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienCategory : Leaf
    {
        public enum Type
        {
            Root,
            Column,
            Brick,

            Grid,

            Octopus,
            Crab,
            Squid,

            Unitialized
        }

        protected AlienCategory(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, AlienCategory.Type alienType)
            : base(name, spriteName, posX, posY)
        {
            this.alienType = alienType;
        }
        // Data: ---------------
        ~AlienCategory()
        {
        }
        public AlienCategory.Type GetCategoryType()
        {
            return this.alienType;
        }

        // --------------------------------------------------------------------
        // Data:
        // --------------------------------------------------------------------

        // this is just a placeholder, who knows what data will be stored here
        protected AlienCategory.Type alienType;

    }
}

// --- End of File ---
