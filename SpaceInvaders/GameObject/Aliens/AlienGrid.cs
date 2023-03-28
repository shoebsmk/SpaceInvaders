//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGrid : Composite
    {
        public AlienGrid(IrrKlang.ISoundEngine _pSoundEngine)
            : base(GameObject.Name.AlienGrid, SpriteGame.Name.NullObject)
        {
            this.name = Name.AlienGrid;
            this.delta = 4.0f;
            this.poColObj.pColSprite.SetColor(0, 1, 0);

            Debug.Assert(_pSoundEngine != null);
            pSoundEngine = _pSoundEngine;
            pSndVader0 = pSoundEngine.GetSoundSource("fastinvader1.wav");
            pSndVader1 = pSoundEngine.GetSoundSource("fastinvader2.wav");
            pSndVader2 = pSoundEngine.GetSoundSource("fastinvader3.wav");
            pSndVader3 = pSoundEngine.GetSoundSource("fastinvader4.wav");
            Debug.Assert(pSndVader0 != null);
            Debug.Assert(pSndVader1 != null);
            Debug.Assert(pSndVader2 != null);
            Debug.Assert(pSndVader3 != null);

        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitGroup(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldGrid
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        /*public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdGroup vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }*/

        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
            updateNoColumns();

        }
        public void updateNoColumns()
        {
            IteratorForwardComposite pFor = new IteratorForwardComposite(this);

            Component pNode = pFor.First();
            GameObject pGameObj = (GameObject)pNode;
            int noOfColumns = 0;

            while (!pFor.IsDone())
            {
                pGameObj = (GameObject)pNode;

                //Get random column and its last child
                if ((GameObject.Name)pGameObj.GetName() == GameObject.Name.AlienColumn)
                {
                    noOfColumns++;
                   
                }
                pNode = pFor.Next();
            }

            columnsCnt = noOfColumns;
        }
        private void playNextSound()
        {

            if(soundCurr == 1)
            {
                pSoundEngine.Play2D(pSndVader0, false, false, false);
            }
            else if(soundCurr == 2)
            {
                pSoundEngine.Play2D(pSndVader1, false, false, false);
            }
            else if(soundCurr == 3)
            {
                pSoundEngine.Play2D(pSndVader3, false, false, false);
            }
            else if(soundCurr == 4)
            {
                pSoundEngine.Play2D(pSndVader3, false, false, false);
            }
            else
            {
                Debug.Assert(false);
            }
            soundCurr++;
            if(soundCurr >= 4)
            {
                soundCurr = 1;
            }
        }
        public void Move()
        {
            //Debug.WriteLine(this.GetHashCode());
            //STN - Only to iterate
            IteratorForwardComposite pIt = new IteratorForwardComposite(this);
            Debug.Assert(pIt != null);

            playNextSound();
            pSoundEngine.Play2D(pSndVader0, false, false, false);

            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            
            if (mD == true && lastMD == false)
            {
                MoveDown();
            }

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                pGameObj = (GameObject)pIt.Curr();
                Debug.Assert(pGameObj != null);

                pGameObj.x += delta;


            }

            

            pGameObj.Update();
            lastMD = mD;

        }
        public int GetChildCount()
        {
            int count = 1;
            IteratorForwardComposite pIt = new IteratorForwardComposite(this);
            Debug.Assert(pIt != null);

            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);


            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                pGameObj = (GameObject)pIt.Curr();
                Debug.Assert(pGameObj != null);
                count++;

            }

            return count;
        }
        public GameObject GetBottomAlien()
        {
            //STN - Only to iterate
            IteratorForwardComposite pFor = new IteratorForwardComposite(this);

            Component pNode = pFor.First();
            GameObject pGameObj = (GameObject)pNode;
            int rand = new Random().Next(0, columnsCnt);
            int breakLoop = 0;
            GameObject pChild = null;

            while (!pFor.IsDone())
            {
                pGameObj = (GameObject)pNode;

                //Get random column and its last child
                if((GameObject.Name)pGameObj.GetName() == GameObject.Name.AlienColumn)
                {

                    pChild =  (GameObject)IteratorForwardComposite.GetLastChild(pGameObj);
                    
                    breakLoop++;
                    if(breakLoop == rand)
                    {
                        return pChild;

                    }

                    
                }

                pNode = pFor.Next();

                
            }

            //Debug.Assert(pChild != null);
            
            return pChild;

        }

        bool lastMD = false;

        bool mD = false;


        public void MoveDown()
        {
            Debug.WriteLine("Alien Moving Down!");
            //STN - Only to iterate
            IteratorForwardComposite pIt = new IteratorForwardComposite(this);
            Debug.Assert(pIt != null);

            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            //delta = deltaX ;
            

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                pGameObj = (GameObject)pIt.Curr();
                Debug.Assert(pGameObj != null);
                pGameObj.y -= 5.0f;
                

            }

            pGameObj.Update();

        }


        public void SetDelta(float inDelta)
        {
            this.delta = inDelta;
        }
        
        public void SetMoveDown()
        {
            mD = true;
            lastMD = false;
        }
        
        

        // Data: ---------------
        private float delta;
        public int columnsCnt = 0;
        public int soundCnt = 4;
        public int soundCurr = 1;

        IrrKlang.ISoundEngine pSoundEngine;
        IrrKlang.ISoundSource pSndVader0;
        IrrKlang.ISoundSource pSndVader1;
        IrrKlang.ISoundSource pSndVader2;
        IrrKlang.ISoundSource pSndVader3;

    }
}

// --- End of File ---
