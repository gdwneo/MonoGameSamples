// This samples draws a 3d cube using counter clockwise indices

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameSample3dCube
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private BasicEffect _basicEffect;
        private VertexPositionColor[] _colors;
        private int[] _indices;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            _colors = new[]
            {
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f), Color.Black),
                new VertexPositionColor(new Vector3(+0.5f, -0.5f, -0.5f), Color.Red),
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, -0.5f), Color.Green),
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, -0.5f), Color.Yellow),
                //
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, +0.5f), Color.Purple),
                new VertexPositionColor(new Vector3(+0.5f, -0.5f, +0.5f), Color.Magenta),
                new VertexPositionColor(new Vector3(-0.5f, +0.5f, +0.5f), Color.Blue),
                new VertexPositionColor(new Vector3(+0.5f, +0.5f, +0.5f), Color.White)
            };

            // CW, front-facing
            _indices = new[]
            {
                // front
                0, 2, 3,
                0, 3, 1,
                // top
                2, 6, 7,
                2, 7, 3,
                // right
                1, 3, 7,
                1, 7, 5,
                // left
                4, 6, 2,
                4, 2, 0,
                // bottom
                1, 5, 4,
                1, 4, 0,
                // back
                5, 7, 6,
                5, 6, 4
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
            var angle = MathHelper.ToRadians((float) gameTime.TotalGameTime.TotalMilliseconds/10.0f);
            _basicEffect.World =
                Matrix.CreateRotationX(angle)*
                Matrix.CreateRotationY(angle)*
                Matrix.CreateRotationZ(angle);
            _basicEffect.View = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 2.0f), Vector3.Zero, Vector3.Up);
            _basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                _graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100.0f);

            /* NOTE : since above we took care to define our indices for triangles
             * in a clockwise fashion, we did not use None as in the previous sample
             * try to uncomment the 2nd state to see how render gets affected */
            GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
            //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (var pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _colors, 0, _colors.Length,
                    _indices, 0, _indices.Length/3);
            }
            base.Draw(gameTime);
        }
    }
}