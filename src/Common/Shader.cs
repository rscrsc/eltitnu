using System;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace Eltitnu.Common
{
    public class Shader
    {
        private const int MAX_BUF_SIZE = 1024;

        public readonly ProgramHandle Handle;
        private readonly Dictionary<string, int> _uniformLocations;

        public Shader(string _vertPath, string _fragPath)
        {
            var _shaderSource = File.ReadAllText(_vertPath);
            var _vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(_vertexShader, _shaderSource);
            CompileShader(_vertexShader);

            _shaderSource = File.ReadAllText(_fragPath);
            var _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_fragmentShader, _shaderSource);
            CompileShader(_fragmentShader);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, _vertexShader);
            GL.AttachShader(Handle, _fragmentShader);

            LinkProgram(Handle);

            GL.DetachShader(Handle, _vertexShader);
            GL.DetachShader(Handle, _fragmentShader);
            GL.DeleteShader(_fragmentShader);
            GL.DeleteShader(_vertexShader);

            int numberOfUniforms = 0;
            GL.GetProgrami(Handle, ProgramPropertyARB.ActiveUniforms, ref numberOfUniforms);

            _uniformLocations = new Dictionary<string, int>();

            for (uint i = 0; i < numberOfUniforms; i++)
            {
                int len = 0;
                string key = GL.GetActiveUniformName(Handle, i, MAX_BUF_SIZE, ref len);
                var location = GL.GetUniformLocation(Handle, key);
                _uniformLocations.Add(key, location);
            }
        }

        private static void CompileShader(ShaderHandle shader)
        {
            GL.CompileShader(shader);

            int code = (int)All.True;
            GL.GetShaderi(shader, ShaderParameterName.CompileStatus, ref code);
            if (code != (int)All.True)
            {
                string infoLog;
                GL.GetShaderInfoLog(shader, out infoLog);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        private static void LinkProgram(ProgramHandle program)
        {
            GL.LinkProgram(program);

            int code = (int)All.True;
            GL.GetProgrami(program, ProgramPropertyARB.LinkStatus, ref code);
            if (code != (int)All.True)
            {
                string infoLog;
                GL.GetProgramInfoLog(program, out infoLog);
                throw new Exception($"Error occurred whilst linking Program({program}).\n\n{infoLog}");
            }
        }

        // A wrapper function that enables the shader program.
        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public uint GetAttribLocation(string attribName)
        {
            return (uint)GL.GetAttribLocation(Handle, attribName);
        }

        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1i(_uniformLocations[name], data);
        }

        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1f(_uniformLocations[name], data);
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4f(_uniformLocations[name], true, data);
        }

        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3f(_uniformLocations[name], data);
        }
    }
}
