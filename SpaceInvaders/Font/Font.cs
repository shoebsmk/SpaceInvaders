//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Font : DLink
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            TestMessage,
            TestOneOff,
            TimedCharacter,
            Lives,
            Score,
            ScoreSel,
            HighScore,
            HighScoreSel,

            NullObject,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------

        public Font()
        {
            this.name = Name.Uninitialized;
            this.poSpriteFont = new SpriteFont();
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------

        public void Set(Font.Name name, string pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);

            this.name = name;
            this.poSpriteFont.Set(name, pMessage, glyphName, xStart, yStart);
        }


        public void UpdateMessage(string pMessage)
        {
            Debug.Assert(pMessage != null);
            Debug.Assert(this.poSpriteFont != null);
            this.poSpriteFont.UpdateMessage(pMessage);
        }

        private void privClear()
        {
            this.name = Name.Uninitialized;
            this.poSpriteFont.Set(Font.Name.NullObject, pNullString, Glyph.Name.NullObject, 0.0f, 0.0f);
        }

        public void SetColor(float red, float green, float blue)
        {
            this.poSpriteFont.SetColor(red, green, blue);
        }


        //---------------------------------------------------------------------------------------------------------
        // Override
        //---------------------------------------------------------------------------------------------------------

        public override System.Enum GetName()
        {
            return this.name;
        }

        override public void Wash()
        {
            this.privClear();
        }

        override public bool Compare(NodeBase pTarget)
        {
            // This is used in ManBase.Find() 
            Debug.Assert(pTarget != null);

            Font pDataB = (Font)pTarget;

            bool status = false;

            if (this.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }

        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            base.baseDump();
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        public Name name;
        public SpriteFont poSpriteFont;
        static private string pNullString = "null";
    }
}

// --- End of File ---
