﻿using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;
using System.Linq;

namespace Eltitnu.Common
{
    public class ObjectRenderData
    {
        //public BufferHandle _elementBufferObject;

        public BufferHandle _vertexBufferObject;

        public VertexArrayHandle _vertexArrayObject;

        public ModelData _model;

        public Shader _shader;

        public Texture _texture;
    }

    internal class PreparedRenderPair
    {
        internal ObjectRenderData _data;
        internal List<GameObject> _objects;

        internal PreparedRenderPair(ObjectRenderData data, List<GameObject> objects)
        {
            _data = data;
            _objects = objects;
        }

    }
 
    public class Renderer
    {
        private Dictionary<TexturedModel, List<GameObject>> _objects = World.Entities;

        private List<PreparedRenderPair> PreparedEntities;

        public Renderer()
        {
        }

        public void PrepareRender()
        {
            //TODO: prepare chunks
            PreparedEntities = _objects.Select(item => new PreparedRenderPair(PrepareRenderObject(item.Key), item.Value)).ToList();
        }

        public void Render(Camera _camera)
        {
            foreach (var m in PreparedEntities)
            {
                foreach (var o in m._objects)
                {
                    RenderObject(o, m._data, _camera);
                }
            }
        }

        public void Clean()
        {
            // TODO: Do unbinds here
        }
        private void RenderObject(GameObject _obj, ObjectRenderData _data, Camera _camera)
        {

            GL.BindVertexArray(_data._vertexArrayObject);

            _data._texture.Use(TextureUnit.Texture0);
            _data._shader.Use();

            _data._shader.SetMatrix4("translation", Matrix4.CreateTranslation(_obj.position.Xyz));
            _data._shader.SetMatrix4("view", _camera.GetViewMatrix());
            _data._shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            //GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _data._model.triangleCount * 3);
        }

        private ObjectRenderData PrepareRenderObject(TexturedModel _tmodel)
        {
            ObjectRenderData renderData = new ObjectRenderData();
            renderData._vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(renderData._vertexArrayObject);

            renderData._model = _tmodel.model;
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

            renderData._shader = _tmodel.shader;
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

            renderData._texture = _tmodel.texture;
            renderData._texture.Use(TextureUnit.Texture0);

            // Set texture filter
            GL.TextureParameteri(renderData._texture.Handle, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TextureParameteri(renderData._texture.Handle, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            renderData._shader.SetInt("texture0", 0);

            return renderData;
        }
    }
}
