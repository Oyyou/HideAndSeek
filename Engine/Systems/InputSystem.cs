using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Engine.Systems
{
  public class InputSystem : ISystem
  {
    public SystemTypes SystemType => SystemTypes.Update;

    public void Update(List<GameObject> gameObjects, GameTime gameTime)
    {

    }
  }
}
