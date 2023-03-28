﻿//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColSubject
    {
        public ColSubject()
        {
            this.pObjB = null;
            this.pObjA = null;
            this.pHead = null;
            this.poSLinkMan = new SLinkMan();
            Debug.Assert(this.poSLinkMan != null);
        }

        ~ColSubject()
        {
            this.pObjB = null;
            this.pObjA = null;
            // ToDo
            // Need to walk and nuke the list
            this.pHead = null;
        }

        public void Attach(ColObserver pObserver)
        {
            // protection
            Debug.Assert(pObserver != null);

            Debug.Assert(this.poSLinkMan != null);
            this.poSLinkMan.AddToFront(pObserver);

            pObserver.pSubject = this;

        }

        public void Notify()
        {
            Iterator iT = this.poSLinkMan.GetIterator();
            for (iT.First(); !iT.IsDone(); iT.Next())
            {
                ColObserver pNode = (ColObserver)iT.Current();
                Debug.Assert(pNode != null);

                // Fire off the listener
                pNode.Notify();
            }

        }

        public void Detach(ColObserver pObserver)
        {
            Debug.Assert(pObserver != null);

            this.poSLinkMan.Remove(pObserver);
        }


        // Data: ------------------------
        private ColObserver pHead;
        private SLinkMan poSLinkMan;
        public GameObject pObjA;
        public GameObject pObjB;


    }
}

// --- End of File ---
