using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Eltitnu.Common
{
    //public abstract class GameObject
    public class GameObject
    {
        public Vector4 position;

        // TODO: Add rotation information

        public GameObject(Vector4 position)
        {
            this.position = position;
        }
    }
}
