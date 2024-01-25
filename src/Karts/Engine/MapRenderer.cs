using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Karts.Engine;

public class MapRenderer
{
    private readonly Color[] _buffer;

    private Color[] _map;

    private Point2D _position = new(0.11d, 0.56d);
    
    private double _angle = -Math.PI / 2;

    // TODO: Urgh.
    public static double AngleDelta;

    private double _speed;

    private const double Near = 0.0005d;

    private const double Far = 0.03d;

    private const double FovHalf = Math.PI / 4.0d;

    public MapRenderer(Color[] buffer)
    {
        _buffer = buffer;
    }

    public void LoadContent(ContentManager contentManager)
    {
        var map = contentManager.Load<Texture2D>("map-2");

        _map = new Color[Constants.MapSize * Constants.MapSize];
        
        map.GetData(_map);
    }

    public void Update()
    {
        var state = Keyboard.GetState();
        
        if (state.IsKeyDown(Keys.P))
        {
            if (AngleDelta < 0.02d)
            {
                AngleDelta += 0.0005d;
            }
        }
        else if (state.IsKeyDown(Keys.O))
        {
            if (AngleDelta > -0.02d)
            {
                AngleDelta -= 0.0005d;
            }
        }
        else
        {
            switch (AngleDelta)
            {
                case > 0:
                    AngleDelta -= 0.001d;
                    break;
                case < 0:
                    AngleDelta += 0.001d;
                    break;
            }
        }

        if (state.IsKeyDown(Keys.Q))
        {
            if (_speed < 0.004d)
            {
                _speed += 0.00001d;
            }
        }
        else if (state.IsKeyDown(Keys.A))
        {
            if (_speed > 0)
            {
                _speed -= 0.00004d;
            }
        }
        else if (_speed > 0)
        {
            _speed -= 0.00001d;
        }

        _position = _position.MoveBy(_angle, _speed);

        if (_speed > 0)
        {
            _angle += AngleDelta;
        }
    }

    public void Draw()
    {
        Array.Fill(_buffer, Color.Black);

        var farLeft = _position.MoveBy(_angle - FovHalf, Far);

        var nearLeft = _position.MoveBy(_angle - FovHalf, Near);

        var farRight = _position.MoveBy(_angle + FovHalf, Far);

        var nearRight = _position.MoveBy(_angle + FovHalf, Near);
        
        for (var y = 1; y < Constants.BufferHeight * 0.75d; y++)
        {
            var sampleDepth = y / (Constants.BufferHeight / 2.0d);

            var start = (farLeft - nearLeft) / sampleDepth + nearLeft;

            var end = (farRight - nearRight) / sampleDepth + nearRight;
            
            for (var x = 0; x < Constants.BufferWidth; x++)
            {
                var sampleWidth = x / (double) Constants.BufferWidth;

                var sample = (end - start) * sampleWidth + start;
                
                var mapPosition = GetMapPosition((int) (sample.X * Constants.MapSize), (int) (sample.Y * Constants.MapSize));

                if (mapPosition != null)
                {
                    var pixel = _map[mapPosition.Value];

                    _buffer[GetBufferPosition(x, (int) (y + Constants.BufferHeight * 0.25))] = pixel;
                }
                else
                {
                    _buffer[GetBufferPosition(x, (int) (y + Constants.BufferHeight * 0.25))] = Color.Black;
                }
            }
        }
    }

    private static int GetBufferPosition(int x, int y)
    {
        return y * Constants.BufferWidth + x;
    }

    private static int? GetMapPosition(int x, int y)
    {
        if (x < 0 || x >= Constants.MapSize || y < 0 || y >= Constants.MapSize)
        {
            return null;
        }
        
        return x + y * Constants.MapSize;
    }
}