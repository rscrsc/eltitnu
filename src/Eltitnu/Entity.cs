using System;
using OpenTK.Mathematics;
namespace Eltitnu.Eltitnu
{
	public class Entity : GameObject
	{
		public Vector2 Position { get; set; }
		public string TexturePath { get; set; }
		public Entity(Vector2 position, string texturePath)
		{
			Position = position;
			TexturePath = texturePath;
		}
	}
}

