//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------
    public class TimerEvent : DLink
    {
        //-----------------------------------------------------------
        // Enum
        //-----------------------------------------------------------
        public enum Name
        {
            Animation,
            MissileSpawn,
            BombSpawn,
            Counter,
            Move,
            RepeatSample,
            TimedCharacter,
            AlienExplosion,
            ShipExplosion,
            AlienShotExplosion,

            Uninitialized
        }

        //-----------------------------------------------------------
        // Constructor
        //-----------------------------------------------------------
        public TimerEvent()
            : base()
        {
            this.name = TimerEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }

        //------------------------------------------------------------
        // Methods
        //------------------------------------------------------------

        public void Set(TimerEvent.Name eventName, Command pCommand, float deltaTimeToTrigger)
        {
            Debug.Assert(pCommand != null);

            this.name = eventName;
            this.pCommand = pCommand;
            this.deltaTime = deltaTimeToTrigger;

            // set the trigger time
            this.triggerTime = TimerEventMan.GetCurrTime() + deltaTimeToTrigger;
        }
        public void Process()
        {
            // make sure the command is valid
            Debug.Assert(this.pCommand != null);
            // fire off command
            this.pCommand.Execute(deltaTime);
        }
        public new void Clear()   // the "new" is there to shut up warning - overriding at derived class
        {
            this.name = TimerEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }

        public override System.Enum GetName()
        {
            return this.name;
        }
        override public void Wash()
        {
            this.baseClear();
            this.Clear();
        }
        override public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("      Command: {0}", this.pCommand);
            Debug.WriteLine("   Event Name: {0}", this.name);
            Debug.WriteLine(" Trigger Time: {0}", this.triggerTime);
            Debug.WriteLine("   Delta Time: {0}", this.deltaTime);

            // Let the base print its contribution
            this.baseDump();
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        public Name name;
        public Command pCommand;
        public float triggerTime;
        public float deltaTime;
    }
}

// --- End of File ---
