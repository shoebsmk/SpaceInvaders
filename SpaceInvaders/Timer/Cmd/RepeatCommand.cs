﻿//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RepeatCommand : Command
    {
        public RepeatCommand(String txt, float deltaRepeatTime)
        {
            // string only here for testing purposes
            this.pString = txt;
            this.repeatDelta = deltaRepeatTime;
        }

        public override void Execute(float deltaTime)
        {
            Debug.WriteLine(" {0} time:{1} ", this.pString, TimerEventMan.GetCurrTime());

            // Add itself back to timer
            TimerEventMan.Add(TimerEvent.Name.RepeatSample, this, this.repeatDelta);
        }

        private String pString;
        private float repeatDelta;
    }
}

// --- End of File ---
