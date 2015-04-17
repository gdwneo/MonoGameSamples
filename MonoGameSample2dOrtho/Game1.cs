// This sample shows how to draw 2D lines

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameSample2dOrtho
{
    public class Game1 : Game
    {
        private BasicEffect _basicEffect;
        private VertexPositionColor[] _colors;
        private GraphicsDeviceManager _graphics;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            _colors = new[]
            {
                new VertexPositionColor(new Vector3(0.0f, 0.0f, 0.0f), Color.Red),
                new VertexPositionColor(new Vector3(100.0f, 0.0f, 0.0f), Color.Green),
                new VertexPositionColor(new Vector3(100.0f, 100.0f, 0.0f), Color.Blue),
                new VertexPositionColor(new Vector3(0.0f, 100.0f, 0.0f), Color.Yellow),
                new VertexPositionColor(new Vector3(0.0f, 0.0f, 0.0f), Color.Red)
            };
            _basicEffect = new BasicEffect(GraphicsDevice) {VertexColorEnabled = true};
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _basicEffect.World = // NOTE: the correct order for matrices is SRT (scale, rotate, translate)
                Matrix.CreateTranslation(0.5f, 0.5f, 0.0f)* // offset by half pixel to get pixel perfect rendering
                Matrix.CreateTranslation(100.0f, 100.0f, 0.0f)* // draw at 100, 100
                Matrix.CreateOrthographicOffCenter // 2d projection
                    (
                        0.0f,
                        GraphicsDevice.Viewport.Width, // NOTE : here not an X-coordinate (i.e. width - 1)
                        GraphicsDevice.Viewport.Height,
                        0.0f,
                        0.0f,
                        1.0f
                    );

            var passes = _basicEffect.CurrentTechnique.Passes;
            foreach (var pass in passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, _colors, 0, _colors.Length - 1);
            }
            base.Draw(gameTime);
        }
    }
}