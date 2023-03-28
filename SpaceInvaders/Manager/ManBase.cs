//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // Whole class should have NO knowledge of Node
    abstract public class ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public ManBase(ListBase _poActive, ListBase _poReserve, int InitialNumReserved = 5, int DeltaGrow = 2)
        {
            // Check now or pay later
            Debug.Assert(_poActive != null);
            Debug.Assert(_poReserve != null);
            Debug.Assert(InitialNumReserved >= 0);
            Debug.Assert(DeltaGrow > 0);

            // Initialize all variables
            this.mDeltaGrow = DeltaGrow;
            this.mNumReserved = 0;
            this.mNumActive = 0;
            this.mTotalNumNodes = 0;
            this.poActive = _poActive;
            this.poReserve = _poReserve;

            // Preload the reserve
            this.privFillReservedPool(InitialNumReserved);
        }

        //----------------------------------------------------------------------
        // Base methods - called in Derived class but lives in Base
        //----------------------------------------------------------------------

        protected void baseSetReserve(int reserveNum, int reserveGrow)
        {
            this.mDeltaGrow = reserveGrow;

            if (reserveNum > this.mNumReserved)
            {
                // Preload the reserve
                this.privFillReservedPool(reserveNum - this.mNumReserved);
            }
        }

        public NodeBase baseAdd()
        {
            Iterator pIt = poReserve.GetIterator();
            Debug.Assert(pIt != null);

            // Are there any nodes on the Reserve list?
            if (pIt.First() == null)
            {
                // refill the reserve list by the DeltaGrow
                this.privFillReservedPool(this.mDeltaGrow);
            }

            // Always take from the reserve list
            NodeBase pNodeBase = poReserve.RemoveFromFront();
            Debug.Assert(pNodeBase != null);

            // Wash it
            /*this.derivedWash(pNodeBase);*/
            pNodeBase.Wash();

            // Update stats
            this.mNumActive++;
            this.mNumReserved--;

            // copy to active
            poActive.AddToFront(pNodeBase);

            // YES - here's your new one (may its reused from reserved)
            return pNodeBase;
        }
        public NodeBase baseAddToEnd()
        {
            Iterator pIt = poReserve.GetIterator();
            Debug.Assert(pIt != null);

            // Are there any nodes on the Reserve list?
            if (pIt.First() == null)
            {
                // refill the reserve list by the DeltaGrow
                this.privFillReservedPool(this.mDeltaGrow);
            }

            // Always take from the reserve list
            NodeBase pNodeBase = poReserve.RemoveFromFront();
            Debug.Assert(pNodeBase != null);

            // Wash it
            /*this.derivedWash(pNodeBase);*/
            pNodeBase.Wash();

            // Update stats
            this.mNumActive++;
            this.mNumReserved--;

            // copy to active
            DLinkMan dLinkManPOA = (DLinkMan)poActive;
            dLinkManPOA.AddToEnd(pNodeBase);

            // YES - here's your new one (may its reused from reserved)
            return pNodeBase;
        }

        public NodeBase baseAddPriority(NodeBase pNodeTarget)
        {
            Iterator pIt = poReserve.GetIterator();
            Debug.Assert(pIt != null);

            // Are there any nodes on the Reserve list?
            if (pIt.First() == null)
            {
                // refill the reserve list by the DeltaGrow
                this.privFillReservedPool(this.mDeltaGrow);
            }

            // Always take from the reserve list
            NodeBase pNodeBase = poReserve.RemoveFromFront();
            Debug.Assert(pNodeBase != null);

            // Wash it
            /*this.derivedWash(pNodeBase);*/
            pNodeBase.Wash();

            // Update stats
            this.mNumActive++;
            this.mNumReserved--;

            // copy to active
            
            DLinkMan dLinkManPOA = (DLinkMan)poActive;
            dLinkManPOA.AddBefore(pNodeBase, pNodeTarget);


            // YES - here's your new one (may its reused from reserved)
            return pNodeBase;
        }



        protected Iterator baseGetIterator()
        {
            return poActive.GetIterator();
        }

        protected NodeBase baseFind(NodeBase pNodeTarget)
        {
            // search the active list
            Iterator pIt = poActive.GetIterator();
            Debug.Assert(pIt != null);

            // Found node
            NodeBase pNode = null;

            // iterate through the nodes
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                // Downcast (its OK - homogeneous list)
                NodeBase pTmp = (NodeBase)pIt.Current();
                if (pTmp.Compare(pNodeTarget))
                {
                    // found it
		   pNode = pTmp;
                    break;
                }
            }

            return pNode;
        }
        public void baseRemove(NodeBase pNode)
        {
            Debug.Assert(pNode != null);

            // Don't do the work here... delegate it
            poActive.Remove(pNode);

            // wash it before returning to reserve list
            /*this.derivedWash(pNode);*/
            pNode.Wash();

            // add it to the return list
            poReserve.AddToFront(pNode);

            // stats update
            this.mNumActive--;
            this.mNumReserved++;
        }
        protected void baseDumpStats()
        {
            Debug.WriteLine("");
            Debug.WriteLine("         mDeltaGrow: {0} ", mDeltaGrow);
            Debug.WriteLine("     mTotalNumNodes: {0} ", mTotalNumNodes);
            Debug.WriteLine("       mNumReserved: {0} ", mNumReserved);
            Debug.WriteLine("         mNumActive: {0} \n", mNumActive);

        }
        protected void baseDump()
        {
            this.baseDumpStats();

            Iterator pItActive = poActive.GetIterator();
            Debug.Assert(pItActive != null);

            NodeBase pNodeActive = (NodeBase)pItActive.First();
            if (pNodeActive == null)
            {
                Debug.WriteLine("    Active Head: null");
            }
            else
            {
                Debug.WriteLine("    Active Head:  {0} ({1})", pNodeActive.GetName(), pNodeActive.GetHashCode());
            }

            Iterator pItReserve = poReserve.GetIterator();
            Debug.Assert(pItReserve != null);

            NodeBase pNodeReserve = (NodeBase)pItReserve.First();
            if (pNodeReserve == null)
            {
                Debug.WriteLine("   Reserve Head: null\n");
            }
            else
            {
                Debug.WriteLine("   Reserve Head:  {0} ({1})\n", pNodeReserve.GetName(), pNodeReserve.GetHashCode());
            }

            Debug.WriteLine("   ------ Active List: -----------\n");


            int i = 0;

           // iterate through the nodes
           for (pItActive.First(); !pItActive.IsDone(); pItActive.Next())
            {
                Debug.WriteLine("   {0}: -------------", i);
                NodeBase pTmp = (NodeBase)pItActive.Current();

                pTmp.Dump();
                i++;
            }

            Debug.WriteLine("");
            Debug.WriteLine("   ------ Reserve List: ----------\n");

            i = 0;
            // iterate through the nodes
            for (pItReserve.First(); !pItReserve.IsDone(); pItReserve.Next())
            {
                Debug.WriteLine("   {0}: -------------", i);
                NodeBase pTmp = (NodeBase)pItReserve.Current();

                pTmp.Dump();
                i++;
            }

            Debug.WriteLine("\n   ------ End ------\n");
        }

        //----------------------------------------------------------------------
        // Abstract methods - the "contract" Derived class must implement
        //----------------------------------------------------------------------
        abstract protected NodeBase derivedCreateNode();


        //----------------------------------------------------------------------
        // Private methods - helpers
        //----------------------------------------------------------------------
        private void privFillReservedPool(int count)
        {
            // doesn't make sense if its not at least 1
            Debug.Assert(count >= 0);

            this.mTotalNumNodes += count;
            this.mNumReserved += count;

            // Preload the reserve
            for (int i = 0; i < count; i++)
            {
                NodeBase pNode = this.derivedCreateNode();
                Debug.Assert(pNode != null);

                poReserve.AddToFront(pNode);
            }
        }

        //----------------------------------------------------------------------
        // Data:
        //----------------------------------------------------------------------
        private ListBase poActive;
        private ListBase poReserve;
        private int mDeltaGrow;
        private int mTotalNumNodes;
        private int mNumReserved;
        private int mNumActive;

    }
}

// --- End of File ---
