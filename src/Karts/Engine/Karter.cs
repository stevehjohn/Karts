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

    public void Draw()
    {
    }
}