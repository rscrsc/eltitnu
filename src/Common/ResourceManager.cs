using System.IO;
using System.Collections.Generic;

namespace Eltitnu.Common
{
    public static class ResourceManager
    {
        private static List<(string vert,string frag)> shaderSources;
        static ResourceManager()
        {
            shaderSources = new List<(string vert, string frag)>();
        }
        public static void LoadShaders(List<string> vertPaths, List<string> fragPaths)
        {
            (string vert, string frag) shaderSource = default;
            foreach(string vertPath in vertPaths)
            {
                shaderSource.vert = File.ReadAllText(vertPath);
            }
            foreach(string fragPath in fragPaths)
            {
                shaderSource.frag = File.ReadAllText(fragPath);
            }
            shaderSources.Add(shaderSource);
        }
        public static void CompileShaders()
        {

        }
    }
}

