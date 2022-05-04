using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eltitnu.Common;
using OpenTK.Mathematics;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace Eltitnu.Eltitnu
{
    public static class ChunkData
    {
        public static readonly int ChunkWidthA = 5, ChunkWidthB = 5, ChunkWidthC = 5;
    }
    public class Chunk
    {
        public Vector4 position;
        public Chunk(Vector4 position)
        {
            this.position = position;
        }
        public ObjectRenderData PrepareRenderData()
        {
            for (int c = 0; c < ChunkData.ChunkWidthC; c++)
            {
                for(int a = 0; a < ChunkData.ChunkWidthA; a++)
                {
                    for(var b = 0; b < ChunkData.ChunkWidthB; b++)
                    {

                    }
                }
            }

            ObjectRenderData renderData = new();
            renderData._vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(renderData._vertexArrayObject);

            renderData._model.vertexBuffer = new BufferArray("TO_ARRAY");
            // fill in model data here
            float[] vertexBuffer = renderData._model.Generate();

            renderData._vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, renderData._vertexBufferObject);
            GL.BufferData(
                BufferTargetARB.ArrayBuffer,
                vertexBuffer.Length * sizeof(float),
                Marshal.UnsafeAddrOfPinnedArrayElement(vertexBuffer, 0),
                BufferUsageARB.StaticDraw
            );

            //_elementBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, _elementBufferObject);
            //GL.BufferData(
            //    BufferTargetARB.ElementArrayBuffer,
            //    _indices.Length * sizeof(uint),
            //    Marshal.UnsafeAddrOfPinnedArrayElement(_indices, 0),
            //    BufferUsageARB.StaticDraw
            //);

            renderData._shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            renderData._shader.Use();

            uint vertexLocation = renderData._shader.GetAttribLocation("inPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            uint normalLocation = renderData._shader.GetAttribLocation("inNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            uint texCoordLocation = renderData._shader.GetAttribLocation("inTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            renderData._texture = Texture.LoadFromFile("Resources/atlas.png");
            renderData._texture.Use(TextureUnit.Texture0);

            // Set texture filter
            GL.TextureParameteri(renderData._texture.Handle, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TextureParameteri(renderData._texture.Handle, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            renderData._shader.SetInt("texture0", 0);

            return renderData;
        }
    }
}
