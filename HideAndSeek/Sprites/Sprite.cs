using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek.Sprites
{
  public class Sprite
  {
    protected Vector2 _velocity;

    public Color Colour = Color.White;

    public Vector2 Position;

    public readonly Texture2D Texture;

    public bool IsVisible { get; set; } = true;

    public Rectangle FutureRectangle
    {
      get
      {
        return new Rectangle((int)(Rectangle.X + _velocity.X), (int)(Rectangle.Y + _velocity.Y), Texture.Width, Texture.Height);
      }
    }

    public Rectangle Rectangle
    {
      get
      {
        return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, Texture.Width, Texture.Height);
      }
    }

    public float Rotatation { get; set; } = 0;

    public Vector2 Origin
    {
      get
      {
        return new Vector2(Texture.Width / 2, Texture.Height / 2);
      }
    }

    public Sprite(Texture2D texture)
    {
      Texture = texture;
    }

    public virtual void Update(GameTime gameTime)
    {

    }

    public virtual void PostUpdate(GameTime gameTime)
    {

    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      if (!IsVisible)
        return;

      spriteBatch.Draw(Texture, Position, null, Colour, Rotatation, Origin, 1f, SpriteEffects.None, 0f);
    }

    public virtual void OnCollide(Sprite sprite)
    {

    }

    public virtual void OnEnter(Sprite sprite)
    {

    }

    public virtual void OnLeave(Sprite sprite)
    {

    }
  }
}
