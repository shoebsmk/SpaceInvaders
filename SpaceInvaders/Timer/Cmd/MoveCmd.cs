using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace SpaceInvaders

{
    class MoveCmd : Command
    {
        public MoveCmd(AlienGrid _pGrid)
        {
            // initialized the sprite animation is attached to
            this.pGameObject = _pGrid;
            Debug.Assert(this.pGameObject != null);


        }

        public override void Execute(float deltaTime)
        {
            /*float x = 1 - pGameObject.GetChildCount() / 55;
            Debug.WriteLine(x);*/
            pGameObject.Move();

            //Debug.WriteLine("Moved step- " + cnt);
            cnt++;
            if(pGameObject.columnsCnt > 0)
            {
                TimerEventMan.Add(TimerEvent.Name.Move, this, deltaTime);
            }
        }
        

        // Data: ---------------
        private AlienGrid pGameObject;
        private float cnt;
    }
}
