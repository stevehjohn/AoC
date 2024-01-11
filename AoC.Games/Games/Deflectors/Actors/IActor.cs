using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors.Actors;

public interface IActor
{
    void LoadContent(ContentManager contentManager);

    void Update();

    public void Draw(SpriteBatch spriteBatch);
}