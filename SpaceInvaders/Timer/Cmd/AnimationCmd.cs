//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimationCmd : Command
    {
        public AnimationCmd(AlienGrid _pGrid, SpriteGame.Name spriteName)
        {
            // initialized the sprite animation is attached to
            this.pSprite = SpriteGameMan.Find(spriteName);
            Debug.Assert(this.pSprite != null);

            // LTN - AnimationCmd
            this.poSLinkMan = new SLinkMan();
            Debug.Assert(this.poSLinkMan != null);

            // need to keep iterator for state
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);

            Debug.Assert(_pGrid != null);
            pGrid = _pGrid;

        }

        public void Attach(Image.Name imageName)
        {
            // Get the image
            Image pImage = ImageMan.Find(imageName);
            Debug.Assert(pImage != null);

            // Create a new holder
            // LTN - AnimationCmd
            ImageNode pImageHolder = new ImageNode(pImage);
            Debug.Assert(pImageHolder != null);

            // Attach it to the Animation Sprite ( Push to front )
            this.poSLinkMan.AddToFront(pImageHolder);

            // update the iterator
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);
        }

        public override void Execute(float deltaTime)
        {
            // Wrap if at end of iteration list
            if (this.pIt.IsDone())
            {
                this.pIt.First();
            }

            //Debug.WriteLine("<--- trig");
            // Get the image
            ImageNode pImageNode = (ImageNode)this.pIt.Current();
            Debug.Assert(pImageNode != null);

            // advance for next iteration
            this.pIt.Next();

            // change image
            this.pSprite.SwapImage(pImageNode.pImage);

            // Add itself back to timer
            if(pGrid.columnsCnt > 0)
            {
                TimerEventMan.Add(TimerEvent.Name.Animation, this, deltaTime);
            }
        }

        // Data: ---------------
        private SpriteGame pSprite;
        private SLinkMan poSLinkMan;
        private Iterator pIt;
        private AlienGrid pGrid;
    }

}

// --- End of File ---
