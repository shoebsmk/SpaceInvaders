//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneOver : SceneState
    {
        SceneContext pSceneContext;

        public SceneOver(SceneContext _pSceneContext)
        {
            pSceneContext = _pSceneContext;
            this.Initialize();

        }

        public override void Initialize()
        {
            this.poTimerEventMan = new TimerEventMan();
            TimerEventMan.SetActive(poTimerEventMan);

            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);

            TextureMan.Add(Texture.Name.Consolas36pt, "consolas36pt.tga");
            GlyphMan.AddXml("Consolas36pt.xml", Glyph.Name.Consolas36pt, Texture.Name.Consolas36pt);

            Font pFont = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "GAME OVER", Glyph.Name.SpaceInvaders, 260, 420);

            Font pFont2 = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Press Y to play again", Glyph.Name.SpaceInvaders, 135, 120);

            pFont.SetColor(0.10f, 1.0f, 0.50f);
/*
            this.poFontMan = new FontMan(3, 1);
            FontMan.SetActive(this.poFontMan);
            TimedCharacterFactory.Install("GAME OVER", 1.5f, 0.2f, 260, 420, 0.9f, 0.1f, 0.1f);*/
        }
        public override void Update(float systemTime)
        {

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_Y) == true)
            {

                pSceneContext.SetState(SceneContext.Scene.Select);

            }

            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Input
            //InputMan.Update();

            // Run based on simulation stepping
            if (Simulation.GetTimeStep() > 0.0f)
            {
                // Fire off the timer events
                TimerEventMan.Update(Simulation.GetTotalTime());

                // Do the collision checks
                ColPairMan.Process();

                // walk through all objects and push to flyweight
                GameObjectNodeMan.Update();

                // Delete any objects here...
                DelayedObjectMan.Process();
            }
        }
        public override void Draw()
        {
            // draw all objects
            SpriteBatchMan.Draw();
        }
        public override void Entering()
        {
            // update SpriteBatchMan()
            //FontMan.SetActive(this.poFontMan);
            ScenePlay.score = 0;

            TimerEventMan.SetActive(this.poTimerEventMan);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
        }
        public override void Leaving()
        {
            // update SpriteBatchMan()
            this.TimeAtPause = TimerEventMan.GetCurrTime();
        }

        public override int GetSceneId()
        {
            return 2;
        }

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        public TimerEventMan poTimerEventMan;
        public FontMan poFontMan;
    }
}
// --- End of File ---
