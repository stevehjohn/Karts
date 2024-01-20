using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Karts.Engine;

public class Karts : Game
{
    private GraphicsDeviceManager _graphicsDeviceManager;
    
    private SpriteBatch _spriteBatch;
    
    public Karts()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1150,
            PreferredBackBufferHeight = 1150
        };

        Content.RootDirectory = "./_Content";
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        base.LoadContent();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }
}