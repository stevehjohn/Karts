using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Karts.Engine;

public class MapRenderer
{
    private readonly Color[] _buffer;

    private Color[] _map;

    private const double Near = 0.0005d;

    private const double Far = 0.12d;

    private const double FovHalf = Math.PI / 4.0d;

    private readonly Player _player;

    public MapRenderer(Color[] buffer, Player player)
    {
        _buffer = buffer;

        _player = player;
    }

    public void LoadContent(ContentManager contentManager)
    {
        var map = contentManager.Load<Texture2D>("map-1");

        _map = new Color[Constants.MapSize * Constants.MapSize];
        
        map.GetData(_map);
    }

    public void Update()
    {
    }

    public void Draw()
    {
        Array.Fill(_buffer, Color.Black);

        var farLeft = _player.Position.MoveBy(_player.Direction - FovHalf, Far);

        var nearLeft = _player.Position.MoveBy(_player.Direction - FovHalf, Near);

        var farRight = _player.Position.MoveBy(_player.Direction + FovHalf, Far);

        var nearRight = _player.Position.MoveBy(_player.Direction + FovHalf, Near);
        
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