//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class CounterEvent : Command
    {
        public CounterEvent()
        {
            //this.count = 0;
        }

        override public void Execute(float deltaTime)
        {
            //Debug.WriteLine("event: {0}", deltaTime);

            Debug.WriteLine("{0}", count);

            // Add itself back to timer
            //  TimerMan.Add(TimeEvent.Name.Counter, this, deltaTime);

            Font pFont = FontMan.Add(Font.Name.TestMessage,
                SpriteBatch.Name.Texts,
                "c " + count,
                Glyph.Name.Consolas36pt,
                20 + 100 * (count / 10),
                700 - count * 40 + (count / 10) * 400);

            pFont.SetColor(0.10f, 1.0f, 0.10f);
            count++;

        }

        public static int count = 0;
    }
}

// --- End of File ---
