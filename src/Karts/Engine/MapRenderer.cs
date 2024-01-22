using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Karts.Engine;

public class MapRenderer
{
    private readonly Color[] _buffer;

    private Color[] _map;

    private (float X, float Y) _position = (1_000f, 1_000f);

    private float _angle = 0f;
    
    public MapRenderer(Color[] buffer)
    {
        _buffer = buffer;
    }

    public void LoadContent(ContentManager contentManager)
    {
        var map = contentManager.Load<Texture2D>("map");

        map.GetData(_map);
    }

    public void Draw()
    {
    }

    private int GetBufferPosition(int x, int y)
    {
        return y * Constants.BufferWidth + x;
    }

    private int GetMapPosition(int x, int y)
    {
        return y * Constants.MapSize + x;
    }
}