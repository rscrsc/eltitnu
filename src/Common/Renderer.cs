using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System.Runtime.InteropServices;

namespace Eltitnu.Common
{
    internal class ObjectRenderer
    {
        //private BufferHandle _elementBufferObject;

        private BufferHandle _vertexBufferObject;

        private VertexArrayHandle _vertexArrayObject;

        COLLADA _model;

        private Shader _shader;

        private Texture _texture;
        internal void RenderObject(GameObject _obj, Camera _camera)
        {

            GL.BindVertexArray(_vertexArrayObject);

            _texture.Use(TextureUnit.Texture0);
            _shader.Use();

            _shader.SetMatrix4("translation", Matrix4.CreateTranslation(_obj.position.Xyz));
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            //GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _model.triangleCount * 3);
        }

        internal void PrepareRenderObject(TexturedModel _tmodel)
        {
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _model = _tmodel.model;
            float[] vertexBuffer = _model.Generate();

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
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

            _shader = _tmodel.shader;
            _shader.Use();

            uint vertexLocation = _shader.GetAttribLocation("inPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            uint normalLocation = _shader.GetAttribLocation("inNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            uint texCoordLocation = _shader.GetAttribLocation("inTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            _texture = _tmodel.texture;
            _texture.Use(TextureUnit.Texture0);

            // Set texture filter
            GL.TextureParameteri(_texture.Handle, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TextureParameteri(_texture.Handle, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            _shader.SetInt("texture0", 0);
        }

        internal void CleanObject()
        {
            //TODO
        }

    }
    public class Renderer
    {
        private ObjectRenderer _objectRender = new ObjectRenderer();
        // private Dictionary<TexturedModel, List<GameObject>> _objects = World.GameObjects;
        private Dictionary<TexturedModel, List<GameObject>> _objects = new();

        // for test
        private TexturedModel test, test2;
        private List<GameObject> testo = new() { new GameObject(new Vector4(-3f, 0f, 0f, 1f)), new GameObject(new Vector4(0f, 0f, 0f, 1f)), new GameObject(new Vector4(3f, 0f, 0f, 1f)) };
        private List<GameObject> testo2 = new() { new GameObject(new Vector4(-3f, 3f, 0f, 1f)), new GameObject(new Vector4(0f, 3f, 0f, 1f)), new GameObject(new Vector4(3f, 3f, 0f, 1f)) };
        public Renderer()
        {
        }

        public void PrepareRender()
        {
            test = new TexturedModel("Resources/creeper.dae", "Shaders/shader.vert", "Shaders/shader.frag", "Resources/creeper.png");
            test2 = new TexturedModel("Resources/block.dae", "Shaders/shader.vert", "Shaders/shader.frag", "Resources/dirt.png");

            _objects.Add(test, testo);
            _objects.Add(test2, testo2);

            foreach (var m in _objects.Keys)
            {
                _objectRender.PrepareRenderObject(m);
            }
        }

        public void Render(Camera _camera)
        {
            foreach (var m in _objects.Values)
            {
                foreach (var o in m)
                {
                    _objectRender.RenderObject(o, _camera);
                }
            }
        }

        public void Clean()
        {
            // TODO: Do unbinds here
        }
    }
}
