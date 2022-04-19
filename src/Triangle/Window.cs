using Eltitnu.Common;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Runtime.InteropServices;

namespace Eltitnu
{
    public class TriangleWindow : GameWindow
    {
        // Vertices for the triangle (NDC)
        private readonly float[] _vertices =
        {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
             0.5f, -0.5f, 0.0f, // Bottom-right vertex
             0.0f,  0.5f, 0.0f  // Top vertex
        };

        // Handles to OpenGL objects
        private BufferHandle _vertexBufferObject;
        private VertexArrayHandle _vertexArrayObject;

        // A wrapper around a shader
        private Shader _shader;

        public TriangleWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(
                BufferTargetARB.ArrayBuffer,
                _vertices.Length * sizeof(float),
                Marshal.UnsafeAddrOfPinnedArrayElement(_vertices, 0),
                BufferUsageARB.StaticDraw
            );

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _shader = new Shader("Shaders/triangle.vert", "Shaders/triangle.frag");
            _shader.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            int width, height;
            unsafe
            {
                GLFW.GetFramebufferSize(WindowPtr, out width, out height);
            }
            GL.Viewport(0, 0, width, height);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, BufferHandle.Zero);
            GL.BindVertexArray(VertexArrayHandle.Zero);
            GL.UseProgram(ProgramHandle.Zero);

            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            GL.DeleteProgram(_shader.Handle);

            base.OnUnload();
        }
    }
}
