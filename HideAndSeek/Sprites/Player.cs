using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HideAndSeek.Sprites
{
  public class Player : Sprite
  {
    public Player(Texture2D texture) :
      base(texture)
    {
    }

    public override void Update(GameTime gameTime)
    {
      _velocity = Vector2.Zero;

      var speed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 5f;

      if (Keyboard.GetState().IsKeyDown(Keys.W))
        _velocity.Y = -speed;
      else if (Keyboard.GetState().IsKeyDown(Keys.S))
        _velocity.Y = speed;

      if (Keyboard.GetState().IsKeyDown(Keys.A))
        _velocity.X = -speed;
      else if (Keyboard.GetState().IsKeyDown(Keys.D))
        _velocity.X = speed;
    }

    public override void PostUpdate(GameTime gameTime)
    {
      Position += _velocity;
    }

    public override void OnCollide(Sprite sprite)
    {
      if (sprite is Door)
      {
        var door = sprite as Door;
        
        if (door.ActualDoor.IsVisible)
          _velocity = Vector2.Zero;
      }
      else
      {
        _velocity = Vector2.Zero;
      }
    }
  }
}
