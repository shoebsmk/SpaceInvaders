//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SampleCommand : Command
    {
        public SampleCommand(String txt)
        {
            // string only for testing
            this.pString = txt;
        }

        public override void Execute(float deltaTime)
        {
            Debug.WriteLine(" {0} time:{1} ", this.pString, TimerEventMan.GetCurrTime());
            Debug.WriteLine( "---LATE COMMAND--");
        }

        private String pString;
    }
}

// --- End of File ---
