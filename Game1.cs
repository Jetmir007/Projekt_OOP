using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projekt_OOP
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _g;
        SpriteBatch _sb;
        SpriteFont font;

        GameState _state = GameState.CharacterSelect;
        KeyboardState _prev;

        CharacterType p1Choice = CharacterType.Boxer;
        CharacterType p2Choice = CharacterType.Ninja;

        CharacterBase p1, p2;
        IPlayerInput input1, input2;

        Texture2D boxerTex, ninjaTex, samuraiTex;

        Rectangle boxerRect = new Rectangle(200, 300, 128, 128);
        Rectangle ninjaRect = new Rectangle(400, 300, 128, 128);
        Rectangle samuraiRect = new Rectangle(600, 300, 128, 128);
        Rectangle boxerRect2 = new Rectangle(1000, 300, 128, 128);
        Rectangle ninjaRect2 = new Rectangle(1200, 300, 128, 128);
        Rectangle samuraiRect2 = new Rectangle(1400, 300, 128, 128);



        public Game1()
        {
            _g = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _g.PreferredBackBufferWidth = 1800;
            _g.PreferredBackBufferHeight = 900;
        }

        protected override void LoadContent()
        {
            _sb = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");

            boxerTex = Content.Load<Texture2D>("boxer(1)");
            ninjaTex = Content.Load<Texture2D>("ninja");
            samuraiTex = Content.Load<Texture2D>("sword");

            input1 = new KeyboardInput(Keys.A, Keys.D, Keys.W, Keys.F, Keys.G);
            input2 = new KeyboardInput(Keys.Left, Keys.Right, Keys.Up, Keys.NumPad1, Keys.NumPad2);
        }

        protected override void Update(GameTime gameTime)
        {
            var cur = Keyboard.GetState();

            if (cur.IsKeyDown(Keys.Escape))
                Exit();

            switch (_state)
            {
                case GameState.CharacterSelect:
                    if (cur.IsKeyDown(Keys.A)) p1Choice = CharacterType.Boxer;
                    if (cur.IsKeyDown(Keys.S)) p1Choice = CharacterType.Ninja;
                    if (cur.IsKeyDown(Keys.D)) p1Choice = CharacterType.Samurai;

                    if (cur.IsKeyDown(Keys.Left)) p2Choice = CharacterType.Boxer;
                    if (cur.IsKeyDown(Keys.Up)) p2Choice = CharacterType.Ninja;
                    if (cur.IsKeyDown(Keys.Right)) p2Choice = CharacterType.Samurai;

                    if (cur.IsKeyDown(Keys.Enter))
                        StartGame();
                    break;

                case GameState.InGame:
                    p1.HandleInput(input1, cur, _prev, p2);
                    p2.HandleInput(input2, cur, _prev, p1);

                    p1.Update(gameTime);
                    p2.Update(gameTime);

                    if (p1.Hp <= 0 || p2.Hp <= 0)
                        _state = GameState.Results;
                    break;

                case GameState.Results:
                    if (cur.IsKeyDown(Keys.Enter))
                        _state = GameState.CharacterSelect;
                    break;
            }

            _prev = cur;
            base.Update(gameTime);
        }

        void StartGame()
        {
            p1 = p1Choice switch
            {
                CharacterType.Boxer => new Boxer(boxerTex),
                CharacterType.Ninja => new Ninja(ninjaTex),
                _ => new Samurai(samuraiTex)
            };

            p2 = p2Choice switch
            {
                CharacterType.Boxer => new Boxer(boxerTex),
                CharacterType.Ninja => new Ninja(ninjaTex),
                _ => new Samurai(samuraiTex)
            };

            p1.Position = new Vector2(200, 600);
            p2.Position = new Vector2(1500, 600);

            _state = GameState.InGame;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _sb.Begin();

            if (_state == GameState.CharacterSelect)
            {
                _sb.Draw(boxerTex, boxerRect, Color.White);
                _sb.DrawString(font, "A", new Vector2(boxerRect.X + 40, boxerRect.Y + 200), Color.Black);
                _sb.DrawString(font, "Left", new Vector2(boxerRect2.X + 40, boxerRect2.Y + 200), Color.Black);
                _sb.DrawString(font, "S", new Vector2(ninjaRect.X + 40, ninjaRect.Y + 200), Color.Black);
                _sb.DrawString(font, "Up", new Vector2(ninjaRect2.X + 40, ninjaRect2.Y + 200), Color.Black);
                _sb.DrawString(font, "D", new Vector2(samuraiRect.X + 40, samuraiRect.Y + 200), Color.Black);
                _sb.DrawString(font, "Right", new Vector2(samuraiRect2.X + 40, samuraiRect2.Y + 200), Color.Black);
                _sb.Draw(ninjaTex, ninjaRect, Color.White);
                _sb.Draw(samuraiTex, samuraiRect, Color.White);

                _sb.Draw(boxerTex, boxerRect2, Color.White);
                _sb.Draw(ninjaTex, ninjaRect2, Color.White);
                _sb.Draw(samuraiTex, samuraiRect2, Color.White);

                Rectangle selected = p1Choice switch
                {
                    CharacterType.Boxer => boxerRect,
                    CharacterType.Ninja => ninjaRect,
                    _ => samuraiRect
                };

                Rectangle selected2 = p2Choice switch
                {
                    CharacterType.Boxer => boxerRect,
                    CharacterType.Ninja => ninjaRect,
                    _ => samuraiRect
                };
            }


            if (_state == GameState.InGame)
            {
                p1.Draw(_sb);
                p2.Draw(_sb);
            }

            _sb.End();
            base.Draw(gameTime);
        }
    }
}
