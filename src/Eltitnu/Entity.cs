using System;
using OpenTK.Mathematics;
using Eltitnu.Common;

namespace Eltitnu.Eltitnu
{
    public class Entity : GameObject
    {
        public Entity(Vector4 position) : base(position)
        {
            this.position = position;
        }
    }
}

