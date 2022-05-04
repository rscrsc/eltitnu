using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

namespace Eltitnu.Common
{
    public static class World
    {
        public static Dictionary<TexturedModel, List<GameObject>> Entities = new();
        public static List<GameObject> Chunks = new();
    }
}

