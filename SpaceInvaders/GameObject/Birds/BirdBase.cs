//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BirdBase : Leaf
    {
        public enum Type
        {
            Red,
            Yellow,
            Green,
            White
        }

        protected BirdBase(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y)
            : base(gameName,spriteName,_x,_y)
        {

        }
    }
}

// --- End of File ---
