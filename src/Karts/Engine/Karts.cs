using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Karts.Engine;

public class Karts : Game
{
    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphicsDeviceManager;

    private SpriteBatch _spriteBatch;

    private readonly Color[] _buffer = new Color[Constants.BufferWidth * Constants.BufferHeight];

    private Texture2D _bufferTexture;

    public Karts()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = Constants.BufferWidth,
            PreferredBackBufferHeight = Constants.BufferHeight
        };

        Content.RootDirectory = "./_Content";
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _bufferTexture = new Texture2D(GraphicsDevice, Constants.BufferWidth, Constants.BufferHeight);
        
        base.LoadContent();
    }

    protected override void Initialize()
    {
        IsMouseVisible = false;
        
        base.Initialize();
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        
        _bufferTexture.SetData(_buffer);
        
        _spriteBatch.Draw(_bufferTexture, new Vector2(0, 0), new Rectangle(0, 0, Constants.BufferWidth, Constants.BufferHeight), Color.White);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}