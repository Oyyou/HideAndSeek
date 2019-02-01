using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Systems
{
  public enum SystemTypes
  {
    Update,
    Draw,
  }

  public interface ISystem
  {
    SystemTypes SystemType { get; }

    void Update(List<GameObject> gameObjects, GameTime gameTime);
  }
}
