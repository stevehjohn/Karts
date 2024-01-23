using Microsoft.Xna.Framework.Content;

namespace Karts.Engine;

public interface IActor
{
    void LoadContent(ContentManager contentManager);

    void Update();
    
    void Draw();
}