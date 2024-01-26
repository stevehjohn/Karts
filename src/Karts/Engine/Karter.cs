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

    private readonly Player _player;

    public Karter(Player player)
    {
        _player = player;
    }
    
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
        var delta = Math.Abs(_player.SteeringAngle);

        var offset = delta switch
        {
            < 0.005d => 0,
            < 0.01d => 1,
            < 0.015d => 2,
            _ => 3
        };

        var effect = _player.SteeringAngle < -0.005d
            ? SpriteEffects.FlipHorizontally
            : SpriteEffects.None;

        // ReSharper disable once PossibleLossOfFraction
        spriteBatch.Draw(
            _texture,
            new Vector2(Constants.BufferWidth / 2 - 32, (int) (Constants.BufferHeight * .85) + _offset),
            new Rectangle(offset * 33, 0, 31, 31), Color.White,
            0f,
            Vector2.Zero,
            new Vector2(2, 2),
            effect,
            0f);
    }
}