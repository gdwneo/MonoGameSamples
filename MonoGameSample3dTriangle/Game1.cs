// This samples draws a triangle in a 3D space

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameSample3dTriangle
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private BasicEffect _basicEffect;
        private VertexPositionColor[] _colors;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            _colors = new[]
            {
                new VertexPositionColor(new Vector3(0.0f, +0.5f, 0.0f), Color.Red),
                new VertexPositionColor(new Vector3(+0.5f, -0.5f, 0.0f), Color.Green),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.0f), Color.Blue)
            };

            _basicEffect = new BasicEffect(GraphicsDevice) {VertexColorEnabled = true};
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // rotate our triangle continuously
            var angle = MathHelper.ToRadians((float) gameTime.TotalGameTime.TotalMilliseconds/10.0f);
            _basicEffect.World = Matrix.CreateRotationY(angle);

            // look at it, step back a bit
            _basicEffect.View = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 2.0f), Vector3.Zero, Vector3.Up);

            // we need a projection too, here 45 degrees
            _basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                _graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100.0f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // to simplify here,
            // we want to see our triangle no matter where we look at it from
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach (var pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _colors, 0, _colors.Length/3);
            }
            base.Draw(gameTime);
        }
    }
}