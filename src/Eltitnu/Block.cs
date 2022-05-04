using System;
using OpenTK.Mathematics;
using Eltitnu.Common;
namespace Eltitnu.Eltitnu
{
    public static class BlockData
    {
        public static readonly int TextureAtlasSizeByBlock = 2;
        public enum Faces
        {
            Top = 0,
            Bottom = 1,
            Left = 2,
            Right = 3,
            Front = 4,
            Back = 5,
        };
        public readonly static Block[] MeshData = new Block[1]
        {
            new Block(new Vector4(0, 0, 0, 0), new Face[6]
            {
                new Face(BlockData.Faces.Top, 1),
                new Face(BlockData.Faces.Bottom, 0),
                new Face(BlockData.Faces.Left, 0),
                new Face(BlockData.Faces.Right, 0),
                new Face(BlockData.Faces.Front, 0),
                new Face(BlockData.Faces.Back, 0)
            })
        };

        public static Vector2 GetFaceTexCoord(int textureIndex)
        {
            int row = textureIndex / TextureAtlasSizeByBlock, column = textureIndex % TextureAtlasSizeByBlock;
            int reversedRow = TextureAtlasSizeByBlock - 1 - row, reversedColumn = TextureAtlasSizeByBlock - 1 - column;
            return new Vector2(1f / TextureAtlasSizeByBlock * reversedColumn, 1f / TextureAtlasSizeByBlock * reversedRow);
        }
    }
    public class Face
    {
        internal BlockData.Faces type;
        internal int textureIndex;

        internal Face(BlockData.Faces type, int textureIndex)
        {
            this.type = type;
            this.textureIndex = textureIndex;
        }
        public Vector4 GetPositionOffset()
        {
            switch (type)
            {
                case BlockData.Faces.Top: 
                    return new Vector4(0, 0.5f, 0, 0);
                case BlockData.Faces.Bottom: 
                    return new Vector4(0, -0.5f, 0, 0);
                case BlockData.Faces.Left: 
                    return new Vector4(-0.5f, 0, 0, 0);
                case BlockData.Faces.Right:
                    return new Vector4(0.5f, 0, 0, 0);
                case BlockData.Faces.Front:
                    return new Vector4(0, 0, 0.5f, 0);
                case BlockData.Faces.Back:
                    return new Vector4(0, 0, -0.5f, 0);
                default:
                    throw new Exception("Wrong Type Value");
            }
        }
        public static void Parse()
        {

        }
    }
    public class Block : GameObject
    {
        public Face[] faces = new Face[6];
        public Block(Vector4 position, Face[] faces) : base(position)
        {
            base.position = position;
            this.faces = faces;
        }

    }
}