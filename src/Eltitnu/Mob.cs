using System;
using OpenTK.Mathematics;
namespace Eltitnu.Eltitnu
{
	public class Mob : Entity
	{
		public Mob(Vector2 position, string texturePath) : base(position, texturePath)
		{
			Position = position;
			TexturePath = texturePath;
		}
	}
}

