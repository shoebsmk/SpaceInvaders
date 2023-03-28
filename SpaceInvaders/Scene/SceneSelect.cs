//-----------------------------------------------------------------------------
// Copyright 2023, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneSelect : SceneState
    {
        SceneContext pSceneContext;
        Font pFontHighScore;

        public SceneSelect(SceneContext _pSceneContext)
        {
            this.Initialize();
            pSceneContext = _pSceneContext;
        }

        public override void Initialize()
        {
            this.poTimerEventMan = new TimerEventMan();
            TimerEventMan.SetActive(poTimerEventMan);

            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            this.poFontMan = new FontMan(3, 1);
            FontMan.SetActive(this.poFontMan);
            //LoadGlyphs();

            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);

            TextureMan.Add(Texture.Name.Consolas36pt, "consolas36pt.tga");
            GlyphMan.AddXml("Consolas36pt.xml", Glyph.Name.Consolas36pt, Texture.Name.Consolas36pt);


            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Score<1>", Glyph.Name.SpaceInvaders, 26, 740);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-Score", Glyph.Name.SpaceInvaders, 265, 740);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Score<2>", Glyph.Name.SpaceInvaders, 503, 740);

            Font pFont2 = FontMan.Add(Font.Name.Score, SpriteBatch.Name.Texts, "0768", Glyph.Name.SpaceInvaders, 63, 700);
            

            pFontHighScore = FontMan.Add(Font.Name.HighScoreSel, SpriteBatch.Name.Texts, "100", Glyph.Name.SpaceInvaders, 302, 700);
            Font pFont3 = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0456", Glyph.Name.SpaceInvaders, 540, 700);

            Font pFontCredits = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Credit", Glyph.Name.SpaceInvaders, 465, 30);
            Font pFont00 = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "00", Glyph.Name.SpaceInvaders, 608, 30);

            this.LoadOnEntry();
            //ScenePlay.score = 0;
            



        }

        private void LoadOnEntry()
        {
            /*Font pFont = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Attract Mode", Glyph.Name.SpaceInvaders, 250, 700);
            pFont.SetColor(0.90f, 0.90f, 0.90f);
*/
            
            TimedCharacterFactory.Install("PLAY", 1.0f, 0.1f, 300, 520, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("SPACE  INVADERS", 1.5f, 0.04f, 200, 450, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("*SCORE ADVANCE TABLE*", 2.0f, 0.04f, 150, 350, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("= ? MYSTERY", 2.0f, 0.05f, 290, 300, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("= 30 POINTS", 2.5f, 0.05f, 290, 250, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("= 20 POINTS", 3.0f, 0.05f, 290, 200, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("= 10 POINTS", 3.5f, 0.05f, 290, 150, 0.2f, 0.8f, 0.2f);
            TimedCharacterFactory.Install("Press X to play", 3.5f, 0.05f, 205, 100, 0.8f, 0.8f, 0.8f);


        }

        public override void Update(float systemTime)
        {
            
            

            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Input
            InputMan.Update();

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

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_X) == true)
            {
                pSceneContext.SetState(SceneContext.Scene.Play);
            }

            



        }

        public override void Draw()
        {
            // draw all objects
            SpriteBatchMan.Draw();
        }

        public override void Entering()
        {
            TimerEventMan.SetActive(this.poTimerEventMan);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
            FontMan.SetActive(this.poFontMan);


            //  FontMan.Dump();
            //  TimerEventMan.Dump();


            // Update timer since last pause
            float t0 = GlobalTimer.GetTime();
            float t1 = this.TimeAtPause;
            float delta = t0 - t1;
            TimerEventMan.PauseUpdate(delta);

            
            pFontHighScore.UpdateMessage(ScenePlay.highScore.ToString());
            
        }
        public override void Leaving()
        {
            this.TimeAtPause = TimerEventMan.GetCurrTime();
            

            // FontMan.RemoveAll();

            // FontMan.Dump();
            //  TimerEventMan.Dump();

        }
        private static void LoadGlyphs()
        {
            GlyphMan.AddXml("Consolas36pt.xml", Glyph.Name.SpaceInvaders, Texture.Name.SpaceInvaders);

            GlyphMan.Add(Glyph.Name.SpaceInvaders, 65, Texture.Name.SpaceInvaders, 3, 36, 5, 8);    // .A
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 66, Texture.Name.SpaceInvaders, 11, 36, 5, 8);   // .B
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 67, Texture.Name.SpaceInvaders, 19, 36, 5, 8);   // .C
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 68, Texture.Name.SpaceInvaders, 27, 36, 5, 8);   // .D
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 69, Texture.Name.SpaceInvaders, 35, 36, 5, 8);   // .E
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 70, Texture.Name.SpaceInvaders, 43, 36, 5, 8);   // .F
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 71, Texture.Name.SpaceInvaders, 51, 36, 5, 8);   // .G
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 72, Texture.Name.SpaceInvaders, 59, 36, 5, 8);   // .H
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 73, Texture.Name.SpaceInvaders, 67, 36, 5, 8);   // .I
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 74, Texture.Name.SpaceInvaders, 75, 36, 5, 8);   // .J
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 75, Texture.Name.SpaceInvaders, 83, 36, 5, 8);   // .K
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 76, Texture.Name.SpaceInvaders, 91, 36, 5, 8);   // .L
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 77, Texture.Name.SpaceInvaders, 99, 36, 5, 8);   // .M
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 78, Texture.Name.SpaceInvaders, 3, 46, 5, 8);    // .N
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 79, Texture.Name.SpaceInvaders, 11, 46, 5, 8);   // .O
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 80, Texture.Name.SpaceInvaders, 19, 46, 5, 8);   // .P
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 81, Texture.Name.SpaceInvaders, 27, 46, 5, 8);   // .Q
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 82, Texture.Name.SpaceInvaders, 35, 46, 5, 8);   // .R
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 83, Texture.Name.SpaceInvaders, 43, 46, 5, 8);   // .S
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 84, Texture.Name.SpaceInvaders, 51, 46, 5, 8);   // .T
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 85, Texture.Name.SpaceInvaders, 59, 46, 5, 8);   // .U
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 86, Texture.Name.SpaceInvaders, 67, 46, 5, 8);   // .V
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 87, Texture.Name.SpaceInvaders, 75, 46, 5, 8);   // .W
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 88, Texture.Name.SpaceInvaders, 83, 46, 5, 8);   // .X
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 89, Texture.Name.SpaceInvaders, 91, 46, 5, 8);   // .Y
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 90, Texture.Name.SpaceInvaders, 99, 46, 5, 8);   // .Z

            GlyphMan.Add(Glyph.Name.SpaceInvaders, 48, Texture.Name.SpaceInvaders, 3, 56, 5, 8);    // 0
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 49, Texture.Name.SpaceInvaders, 11, 56, 5, 8);   // 1
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 50, Texture.Name.SpaceInvaders, 19, 56, 5, 8);   // 2
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 51, Texture.Name.SpaceInvaders, 27, 56, 5, 8);   // 3
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 52, Texture.Name.SpaceInvaders, 35, 56, 5, 8);   // 4
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 53, Texture.Name.SpaceInvaders, 43, 56, 5, 8);   // 5
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 54, Texture.Name.SpaceInvaders, 51, 56, 5, 8);   // 6
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 55, Texture.Name.SpaceInvaders, 59, 56, 5, 8);   // 7
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 56, Texture.Name.SpaceInvaders, 67, 56, 5, 8);   // 8
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 57, Texture.Name.SpaceInvaders, 75, 56, 5, 8);   // 9

            GlyphMan.Add(Glyph.Name.SpaceInvaders, 60, Texture.Name.SpaceInvaders, 83, 56, 5, 8);   // <
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 62, Texture.Name.SpaceInvaders, 91, 56, 5, 8);   // >
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 32, Texture.Name.SpaceInvaders, 99, 56, 1, 8);   // Space
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 61, Texture.Name.SpaceInvaders, 107, 56, 5, 8);  // =
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 42, Texture.Name.SpaceInvaders, 115, 56, 5, 8);  // *
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 63, Texture.Name.SpaceInvaders, 123, 56, 5, 8);  // ?
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 45, Texture.Name.SpaceInvaders, 131, 56, 5, 8);  // -
        }

        public override int GetSceneId()
        {
            return 0;
        }

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        public TimerEventMan poTimerEventMan;
        public FontMan poFontMan;
        //public int sceneID = 0;

    }
}

// --- End of File ---
