// This samples draws a textured rectangle

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameSample3dTexturing
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private BasicEffect _basicEffect;
        private Texture2D _texture2D;
        private VertexPositionColorTexture[] _vertices;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            // a textured item can also be tinted, see in Draw()
            _vertices = new[]
            {
                new VertexPositionColorTexture(new Vector3(-0.5f, -0.5f, 0.0f), Color.Cyan, new Vector2(0.0f, 1.0f)),
                new VertexPositionColorTexture(new Vector3(-0.5f, +0.5f, 0.0f), Color.Magenta, new Vector2(0.0f, 0.0f)),
                new VertexPositionColorTexture(new Vector3(+0.5f, +0.5f, 0.0f), Color.Yellow, new Vector2(1.0f, 0.0f)),
                //
                new VertexPositionColorTexture(new Vector3(-0.5f, -0.5f, 0.0f), Color.Cyan, new Vector2(0.0f, 1.0f)),
                new VertexPositionColorTexture(new Vector3(+0.5f, +0.5f, 0.0f), Color.Yellow, new Vector2(1.0f, 0.0f)),
                new VertexPositionColorTexture(new Vector3(+0.5f, -0.5f, 0.0f), Color.Black, new Vector2(1.0f, 1.0f))
            };
            _texture2D = Content.Load<Texture2D>("texture.png");
            _basicEffect = new BasicEffect(GraphicsDevice) {VertexColorEnabled = true, TextureEnabled = true};
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var angle = MathHelper.ToRadians((float) gameTime.TotalGameTime.TotalMilliseconds/10.0f);
            _basicEffect.World = Matrix.CreateRotationY(angle);
            _basicEffect.View = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 2.0f), Vector3.Zero, Vector3.Up);
            _basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                _graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100.0f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _basicEffect.Texture = _texture2D; // we need to bind our texture
            // try uncomment this, the rectangle won't be tinted anymore
            // _basicEffect.VertexColorEnabled = false;

            foreach (var pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertices, 0, _vertices.Length/3);
            }
            base.Draw(gameTime);
        }
    }
}