//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    

    class SpaceInvaders : Azul.Game
    {
        SceneContext pSceneContext = null;

      


        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("Sprint 6");
            this.SetWidthHeight(672, 768);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            //-----------------------------------
            // Load Managers
            //-----------------------------------

            TextureMan.Create(0, 1);
            ImageMan.Create(0, 1);
            SpriteGameMan.Create(2, 1);
            SpriteBoxMan.Create();
            SpriteBatchMan.Create();
            SpriteGameProxyMan.Create(10,1);
            TimerEventMan.Create();
            GameObjectNodeMan.Create();
            ColPairMan.Create();
            GhostMan.Create();
            Simulation.Create();
            GlyphMan.Create();
            FontMan.Create();


            
            pSceneContext = new SceneContext();

        }

        

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------

        
        public override void Update()
        {
            GlobalTimer.Update(this.GetTime());

            // Hack to proof of concept... 
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1) == true)
            {
                
                    pSceneContext.SetState(SceneContext.Scene.Select);

            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2) == true)
            {
                pSceneContext.SetState(SceneContext.Scene.Play);
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_3) == true)
            {
                pSceneContext.SetState(SceneContext.Scene.Over);
            }

            // Update the scene
            pSceneContext.GetState().Update(this.GetTime());


        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            pSceneContext.GetState().Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {
            FontMan.Destroy();
            GlyphMan.Destroy();
            GhostMan.Destroy();
            TimerEventMan.Destroy();
            ImageMan.Destroy();
            SpriteGameMan.Destroy();
            TextureMan.Destroy();
            SpriteBatchMan.Destroy();
            SpriteGameProxyMan.Destroy();
            SpriteBoxMan.Destroy();
            GameObjectNodeMan.Destroy();
            ColPairMan.Destroy();
        }

        public SpriteBatchMan poSpriteBatchMan;
        public TimerEventMan poTimerEventMan;

    }
}

// --- End of File ---
