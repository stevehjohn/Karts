using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Karts.Engine;

public class Karter : IActor
{
    private Texture2D _texture;

    public void LoadContent(ContentManager contentManager)
    {
        _texture = contentManager.Load<Texture2D>("mario");
    }

    public void Update()
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // ReSharper disable once PossibleLossOfFraction
        spriteBatch.Draw(
            _texture,
            new Vector2(Constants.BufferWidth / 2 - 64, (int) (Constants.BufferHeight * .65)),
            new Rectangle(0, 0, 31, 31), Color.White,
            0f,
            Vector2.Zero,
            new Vector2(4, 4),
            SpriteEffects.None,
            0f);
    }
}