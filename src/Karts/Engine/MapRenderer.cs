using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Karts.Engine;

public class MapRenderer
{
    private readonly Color[] _buffer;

    private Color[] _map;

    private (double X, double Y) _position = (1_000d, 1_000d);

    private double _angle = 0d;

    private double _near = 0.005d;

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

        for (var y = 0; y < Constants.BufferHeight / 2; y++)
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

                _buffer[GetBufferPosition(x, y + Constants.BufferHeight / 2)] = _map[GetMapPosition(sampleX, sampleY)];
            }
        }
    }

    private int GetBufferPosition(int x, int y)
    {
        return y * Constants.BufferWidth + x;
    }

    private int GetMapPosition(double x, double y)
    {
        return (int) (y * Constants.MapSize * Constants.MapSize + x * Constants.MapSize);
    }
}