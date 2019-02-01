using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HideAndSeek.Sprites
{
  public class Door : Sprite
  {
    public Sprite ActualDoor { get; set; }

    private float _showDoorTimer;

    private List<Sprite> _enteredSprites = new List<Sprite>();

    public Door(Texture2D texture)
      : base(texture)
    {

    }

    public override void Update(GameTime gameTime)
    {
      ActualDoor.Position = this.Position;
      ActualDoor.Rotatation = this.Rotatation;

      if (ActualDoor.IsVisible)
      {
        _showDoorTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      if (_showDoorTimer > 2)
      {
        ActualDoor.IsVisible = false;
        _showDoorTimer = 0;
      }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      base.Draw(gameTime, spriteBatch);

      ActualDoor.Draw(gameTime, spriteBatch);
    }

    public override void OnEnter(Sprite sprite)
    {
      if (!_enteredSprites.Contains(sprite))
        _enteredSprites.Add(sprite);
    }

    public override void OnLeave(Sprite sprite)
    {
      if (_enteredSprites.Contains(sprite))
      {
        _enteredSprites.Remove(sprite);

        if (_enteredSprites.Count == 0)
        {
          ActualDoor.IsVisible = true;
        }
      }
    }
  }
}
