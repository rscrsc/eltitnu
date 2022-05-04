using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Eltitnu.Common
{
    public abstract class GameObject
    {
        public Vector4 position;

        public GameObject(Vector4 position)
        {
            this.position = position;
        }
    }
}
