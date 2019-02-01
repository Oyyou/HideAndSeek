using Penumbra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek
{
  public static class Helpers
  {
    public static bool IsSame(this Hull a, Hull b)
    {
      return a.Position == b.Position;// &&
        //a.Points == b.Points;
    }
  }
}
