using System.Collections.Generic;
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

    private readonly List<IActor> _actors = [];

    public Karts()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = Constants.BufferWidth,
            PreferredBackBufferHeight = Constants.BufferHeight
        };

        Content.RootDirectory = "./_Content";
    }

    protected override void Initialize()
    {
        IsMouseVisible = false;
        
        _actors.Add(new MapRenderer(_buffer));
        
        _actors.Add(new Karter());
        
        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _bufferTexture = new Texture2D(GraphicsDevice, Constants.BufferWidth, Constants.BufferHeight);

        foreach (var actor in _actors)
        {
            actor.LoadContent(Content);
        }
        
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        foreach (var actor in _actors)
        {
            actor.Update();
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();

        foreach (var actor in _actors)
        {
            actor.Draw();
        }
        
        _bufferTexture.SetData(_buffer);
        
        _spriteBatch.Draw(_bufferTexture, new Vector2(0, 0), new Rectangle(0, 0, Constants.BufferWidth, Constants.BufferHeight), Color.White);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}