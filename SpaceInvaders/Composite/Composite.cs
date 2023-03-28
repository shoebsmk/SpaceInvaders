﻿//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Composite : GameObject
    {
        public Composite()
            : base(Component.Container.COMPOSITE,
                  GameObject.Name.NullObject,
                  SpriteGame.Name.NullObject)
        {
            // LTN - Composite
            this.poDLinkMan = new DLinkMan();
        }
        public Composite(GameObject.Name name, SpriteGame.Name spriteName)
        : base(Component.Container.COMPOSITE,
               name,
               spriteName)
        {
            this.poDLinkMan = new DLinkMan();
        }

        override public void Add(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            Debug.Assert(this.poDLinkMan != null);
            this.poDLinkMan.AddToFront(pComponent);

            pComponent.pParent = this;

            //GameObjectNodeMan.Attach((GameObject)pComponent);
        }

        override public void Remove(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            Debug.Assert(this.poDLinkMan != null);
            this.poDLinkMan.Remove(pComponent);
        }

        override public void Resurrect()
        {
            // check the DLinkMan
            Debug.Assert(this.poDLinkMan.poHead == null);

            base.Resurrect();
        }

        public Component GetHead()
        {
            Debug.Assert(this.poDLinkMan != null);
            Component pHead = (GameObject)this.poDLinkMan.poHead;
            return pHead;
        }
        public Component GetTail()
        {
            Debug.Assert(this.poDLinkMan != null);
            Component pHead = (GameObject)this.poDLinkMan.GetLast();
            return pHead;
        }


        public override void Print()
        {
            Debug.WriteLine("");
            Debug.WriteLine("Composite:");

            // walk through the list and render
            Iterator pIt = this.poDLinkMan.GetIterator();
            Debug.Assert(pIt != null);

            for(pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                GameObject pNode = (GameObject)pIt.Current();

                // Update the node
                Debug.Assert(pNode != null);

                pNode.Dump();
            }
        }

        public override void Wash()
        {
            // shouldn't be called
            Debug.Assert(false);
        }

        override public void DumpNode()
        {
            if (IteratorForwardComposite.GetParent(this) != null)
            {
                Debug.WriteLine(" GameObject Name:({0}) parent:{1} <---- Composite", this.GetHashCode(), IteratorForwardComposite.GetParent(this).GetHashCode());
            }
            else
            {
                Debug.WriteLine(" GameObject Name:({0}) parent:null <---- Composite", this.GetHashCode());
            }
        }
        public override int GetNumChildren()
        {
            int count = 0;

            // walk through the list and render
            Iterator pIt = this.poDLinkMan.GetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                count++;
            }

            return count;
        }

        protected DLinkMan poDLinkMan;
    }
}

// --- End of File ---
