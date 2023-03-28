//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SpaceInvaders
{
    public class Texture : SLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            Aliens,
            Stitch,
            Birds,
            PeaShooter,
            Skeleton,
            Running,
            Flying,
            HotPink,
            PacMan,
            SpaceInvaders,
            NullObject,
            Consolas36pt,

            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------
        public Texture()
        : base()
        {
            // Do the create and load
            //LTN - Texture
            this.poAzulTexture = new Azul.Texture();
            Debug.Assert(this.poAzulTexture != null);

            this.name = Texture.Name.Uninitialized;
        }
        public Texture(Name name, string pTextureName)
        : base()
        {
            Debug.Assert(pTextureName != null);

            // Do the create and load
            //LTN - Texture
            this.poAzulTexture = new Azul.Texture(pTextureName);
            Debug.Assert(this.poAzulTexture != null);

            this.name = name;
        }

        //------------------------------------
        // Methods
        //------------------------------------
        public void Set(Name name, string pTextureName)
        {
            Debug.Assert(pTextureName != null);
            Debug.Assert(this.poAzulTexture != null);

            this.poAzulTexture.Set(pTextureName, Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
            this.name = name;
        }

        public Azul.Texture GetAzulTexture()
        {
            return this.poAzulTexture;
        }

        private void privClear()
        {
            // Clear with a default texture
            Debug.Assert(this.poAzulTexture != null);
            this.poAzulTexture.Set("HotPink.tga", Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);

            this.name = Name.Uninitialized;
        }

        //------------------------------------
        // Override
        //------------------------------------

        override public System.Enum GetName()
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

            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            if (this.poAzulTexture == null)
            {
                Debug.WriteLine("      poAzulTexture: {0} ", this.poAzulTexture.GetHashCode());
            }
            else
            {
                Debug.WriteLine("      poAzulTexture: {0} ", this.poAzulTexture.GetHashCode());
            }

            // Let the base print its contribution
            this.baseDump();
        }

        //------------------------------------
        // Data
        //------------------------------------
        public Name name;
        public Azul.Texture poAzulTexture;
    }
}

// --- End of File ---
