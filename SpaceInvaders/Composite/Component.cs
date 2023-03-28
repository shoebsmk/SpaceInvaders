//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Component : ColVisitor
    {
        public enum Container
        {
            LEAF,
            COMPOSITE,
            Unknown
        }

        public Component(Component.Container _type)
        {
            this.type = _type;

            this.pParent = null;
            this.pReverse = null;
        }

        public virtual int GetNumChildren()
        {
            return 0;
        }

        public abstract void Print();
        //public abstract void Move(float x, float y);
        public abstract void Add(Component c);

        public abstract void Remove(Component c);
        public abstract void DumpNode();

        virtual public void Resurrect()
        {
            this.pParent = null;
            this.pReverse = null;
        }

        // Data
        public Container type;
        public Component pParent;
        public Component pReverse;
    }
}

// --- End of File ---
