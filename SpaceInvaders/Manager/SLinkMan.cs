using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SLinkMan : ListBase
    {
        public SLinkMan() : base()
        {
            // LTN - Iterator
            poIterator = new SlinkIterator();
            this.poHead = null;
        }

        override public void AddToFront(NodeBase _pNode)
        {
            // ------------------------------
            // Add CODE/REFACTOR here
            // ------------------------------
            // Hello
            Debug.Assert(_pNode != null);
            SLink pNode = (SLink) _pNode;
            if (this.poHead == null)
            {
                this.poHead = pNode;
                pNode.pNext = null;
            }
            else
            {
                pNode.pNext = this.poHead;
                this.poHead = pNode;
            }

            Debug.Assert(this.poHead != null);
        }
        public void AddToEnd(NodeBase _pNode)
        {
            // ------------------------------
            // Add CODE/REFACTOR here
            SLink pNode = (SLink)_pNode;
            pNode.pNext = null;
            if (this.poHead == null)
            {
                this.poHead = (SLink)_pNode;

            }
            else
            {
                SLink pTmp = this.poHead;
                while (pTmp.pNext != null)
                {
                    pTmp = pTmp.pNext;
                    Debug.WriteLine(pTmp);
                }
                pTmp.pNext = (SLink)_pNode;
            }




            // ------------------------------

        }

        override public void Remove(NodeBase _pNode)
        {
            // ------------------------------
            // Add CODE/REFACTOR here
            // ------------------------------
            SLink pTmp = this.poHead;
            if (_pNode == pTmp)
            {
                this.poHead = pTmp.pNext;
            }
            while (pTmp.pNext != null)
            {
                if (_pNode == pTmp.pNext)
                {
                    if (pTmp.pNext.pNext == null)
                    {
                        pTmp.pNext = null;
                    }
                    else
                    {
                        pTmp.pNext = pTmp.pNext.pNext;
                    }
                }
                pTmp = pTmp.pNext;
                if (pTmp == null)
                {
                    break;
                }
            }
            //pTmp.Clear();
            //_pNode.Clear();
            //pTmp.pNext = _pNode;
        }

        override public NodeBase RemoveFromFront()
        {
            // ------------------------------
            // Add CODE/REFACTOR here
            Debug.Assert(poHead != null);
            Debug.Assert(this.poHead != null);


            SLink pNode = this.poHead;
            this.poHead = pNode.pNext;
            pNode.Clear();

            // ------------------------------
            return pNode;
        }

        public override Iterator GetIterator()
        {
            Debug.Assert(this.poIterator != null);
            //if (poHead == null) return null;
            this.poIterator.Reset(this.poHead);

            return this.poIterator;
        }

        // ---------------------------------------
        // DO not add/modify variables
        // ---------------------------------------
        // Data:
        public SLink poHead;
        public SlinkIterator poIterator;
    }
}
