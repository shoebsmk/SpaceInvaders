using System.Diagnostics;

namespace SpaceInvaders
{
    public class ScenePlay : SceneState
    {

        public static SceneContext pSceneContext;
        IrrKlang.ISoundEngine sndEngine = null;
        public static int score;
        public static int highScore = 100;
        AlienFactory AF;
        Composite pGrid;
        Composite pAlienRoot;
        GameObject pShieldRoot;

        public ScenePlay(SceneContext _pSceneContext)
        {
            this.Initialize();
            pSceneContext = _pSceneContext;
        }
        //Font pFontLives = null;


        public override void Initialize()
        {
            //------------------------------------------------------
            // Sound Experiment
            //------------------------------------------------------
            score = 0;
            // start up the engine
            sndEngine = new IrrKlang.ISoundEngine();
            IrrKlang.ISoundSource pSndVader0 = sndEngine.AddSoundSourceFromFile("fastinvader1.wav");
            IrrKlang.ISoundSource pSndVader1 = sndEngine.AddSoundSourceFromFile("fastinvader2.wav");
            IrrKlang.ISoundSource pSndVader2 = sndEngine.AddSoundSourceFromFile("fastinvader3.wav");
            IrrKlang.ISoundSource pSndVader3 = sndEngine.AddSoundSourceFromFile("fastinvader4.wav");
            IrrKlang.ISoundSource pSndShoot = sndEngine.AddSoundSourceFromFile("Shoot.wav");
            IrrKlang.ISoundSource pSndExplosion = sndEngine.AddSoundSourceFromFile("explosion.wav");
            IrrKlang.ISoundSource pInvaderkilled = sndEngine.AddSoundSourceFromFile("invaderkilled.wav");


            /*pSndVader0.DefaultVolume = 1.0f;
            IrrKlang.ISound pSnd = sndEngine.Play2D(pSndVader0, false, false, false);
            pSndVader0.DefaultVolume = 1.0f;*/

            this.poTimerEventMan = new TimerEventMan();
            TimerEventMan.SetActive(poTimerEventMan);

            /*IrrKlang.ISoundSource pSndVader1 = sndEngine.AddSoundSourceFromFile("fastinvader2.wav");
            pSndVader0.DefaultVolume = 0.0f;
            IrrKlang.ISound pSnd1 = sndEngine.Play2D(pSndVader1, false, false, false);
            pSndVader0.DefaultVolume = 1.0f;*/

            // play a sound file
            /*music = sndEngine.Play2D("theme.wav");
            music.Volume = 0.1f;*/


            //-----------------------------------
            // Load the Textures
            //-----------------------------------

            TextureMan.Add(Texture.Name.Birds, "birds_N_shield.tga");
            TextureMan.Add(Texture.Name.HotPink, "HotPink.tga");
            TextureMan.Add(Texture.Name.PeaShooter, "PeaShooter2.tga");
            TextureMan.Add(Texture.Name.Skeleton, "Skeleton2.tga");
            TextureMan.Add(Texture.Name.Running, "Running2.tga");
            TextureMan.Add(Texture.Name.Flying, "Flying2.tga");
            TextureMan.Add(Texture.Name.PacMan, "PacMan.tga");
            TextureMan.Add(Texture.Name.SpaceInvaders, "SpaceInvaders_ROM.tga");

            //-----------------------------------
            // Create Images
            //-----------------------------------

            // --- Birds ---

            ImageMan.Add(Image.Name.RedBird, Texture.Name.Birds, 47, 41, 48, 46);
            ImageMan.Add(Image.Name.YellowBird, Texture.Name.Birds, 124, 34, 60, 56);
            ImageMan.Add(Image.Name.GreenBird, Texture.Name.Birds, 246, 135, 99, 72);
            ImageMan.Add(Image.Name.WhiteBird, Texture.Name.Birds, 139, 131, 84, 97);
            ImageMan.Add(Image.Name.BlueBird, Texture.Name.Birds, 301, 49, 33, 33);

            ImageMan.Add(Image.Name.PeaShooter, Texture.Name.PeaShooter, 0, 0, 1518, 1551);
            ImageMan.Add(Image.Name.Skeleton, Texture.Name.Skeleton, 100, 25, 440, 450);
            ImageMan.Add(Image.Name.Running, Texture.Name.Running, 0, 0, 255, 255);
            ImageMan.Add(Image.Name.Flying, Texture.Name.Flying, 187, 187, 180, 180);

            ImageMan.Add(Image.Name.BombStraight, Texture.Name.Birds, 111, 101, 5, 49);
            ImageMan.Add(Image.Name.BombZigZag, Texture.Name.Birds, 132, 100, 20, 50);
            ImageMan.Add(Image.Name.BombCross, Texture.Name.Birds, 219, 103, 19, 47);

            // --- Pacman ---

            ImageMan.Add(Image.Name.RedGhost, Texture.Name.PacMan, 616, 148, 33, 33);
            ImageMan.Add(Image.Name.PinkGhost, Texture.Name.PacMan, 663, 148, 33, 33);
            ImageMan.Add(Image.Name.BlueGhost, Texture.Name.PacMan, 710, 148, 33, 33);
            ImageMan.Add(Image.Name.OrangeGhost, Texture.Name.PacMan, 757, 148, 33, 33);
            ImageMan.Add(Image.Name.MsPacMan, Texture.Name.PacMan, 797, 95, 46, 46);

            // --- Hot Pink ---

            ImageMan.Add(Image.Name.HotPink, Texture.Name.HotPink, 0, 0, 128, 128);


            // --- SpaceInvaders ---

            ImageMan.Add(Image.Name.OctopusA, Texture.Name.SpaceInvaders, 3, 3, 12, 8);
            ImageMan.Add(Image.Name.OctopusB, Texture.Name.SpaceInvaders, 18, 3, 12, 8);
            ImageMan.Add(Image.Name.CrabA, Texture.Name.SpaceInvaders, 33, 3, 11, 8);
            ImageMan.Add(Image.Name.CrabB, Texture.Name.SpaceInvaders, 47, 3, 11, 8);
            ImageMan.Add(Image.Name.SquidA, Texture.Name.SpaceInvaders, 61, 3, 8, 8);
            ImageMan.Add(Image.Name.SquidB, Texture.Name.SpaceInvaders, 72, 3, 8, 8);
            ImageMan.Add(Image.Name.Missile, Texture.Name.SpaceInvaders, 3, 29, 1, 4);

            //ImageMan.Add(Image.Name.Missile, Texture.Name.Birds, 73, 53, 5, 4);
            ImageMan.Add(Image.Name.Ship, Texture.Name.Birds, 10, 93, 30, 18);
            ImageMan.Add(Image.Name.Wall, Texture.Name.Birds, 40, 185, 20, 10);

            ImageMan.Add(Image.Name.Brick, Texture.Name.Birds, 20, 210, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top0, Texture.Name.Birds, 15, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top1, Texture.Name.Birds, 15, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Bottom, Texture.Name.Birds, 35, 215, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top0, Texture.Name.Birds, 75, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top1, Texture.Name.Birds, 75, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Bottom, Texture.Name.Birds, 55, 215, 10, 5);

            ImageMan.Add(Image.Name.PlayerShot, Texture.Name.SpaceInvaders, 3, 29, 1, 4);
            ImageMan.Add(Image.Name.PlayerShotExplosion, Texture.Name.SpaceInvaders, 7, 25, 8, 8);
            ImageMan.Add(Image.Name.SquigglyShotA, Texture.Name.SpaceInvaders, 18, 26, 3, 7);

            ImageMan.Add(Image.Name.PlungerShotA, Texture.Name.SpaceInvaders, 42, 27, 3, 6);

            ImageMan.Add(Image.Name.RollingShotA, Texture.Name.SpaceInvaders, 65, 26, 3, 7);

            ImageMan.Add(Image.Name.AlienExplosion, Texture.Name.SpaceInvaders, 83, 3, 13, 8);
            ImageMan.Add(Image.Name.AlienShotExplosion, Texture.Name.SpaceInvaders, 86, 25, 6, 8);
            ImageMan.Add(Image.Name.Saucer, Texture.Name.SpaceInvaders, 99, 3, 16, 8);
            ImageMan.Add(Image.Name.SaucerExplosion, Texture.Name.SpaceInvaders, 118, 3, 21, 8);


            ImageMan.Add(Image.Name.A, Texture.Name.SpaceInvaders, 3, 36, 5, 7);
            ImageMan.Add(Image.Name.B, Texture.Name.SpaceInvaders, 11, 36, 5, 7);
            ImageMan.Add(Image.Name.C, Texture.Name.SpaceInvaders, 19, 36, 5, 7);
            ImageMan.Add(Image.Name.D, Texture.Name.SpaceInvaders, 27, 36, 5, 7);
            ImageMan.Add(Image.Name.E, Texture.Name.SpaceInvaders, 35, 36, 5, 7);
            ImageMan.Add(Image.Name.F, Texture.Name.SpaceInvaders, 43, 36, 5, 7);
            ImageMan.Add(Image.Name.G, Texture.Name.SpaceInvaders, 51, 36, 5, 7);
            ImageMan.Add(Image.Name.H, Texture.Name.SpaceInvaders, 59, 36, 5, 7);
            ImageMan.Add(Image.Name.I, Texture.Name.SpaceInvaders, 67, 36, 5, 7);
            ImageMan.Add(Image.Name.J, Texture.Name.SpaceInvaders, 75, 36, 5, 7);
            ImageMan.Add(Image.Name.K, Texture.Name.SpaceInvaders, 83, 36, 5, 7);
            ImageMan.Add(Image.Name.L, Texture.Name.SpaceInvaders, 91, 36, 5, 7);
            ImageMan.Add(Image.Name.M, Texture.Name.SpaceInvaders, 99, 36, 5, 7);
            ImageMan.Add(Image.Name.N, Texture.Name.SpaceInvaders, 3, 46, 5, 7);
            ImageMan.Add(Image.Name.O, Texture.Name.SpaceInvaders, 11, 46, 5, 7);
            ImageMan.Add(Image.Name.P, Texture.Name.SpaceInvaders, 19, 46, 5, 7);
            ImageMan.Add(Image.Name.Q, Texture.Name.SpaceInvaders, 27, 46, 5, 7);
            ImageMan.Add(Image.Name.R, Texture.Name.SpaceInvaders, 35, 46, 5, 7);
            ImageMan.Add(Image.Name.S, Texture.Name.SpaceInvaders, 43, 46, 5, 7);
            ImageMan.Add(Image.Name.T, Texture.Name.SpaceInvaders, 51, 46, 5, 7);
            ImageMan.Add(Image.Name.U, Texture.Name.SpaceInvaders, 59, 46, 5, 7);
            ImageMan.Add(Image.Name.V, Texture.Name.SpaceInvaders, 67, 46, 5, 7);
            ImageMan.Add(Image.Name.W, Texture.Name.SpaceInvaders, 75, 46, 5, 7);
            ImageMan.Add(Image.Name.X, Texture.Name.SpaceInvaders, 83, 46, 5, 7);
            ImageMan.Add(Image.Name.Y, Texture.Name.SpaceInvaders, 91, 46, 5, 7);
            ImageMan.Add(Image.Name.Z, Texture.Name.SpaceInvaders, 99, 46, 5, 7);

            ImageMan.Add(Image.Name.Zero, Texture.Name.SpaceInvaders, 3, 56, 5, 7);
            ImageMan.Add(Image.Name.One, Texture.Name.SpaceInvaders, 11, 56, 5, 7);
            ImageMan.Add(Image.Name.Two, Texture.Name.SpaceInvaders, 19, 56, 5, 7);
            ImageMan.Add(Image.Name.Three, Texture.Name.SpaceInvaders, 27, 56, 5, 7);
            ImageMan.Add(Image.Name.Four, Texture.Name.SpaceInvaders, 35, 56, 5, 7);
            ImageMan.Add(Image.Name.Five, Texture.Name.SpaceInvaders, 43, 56, 5, 7);
            ImageMan.Add(Image.Name.Six, Texture.Name.SpaceInvaders, 51, 56, 5, 7);
            ImageMan.Add(Image.Name.Seven, Texture.Name.SpaceInvaders, 59, 56, 5, 7);
            ImageMan.Add(Image.Name.Eight, Texture.Name.SpaceInvaders, 67, 56, 5, 7);
            ImageMan.Add(Image.Name.Nine, Texture.Name.SpaceInvaders, 75, 56, 5, 7);
            ImageMan.Add(Image.Name.LessThan, Texture.Name.SpaceInvaders, 83, 56, 5, 7);
            ImageMan.Add(Image.Name.GreaterThan, Texture.Name.SpaceInvaders, 91, 56, 5, 7);
            ImageMan.Add(Image.Name.Space, Texture.Name.SpaceInvaders, 99, 56, 5, 7);
            ImageMan.Add(Image.Name.Equals, Texture.Name.SpaceInvaders, 107, 56, 5, 7);
            ImageMan.Add(Image.Name.Asterisk, Texture.Name.SpaceInvaders, 115, 56, 5, 7);
            ImageMan.Add(Image.Name.Question, Texture.Name.SpaceInvaders, 123, 56, 5, 7);
            ImageMan.Add(Image.Name.Hyphen, Texture.Name.SpaceInvaders, 131, 56, 5, 7);


            ImageMan.Add(Image.Name.Player, Texture.Name.SpaceInvaders, 3, 14, 13, 8);
            ImageMan.Add(Image.Name.PlayerExplosionA, Texture.Name.SpaceInvaders, 19, 14, 16, 8);
            ImageMan.Add(Image.Name.PlayerExplosionB, Texture.Name.SpaceInvaders, 38, 14, 16, 8);

            //--------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------


            SpriteGameMan.Add(SpriteGame.Name.PeaShooter, Image.Name.PeaShooter, 200, 400, 220, 230);
            SpriteGameMan.Add(SpriteGame.Name.Skeleton, Image.Name.Skeleton, 600, 400, 200, 220);
            SpriteGameMan.Add(SpriteGame.Name.Running, Image.Name.Running, 100, 180, 200, 200);
            SpriteGameMan.Add(SpriteGame.Name.Flying, Image.Name.Flying, 90, 230, 150, 150);

            SpriteGameMan.Add(SpriteGame.Name.RedBird, Image.Name.RedBird, 50, 500, 100, 100);
            SpriteGameMan.Add(SpriteGame.Name.YellowBird, Image.Name.YellowBird, 300, 400, 100, 100);
            SpriteGameMan.Add(SpriteGame.Name.GreenBird, Image.Name.GreenBird, 400, 200, 100, 100);
            SpriteGameMan.Add(SpriteGame.Name.WhiteBird, Image.Name.WhiteBird, 600, 300, 100, 100);
            SpriteGameMan.Add(SpriteGame.Name.BlueBird, Image.Name.BlueBird, 50, 50, 50, 50);


            // --- Pacman ---

            SpriteGameMan.Add(SpriteGame.Name.RedGhost, Image.Name.RedGhost, 100, 300, 30, 30);
            SpriteGameMan.Add(SpriteGame.Name.PinkGhost, Image.Name.PinkGhost, 300, 300, 30, 30);
            SpriteGameMan.Add(SpriteGame.Name.BlueGhost, Image.Name.BlueGhost, 500, 300, 30, 30);
            SpriteGameMan.Add(SpriteGame.Name.OrangeGhost, Image.Name.OrangeGhost, 700, 300, 30, 30);

            SpriteGameMan.Add(SpriteGame.Name.MsPacMan, Image.Name.MsPacMan, 150, 190, 100, 100);

            // --- BoxSprites ---
            SpriteBoxMan.Add(SpriteBox.Name.Box1, 550.0f, 500.0f, 50.0f, 150.0f);
            SpriteBoxMan.Add(SpriteBox.Name.Box2, 550.0f, 100.0f, 50.0f, 100.0f);
            SpriteBoxMan.Add(SpriteBox.Name.Box3, 350.0f, 200.0f, 120.0f, 80.0f);
            SpriteBoxMan.Add(SpriteBox.Name.Box4, 250.0f, 400.0f, 100.0f, 60.0f);
            SpriteBoxMan.Add(SpriteBox.Name.Box5, 300.0f, 300.0f, 200.0f, 20.0f);

            // --- SpaceInvaderSprites ---
            //SpriteGameMan.Add(SpriteGame.Name.Octopus, Image.Name.OctopusA, 300.0f, 300.0f, 36.0f, 25.0f);

            SpriteGameMan.Add(SpriteGame.Name.Squid, Image.Name.SquidB, 0, 0, 24, 25);
            SpriteGameMan.Add(SpriteGame.Name.Crab, Image.Name.CrabA, 0, 0, 28, 25);
            SpriteGameMan.Add(SpriteGame.Name.Octopus, Image.Name.OctopusA, 0, 0, 36, 25);
            SpriteGameMan.Add(SpriteGame.Name.Missile, Image.Name.Missile, 0, 0, 4, 20);
            SpriteGameMan.Add(SpriteGame.Name.Ship, Image.Name.Player, 500, 100, 80, 28);
            SpriteGameMan.Add(SpriteGame.Name.Wall, Image.Name.Wall, 400, 900, 800, 3);
            SpriteGameMan.Add(SpriteGame.Name.BombZigZag, Image.Name.SquigglyShotA, 200, 200, 8, 24);
            SpriteGameMan.Add(SpriteGame.Name.BombStraight, Image.Name.RollingShotA, 100, 100, 8, 24);
            SpriteGameMan.Add(SpriteGame.Name.BombDagger, Image.Name.PlungerShotA, 100, 100, 8, 24);

            SpriteGameMan.Add(SpriteGame.Name.AlienExplosion, Image.Name.AlienExplosion, 100, 100, 36, 25);
            SpriteGameMan.Add(SpriteGame.Name.AlienShotExplosion, Image.Name.AlienShotExplosion, 100, 100, 15, 20);
            SpriteGameMan.Add(SpriteGame.Name.PlayerExplosionA, Image.Name.PlayerExplosionA, 100, 100, 80, 28);
            SpriteGameMan.Add(SpriteGame.Name.PlayerExplosionB, Image.Name.PlayerExplosionB, 100, 100, 80, 28);
            SpriteGameMan.Add(SpriteGame.Name.PlayerShotExplosion, Image.Name.PlayerShotExplosion, 100, 100, 7, 25);

            SpriteGameMan.Add(SpriteGame.Name.Brick, Image.Name.Brick, 50, 25, 40, 20);
            SpriteGameMan.Add(SpriteGame.Name.Brick_LeftTop0, Image.Name.BrickLeft_Top0, 50, 25, 40, 20);
            SpriteGameMan.Add(SpriteGame.Name.Brick_LeftTop1, Image.Name.BrickLeft_Top1, 50, 25, 40, 20);
            SpriteGameMan.Add(SpriteGame.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom, 50, 25, 40, 20);
            SpriteGameMan.Add(SpriteGame.Name.Brick_RightTop0, Image.Name.BrickRight_Top0, 50, 25, 40, 20);
            SpriteGameMan.Add(SpriteGame.Name.Brick_RightTop1, Image.Name.BrickRight_Top1, 50, 25, 40, 20);
            SpriteGameMan.Add(SpriteGame.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom, 50, 25, 40, 20);


            //SpriteGameMan.Add(SpriteGame.Name.SquigglyShotA, Image.Name.SquigglyShotA, 200, 200, 20, 60);



            //-------------------------------------------------------
            // Create SpriteBatch
            //-------------------------------------------------------

            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            SpriteBatch pSB_GroupA_PacMan = SpriteBatchMan.AddPriority(SpriteBatch.Name.PacMan, 10);
            SpriteBatch pSB_GroupB_Misc = SpriteBatchMan.AddPriority(SpriteBatch.Name.Misc, 20);
            SpriteBatch pSB_GroupC_Birds = SpriteBatchMan.AddPriority(SpriteBatch.Name.Birds, 30);

            SpriteBatch pSB_Boxes = SpriteBatchMan.AddPriority(SpriteBatch.Name.Boxes, 30);
            SpriteBatch AlienBatch = SpriteBatchMan.AddPriority(SpriteBatch.Name.Aliens, 40);
            SpriteBatch pSB_Shields = SpriteBatchMan.Add(SpriteBatch.Name.Shields);

            //------------------------------------------------------
            // Font Experiment
            //------------------------------------------------------

            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);

            //TextureMan.Add(Texture.Name.Consolas36pt, "consolas36pt.tga");
            //GlyphMan.AddXml("Consolas36pt.xml", Glyph.Name.Consolas36pt, Texture.Name.Consolas36pt);
            LoadGlyphs();

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Score<1>", Glyph.Name.SpaceInvaders, 26, 740);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-Score", Glyph.Name.SpaceInvaders, 265, 740);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Score<2>", Glyph.Name.SpaceInvaders, 503, 740);

            Font pFontScore = FontMan.Add(Font.Name.Score, SpriteBatch.Name.Texts, "0768", Glyph.Name.SpaceInvaders, 63, 700);
            Font pFont2 = FontMan.Add(Font.Name.HighScore, SpriteBatch.Name.Texts, "8712", Glyph.Name.SpaceInvaders, 302, 700);
            Font pFont3 = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0456", Glyph.Name.SpaceInvaders, 540, 700);
            Font pFontlives = FontMan.Add(Font.Name.Lives, SpriteBatch.Name.Texts, "-", Glyph.Name.SpaceInvaders, 44, 30);

            //Credits
            Font pFontCredits = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "Credit", Glyph.Name.SpaceInvaders, 465, 30);
            Font pFont00 = FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "00", Glyph.Name.SpaceInvaders, 608, 30);

            //pFont.SetColor(1.0f, 1.0f, 1.0f);

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------

            InputSubject pInputSubject;
            pInputSubject = InputMan.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputMan.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputMan.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver());

            Simulation.SetState(Simulation.State.Realtime);


            //---------------------------------------------------------------------------------------------------------
            // Bomb
            //---------------------------------------------------------------------------------------------------------

            BombRoot pBombRoot = new BombRoot(GameObject.Name.BombRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pBombRoot.ActivateCollisionSprite(pSB_Boxes);

            /*Bomb pBombDagger = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombDagger, new FallDagger(), 600, 700);
            pBombDagger.ActivateCollisionSprite(pSB_Boxes);
            pBombDagger.ActivateSprite(pSB_GroupC_Birds);

            Bomb pBombStraight = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombStraight, new FallStraight(), 500, 700);
            pBombStraight.ActivateCollisionSprite(pSB_Boxes);
            pBombStraight.ActivateSprite(pSB_GroupC_Birds);

            Bomb pBombZigZag = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombZigZag, new FallZigZag(), 400, 700);
            pBombZigZag.ActivateCollisionSprite(pSB_Boxes);
            pBombZigZag.ActivateSprite(pSB_GroupC_Birds);*/
            /*
                        pBombRoot.Add(pBombStraight);
                        pBombRoot.Add(pBombDagger);
                        pBombRoot.Add(pBombZigZag);
            */
            GameObjectNodeMan.Attach(pBombRoot);

            //---------------------------------------------------------------------------------------------------------
            // Create Walls
            //---------------------------------------------------------------------------------------------------------

            // Wall Root
            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateSprite(pSB_GroupC_Birds);
            pWallGroup.ActivateCollisionSprite(pSB_Boxes);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, SpriteGame.Name.NullObject, 656, 400, 15, 680);
            pWallRight.ActivateCollisionSprite(pSB_Boxes);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, SpriteGame.Name.NullObject, 18, 400, 15, 680);
            pWallLeft.ActivateCollisionSprite(pSB_Boxes);

            WallTop pWallTop = new WallTop(GameObject.Name.WallTop, SpriteGame.Name.NullObject, 336, 670, 660, 10);
            pWallTop.ActivateCollisionSprite(pSB_Boxes);

            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, SpriteGame.Name.Wall, 400, 50, 850, 3);
            pWallBottom.ActivateCollisionSprite(pSB_Boxes);
            pWallBottom.ActivateSprite(pSB_GroupC_Birds);

            // Add to the composite the children
            pWallGroup.Add(pWallRight);
            pWallGroup.Add(pWallLeft);
            pWallGroup.Add(pWallTop);
            pWallGroup.Add(pWallBottom);

            GameObjectNodeMan.Attach(pWallGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create Missile
            //---------------------------------------------------------------------------------------------------------

            MissileGroup pMissileGroup = new MissileGroup();
            pMissileGroup.ActivateSprite(pSB_GroupC_Birds);
            pMissileGroup.ActivateCollisionSprite(pSB_Boxes);

            GameObjectNodeMan.Attach(pMissileGroup);


            /*MissileGroup pMissileGroup = new MissileGroup();
            pMissileGroup.ActivateSprite(pSB_GroupC_Birds);
            pMissileGroup.ActivateCollisionSprite(pSB_Boxes);

            pMissile = new Missile(SpriteGame.Name.BlueBird, 405, 100);
            pMissile.ActivateSprite(pSB_GroupC_Birds);
            pMissile.ActivateCollisionSprite(pSB_Boxes);

            pMissileGroup.Add(pMissile);

            GameObjectNodeMan.Attach(pMissileGroup);
            

            Debug.WriteLine("-------------------");*/

            //  pMissileGroup.Print();

            //---------------------------------------------------------------------------------------------------------
            // Bumper
            //---------------------------------------------------------------------------------------------------------

            BumperRoot pBumperRoot = new BumperRoot(GameObject.Name.BumperRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateSprite(pSB_Boxes);

            BumperRight pBumperRight = new BumperRight(GameObject.Name.BumperRight, SpriteGame.Name.NullObject, 675, 100, 10, 100);
            pBumperRight.ActivateCollisionSprite(pSB_Boxes);

            BumperLeft pBumperLeft = new BumperLeft(GameObject.Name.BumperLeft, SpriteGame.Name.NullObject, 0, 100, 10, 100);
            pBumperLeft.ActivateCollisionSprite(pSB_Boxes);

            // Add to the composite the children
            pBumperRoot.Add(pBumperRight);
            pBumperRoot.Add(pBumperLeft);

            GameObjectNodeMan.Attach(pBumperRoot);


            //---------------------------------------------------------------------------------------------------------
            // Ship
            //---------------------------------------------------------------------------------------------------------

            ShipRoot pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);

            GameObjectNodeMan.Attach(pShipRoot);

            ShipMan.Create(sndEngine, pSndShoot);



            //---------------------------------------------------------------------------------------------------------
            // Aliens Sq =30, Cr = 20, Oc = 10
            //---------------------------------------------------------------------------------------------------------


            // STN - Factory is only used once, to create objects then goes away
            // No need for keeping this long term

            pAlienRoot = (Composite)new AlienRoot(GameObject.Name.AlienRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            GameObjectNodeMan.Attach(pAlienRoot);

            AF = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes, pAlienRoot, sndEngine);
            AF.SetParent(pAlienRoot);

            pGrid = (Composite)AF.Create(GameObject.Name.AlienGrid);


            //CreateAlienSet();


            //---------------------------------------------------------------------------------------------------------
            // Shield 
            //---------------------------------------------------------------------------------------------------------

            pShieldRoot = ShieldFactory.CreateSingleShield(91, 150);
            /*GameObject pShieldRoot2 = ShieldFactory.CreateSingleShield(232, 150);
            GameObject pShieldRoot3 = ShieldFactory.CreateSingleShield(373, 150);
            GameObject pShieldRoo4 = ShieldFactory.CreateSingleShield(514, 150);*/



            //Debug.WriteLine("-------------------");

            /*IteratorForwardComposite pIt = new IteratorForwardComposite(pShieldRoot);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                Component pNode = pIt.Curr();
                //    pNode.Print();
            }*/

            //---------------------------------------------------------------------------------------------------------
            // ColPair 
            //---------------------------------------------------------------------------------------------------------

            // associate in a collision pair Birds


            ColPair pColPair;
            //TEMP
            // Alien vs Wall

            pColPair = ColPairMan.Add(ColPair.Name.Alien_Wall, pAlienRoot, pWallGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new GridObserver());



            // Alien vs Missile
            pColPair = ColPairMan.Add(ColPair.Name.Alien_Missile, pMissileGroup, pAlienRoot);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new RemoveAlienObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SndObserver(sndEngine, pInvaderkilled));



            // Missile vs Wall 
            pColPair = ColPairMan.Add(ColPair.Name.Missile_Wall, pMissileGroup, pWallTop);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new ShipRemoveMissileObserver());

            // Bomb vs Bottom
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Wall, pBombRoot, pWallBottom);
            pColPair.Attach(new BombObserver());




            // Bomb vs Ship
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Ship, pBombRoot, pShipRoot);
            pColPair.Attach(new RemoveShipObserver());
            pColPair.Attach(new BombObserver());
            pColPair.Attach(new SndObserver(sndEngine, pSndExplosion));

            // Bomb vs Missile
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Missile, pBombRoot, pMissileGroup);
            pColPair.Attach(new ShipRemoveMissileObserverBomb());

            pColPair.Attach(new BombObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SndObserver(sndEngine, pSndVader0));

            // Bomb vs Sheild
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Sheild, pBombRoot, pShieldRoot);
            pColPair.Attach(new BombObserver());
            pColPair.Attach(new RemoveBrickObserver());



            // Missile vs Shield
            pColPair = ColPairMan.Add(ColPair.Name.Misslie_Shield, pMissileGroup, pShieldRoot);
            pColPair.Attach(new ShipRemoveMissileObserver());
            pColPair.Attach(new RemoveBrickObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SndObserver(sndEngine, pSndVader0));

            // Bumper vs Ship
            pColPair = ColPairMan.Add(ColPair.Name.Bumper_Ship, pBumperRoot, pShipRoot);
            pColPair.Attach(new ShipMoveObserver());

            //ColPairMan.Add(ColPair.Name.Alien_Wall, pGrid, pWallGroup);
            //ColPairMan.Add(ColPair.Name.Bird_Missile, pMissileGroup, pGridB);

            /*for (int i = 1; i < 51; i++)
            {
                TimerEventMan.Add(TimerEvent.Name.Counter, new CounterEvent(), i * 0.5f);
            }
*/

        }

        private void CreateAlienSet()
        {
            AF.SetParent(pAlienRoot);

            pGrid = (Composite)AF.Create(GameObject.Name.AlienGrid);
            GameObject pColA;
            for (int i = 0; i < 11; i++)
            {
                AF.SetParent(pGrid);
                pColA = AF.Create(GameObject.Name.AlienColumn);
                AF.SetParent(pColA);
                AF.Create(GameObject.Name.Squid, 86.0f + i * 50.0f, 600.0f);
                AF.Create(GameObject.Name.Crab, 86.0f + i * 50.0f, 550);
                AF.Create(GameObject.Name.Crab, 86.0f + i * 50.0f, 500);
                AF.Create(GameObject.Name.Octopus, 86.0f + i * 50.0f, 450);
                AF.Create(GameObject.Name.Octopus, 86.0f + i * 50.0f, 400);
            }



            // LTN - AnimationCmd 
            AnimationCmd OctopusAnim = new AnimationCmd((AlienGrid)pGrid, SpriteGame.Name.Octopus);
            OctopusAnim.Attach(Image.Name.OctopusA);
            OctopusAnim.Attach(Image.Name.OctopusB);
            TimerEventMan.Add(TimerEvent.Name.Animation, OctopusAnim, 0.5f);


            // LTN - AnimationCmd
            AnimationCmd CrabAnim = new AnimationCmd((AlienGrid)pGrid, SpriteGame.Name.Crab);
            CrabAnim.Attach(Image.Name.CrabA);
            CrabAnim.Attach(Image.Name.CrabB);
            TimerEventMan.Add(TimerEvent.Name.Animation, CrabAnim, 0.5f);


            // LTN - AnimationCmd
            AnimationCmd SquidAnim = new AnimationCmd((AlienGrid)pGrid, SpriteGame.Name.Squid);
            SquidAnim.Attach(Image.Name.SquidA);
            SquidAnim.Attach(Image.Name.SquidB);
            TimerEventMan.Add(TimerEvent.Name.Animation, SquidAnim, 0.5f);


            // LTN - MoveCmd, sndEngine, pSndVader0, pSndVader1, pSndVader2, pSndVader3 
            MoveCmd pGridMove = new MoveCmd((AlienGrid)pGrid);
            TimerEventMan.Add(TimerEvent.Name.Move, pGridMove, 0.5f);


            //Bomb Spawn Command
            TimerEventMan.Add(TimerEvent.Name.BombSpawn, new BombSpawnEvent((AlienGrid)pGrid), 1.0f);
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

        public override void Update(float systemTime)
        {
            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Input
            InputMan.Update();

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

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_B))
            {
                SpriteBatchMan.toggleBoxesEnable();
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_U))
            {
                SpriteBatchMan.toggleBoxesDisable();
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_R))
            {
                GhostMan.Dump();
                
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_Z) == true && lastKeyZ == false)
            {
                ShipMan.ResetShipLives();
                ShipMan.RespawnShip();
                Debug.WriteLine("Respawned");
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_E) == true && lastKeyE == false)
            {
                GhostMan.Dump();
                Create4Sheild();
                GhostMan.Dump();

            }
            lastKeyE = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_E);
            lastKeyZ = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_Z);

            // Update lives
            Font pLivesFont = FontMan.Find(Font.Name.Lives);
            Debug.Assert(pLivesFont != null);
            pLivesFont.UpdateMessage((ShipMan.lives-1).ToString());
            
            // Update Score
            Font pScoreFont = FontMan.Find(Font.Name.Score);
            Debug.Assert(pScoreFont != null);
            if (score > highScore) highScore = score;
            pScoreFont.UpdateMessage(score.ToString());
            
            // Update High Score
            Font pHighScoreFont = FontMan.Find(Font.Name.HighScore);
            Debug.Assert(pHighScoreFont != null);

            pHighScoreFont.UpdateMessage(highScore.ToString());

            //Debug.WriteLine(pGrid.GetNumChildren());
            if(pGrid.GetNumChildren() <= 0)
            {
                CreateAlienSet();
                Create4Sheild();
            }


        }

        public static void Create4Sheild()
        {

            GameObject pShieldRoot = ShieldFactory.CreateSingleShield(91, 150);
            GameObject pShieldRoot2 = ShieldFactory.CreateSingleShield(232, 150);
            GameObject pShieldRoot3 = ShieldFactory.CreateSingleShield(373, 150);
            GameObject pShieldRoo4 = ShieldFactory.CreateSingleShield(514, 150);

            
        }

        bool lastKeyE = false;
        bool lastKeyZ = false;

        public override void Entering()
        {
            
            
            
            // update SpriteBatchMan()
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
            TimerEventMan.SetActive(this.poTimerEventMan);

            // Update timer since last pause
            float t0 = GlobalTimer.GetTime();
            float t1 = this.TimeAtPause;
            float delta = t0 - t1;
            InputMan.SetActive(true);
            TimerEventMan.PauseUpdate(delta);
            
            if(ShipMan.lives <= 0)
            {
                ShipMan.ResetShipLives();
                ShipMan.RespawnShip();
            }

            
           



        }
        public override void Leaving()
        {
            
            // Need a better way to do this
            InputMan.SetActive(false);
            if(score> highScore)
            {
                highScore = score;
            }
            GameObject p = (GameObject)pShieldRoot;
            if(p.GetNumChildren() <= 0)
            {
                Create4Sheild();

            }

            //Get all childs of aliens and remove

            this.TimeAtPause = GlobalTimer.GetTime();
        }

        public override void Draw()
        {
            // draw all objects
            SpriteBatchMan.Draw();
        }

        public override int GetSceneId()
        {
            return 1;
        }

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        public TimerEventMan poTimerEventMan;
    }
}