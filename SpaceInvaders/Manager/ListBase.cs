//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;

namespace SpaceInvaders
{
    public abstract class ListBase
    {
        abstract public void AddToFront(NodeBase pNode);
        abstract public void Remove(NodeBase pNode);
        abstract public NodeBase RemoveFromFront();

        abstract public Iterator GetIterator();
    }
}

// --- End of File ---
