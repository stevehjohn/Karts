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

    private readonly List<IActor> _actors = new();

    private MapRenderer _mapRenderer;

    private Player _player;

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

        _player = new Player();
        
        _mapRenderer = new MapRenderer(_buffer, _player);
        
        _actors.Add(new Karter(_player));
        
        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _bufferTexture = new Texture2D(GraphicsDevice, Constants.BufferWidth, Constants.BufferHeight);

        _mapRenderer.LoadContent(Content);
        
        foreach (var actor in _actors)
        {
            actor.LoadContent(Content);
        }
        
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _player.Update();
        
        foreach (var actor in _actors)
        {
            actor.Update();
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

        _mapRenderer.Draw();
        
        _bufferTexture.SetData(_buffer);
        
        _spriteBatch.Draw(_bufferTexture, new Vector2(0, 0), new Rectangle(0, 0, Constants.BufferWidth, Constants.BufferHeight), Color.White);
        
        foreach (var actor in _actors)
        {
            actor.Draw(_spriteBatch);
        }

        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}