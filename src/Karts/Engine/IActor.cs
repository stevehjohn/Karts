using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Karts.Engine;

public interface IActor
{
    void LoadContent(ContentManager contentManager);

    void Update();
    
    void Draw(SpriteBatch spriteBatch);
}