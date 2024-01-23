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

    private (double X, double Y) _position = (1_000.13d, 1_000.54d);

    private double _angle = -Math.PI / 2;

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
        var map = contentManager.Load<Texture2D>("map");

        _map = new Color[Constants.MapSize * Constants.MapSize];
        
        map.GetData(_map);
    }

    public void Update()
    {
        var state = Keyboard.GetState();
        
        if (state.IsKeyDown(Keys.P))
        {
            _angle += 0.02d;
        }

        if (state.IsKeyDown(Keys.O))
        {
            _angle -= 0.02d;
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
                _speed -= 0.00002d;
            }
        }
        else if (_speed > 0)
        {
            _speed -= 0.00001d;
        }

        _position.X += Math.Cos(_angle) * _speed;
        _position.Y += Math.Sin(_angle) * _speed;
    }

    public void Draw()
    {
        var farLeftX = _position.X + Math.Cos(_angle - FovHalf) * Far;
        var farLeftY = _position.Y + Math.Sin(_angle - FovHalf) * Far;
        
        var nearLeftX = _position.X + Math.Cos(_angle - FovHalf) * Near;
        var nearLeftY = _position.Y + Math.Sin(_angle - FovHalf) * Near;
        
        var farRightX = _position.X + Math.Cos(_angle + FovHalf) * Far;
        var farRightY = _position.Y + Math.Sin(_angle + FovHalf) * Far;
        
        var nearRightX = _position.X + Math.Cos(_angle + FovHalf) * Near;
        var nearRightY = _position.Y + Math.Sin(_angle + FovHalf) * Near;

        for (var y = 0; y < Constants.BufferHeight * 0.75d; y++)
        {
            var sampleDepth = y / (Constants.BufferHeight / 2.0d);

            var startX = (farLeftX - nearLeftX) / sampleDepth + nearLeftX;
            var startY = (farLeftY - nearLeftY) / sampleDepth + nearLeftY;
            
            var endX = (farRightX - nearRightX) / sampleDepth + nearRightX;
            var endY = (farRightY - nearRightY) / sampleDepth + nearRightY;

            for (var x = 0; x < Constants.BufferWidth; x++)
            {
                var sampleWidth = x / (double) Constants.BufferWidth;

                var sampleX = ((endX - startX) * sampleWidth + startX) % 1.0d;
                var sampleY = ((endY - startY) * sampleWidth + startY) % 1.0d;

                var pixel = _map[GetMapPosition((int) (sampleX * Constants.MapSize), (int) (sampleY * Constants.MapSize))];

                _buffer[GetBufferPosition(x, (int) (y + Constants.BufferHeight * 0.25))] = pixel;
            }
        }
    }

    private int GetBufferPosition(int x, int y)
    {
        return y * Constants.BufferWidth + x;
    }

    private int GetMapPosition(int x, int y)
    {
        return x + y * Constants.MapSize;
    }
}