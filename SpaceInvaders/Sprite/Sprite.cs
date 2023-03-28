//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SpaceInvaders
{
    public class Sprite : SLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            RedBird,
            YellowBird,
            GreenBird,
            WhiteBird,

            RedGhost,
            PinkGhost,
            BlueGhost,
            OrangeGhost,
            MsPacMan,
            PowerUpGhost,
            Prezel,

            PeaShooter,
            Skeleton,
            Running,
            Flying,

            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------

        public Sprite()
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;

            this.name = Name.Uninitialized;
            this.pImage = null;

            this.poColor = new Azul.Color();
            Debug.Assert(this.poColor != null);

            this.poAzulSprite = new Azul.Sprite();
            Debug.Assert(this.poAzulSprite != null);

            // Temp instead of dynamically calling each time
            Sprite.psRect = new Azul.Rect();
            Debug.Assert(Sprite.psRect != null);

        }


        //------------------------------------
        // Methods
        //------------------------------------
        public void Update()
        {
            this.poAzulSprite.x = this.x;
            this.poAzulSprite.y = this.y;
            this.poAzulSprite.sx = this.sx;
            this.poAzulSprite.sy = this.sy;
            this.poAzulSprite.angle = this.angle;

            this.poAzulSprite.Update();
        }
        public void Render()
        {
            this.poAzulSprite.Render();
        }
        public void Set(Name name, Image pImage, Azul.Rect pScreenRect)
        {
            Debug.Assert(pImage != null);
            Debug.Assert(pScreenRect != null);
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(this.poColor != null);

            this.pImage = pImage;
            this.name = name;

            this.poColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            this.poAzulSprite.Swap(pImage.pTexture.poAzulTexture, pImage.poRect, pScreenRect, poColor);
            this.poAzulSprite.Update();

            this.x = poAzulSprite.x;
            this.y = poAzulSprite.y;
            this.sx = poAzulSprite.sx;
            this.sy = poAzulSprite.sy;
            this.angle = poAzulSprite.angle;
        }
        private void privClear()
        {
            Debug.Assert(this.poColor != null);
            Debug.Assert(this.poAzulSprite != null);

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;

            this.name = Name.Uninitialized;
            this.pImage = null;

            this.poColor.Set(1.0f, 1.0f, 1.0f, 1.0f);

            Image pImage = ImageMan.Find(Image.Name.HotPink);
            Debug.Assert(pImage != null);

            Sprite.psRect.Set(0.0f, 0.0f, 1.0f, 1.0f);
            this.poAzulSprite.Swap(pImage.pTexture.poAzulTexture, pImage.poRect, psRect, poColor);
            this.poAzulSprite.Update();
        }

        //------------------------------------
        // Override
        //------------------------------------
        public override object GetName()
        {
            return this.name;
        }
        override public void Wash()
        {
            this.baseClear();
            this.privClear();
        }
        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("             Image: {0} ({1})", this.pImage.name, this.pImage.GetHashCode());
            Debug.WriteLine("        AzulSprite: ({0})", this.poAzulSprite.GetHashCode());
            Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
            Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
            Debug.WriteLine("           (angle): {0}", this.angle);

            // Let the base print its contribution
            this.baseDump();
        }

        //------------------------------------
        // Data
        //------------------------------------
        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        public Name name;
        public Image pImage;
        public Azul.Color poColor;
        private Azul.Sprite poAzulSprite;
        private static Azul.Rect psRect;
    }
}

// --- End of File ---

