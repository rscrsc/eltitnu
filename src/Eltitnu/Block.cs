using System;
using OpenTK.Mathematics;
namespace Eltitnu.Eltitnu
{
	public class Block : Entity
	{
		public Block(Vector2 position, string texturePath) : base(position, texturePath)
		{
			Position = position;
			TexturePath = texturePath;
		}
	}
}

