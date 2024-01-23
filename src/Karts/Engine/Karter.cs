using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Karts.Engine;

public class Karter : IActor
{
    private Texture2D _texture;

    private int _offset;

    private readonly Random _rng = new();

    private int _frame;
    
    public void LoadContent(ContentManager contentManager)
    {
        _texture = contentManager.Load<Texture2D>("mario");
    }

    public void Update()
    {
        if (_frame % 4 == 0)
        {
            _offset = _rng.Next(2) * 2;
        }

        _frame++;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var delta = Math.Abs(MapRenderer.AngleDelta);

        int offset;
        
        if (delta < 0.005d)
        {
            offset = 0;
        }
        else if (delta < 0.01d)
        {
            offset = 1;
        }
        else if (delta < 0.015d)
        {
            offset = 2;
        }
        else
        {
            offset = 3;
        }

        var effect = MapRenderer.AngleDelta < -0.005d
            ? SpriteEffects.FlipHorizontally
            : SpriteEffects.None;

        // ReSharper disable once PossibleLossOfFraction
        spriteBatch.Draw(
            _texture,
            new Vector2(Constants.BufferWidth / 2 - 64, (int) (Constants.BufferHeight * .65) + _offset),
            new Rectangle(offset * 33, 0, 31, 31), Color.White,
            0f,
            Vector2.Zero,
            new Vector2(4, 4),
            effect,
            0f);
    }
}