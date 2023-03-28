//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienBase : Leaf
    {
        public enum Type
        {
            Octopus,
            Crab,
            Squid
        }
        /*public override void VisitMissileGroup(MissileGroup m)
        {
            // Bird vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Bird
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }



        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }*/

        protected AlienBase(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y)
            : base(gameName,spriteName,_x,_y)
        {

        }
    }
}

// --- End of File ---
