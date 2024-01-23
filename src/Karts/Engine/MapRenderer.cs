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

    private double _near = 0.0005d;

    private double _far = 0.03d;

    private double _fovHalf = Math.PI / 4.0d;
    
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

        if (state.IsKeyDown(Keys.D1))
        {
            _fovHalf += 0.005d;
        }

        if (state.IsKeyDown(Keys.D2))
        {
            _fovHalf -= 0.005d;
        }

        if (state.IsKeyDown(Keys.D3))
        {
            _near += 0.0005d;
        }

        if (state.IsKeyDown(Keys.D4))
        {
            _near -= 0.0005d;
        }

        if (state.IsKeyDown(Keys.D5))
        {
            _far += 0.0005d;
        }

        if (state.IsKeyDown(Keys.D6))
        {
            _far -= 0.0005d;
        }
        
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
            _position.X += Math.Cos(_angle) * 0.002d;
            _position.Y += Math.Sin(_angle) * 0.002d;
        }

        if (state.IsKeyDown(Keys.A))
        {
            _position.X -= Math.Cos(_angle) * 0.002d;
            _position.Y -= Math.Sin(_angle) * 0.002d;
        }
        
        Console.WriteLine($"{_position.X}, {_position.Y}");
    }

    public void Draw()
    {
        var farLeftX = _position.X + Math.Cos(_angle - _fovHalf) * _far;
        var farLeftY = _position.Y + Math.Sin(_angle - _fovHalf) * _far;
        
        var nearLeftX = _position.X + Math.Cos(_angle - _fovHalf) * _near;
        var nearLeftY = _position.Y + Math.Sin(_angle - _fovHalf) * _near;
        
        var farRightX = _position.X + Math.Cos(_angle + _fovHalf) * _far;
        var farRightY = _position.Y + Math.Sin(_angle + _fovHalf) * _far;
        
        var nearRightX = _position.X + Math.Cos(_angle + _fovHalf) * _near;
        var nearRightY = _position.Y + Math.Sin(_angle + _fovHalf) * _near;

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