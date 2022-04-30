using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Eltitnu.Common
{
    public class COLLADA
    {
        public BufferArray vertexBuffer = new("TO_ARRAY");
        public COLLADA(string path)
        {
            // Load.dae model file
            XElement file = XElement.Load(path);
            XNamespace globalNamespace = "http://www.collada.org/2005/11/COLLADASchema";

            var geometries = from geometry in file.Descendants(globalNamespace + "geometry")
                             select geometry;
            foreach (var geometry in geometries)
            {
                var sources = from source in geometry.Descendants(globalNamespace + "source")
                              select new
                              {
                                  id = source.Attribute("id").Value,
                                  value = source.Element(globalNamespace + "float_array").Value,
                              };
                var model = sources.ToDictionary(x => x.id, x => x.value);

                // Load sources
                // - TODO: Key names below may not be that if the model changes
                //   ? How to get KeyValue Pairs one by one in my read order 
                List<float>[] sourceArrays = new List<float>[3]
                {
                    new(), new(), new()
                };
                foreach (var nums in model["Cube-mesh-positions"].Split(' '))
                {
                    sourceArrays[0].Add(float.Parse(nums));
                }
                List<float> normal = new List<float>();
                foreach(var nums in model["Cube-mesh-normals"].Split(' '))
                {
                    sourceArrays[1].Add(float.Parse(nums));
                }
                List<float> texcoord = new List<float>();
                foreach( var nums in model["Cube-mesh-map-0"].Split (' '))
                {
                    sourceArrays[2].Add(float.Parse(nums));
                }

                // Load Triangles
                var counts = from triangles in geometry.Descendants(globalNamespace + "triangles")
                             select triangles.Attribute("count").Value;
                int triangleCount = int.Parse(counts.First());

                var indices = from triangles in geometry.Descendants(globalNamespace + "triangles")
                              select triangles.Element(globalNamespace + "p").Value;
                List<int> indexBuffer = new();
                foreach (var nums in indices.First().Split(' '))
                {
                    indexBuffer.Add(int.Parse(nums));
                }

                // BufferArray pre-settings
                vertexBuffer.setElementCount(triangleCount * 3); // times 3 because we use triangles
                vertexBuffer.AddAttribute(0, "VERTEX", 3, 0, 8);
                vertexBuffer.AddAttribute(1, "NORMAL", 3, 3, 8);
                vertexBuffer.AddAttribute(2, "TEXCOORD", 2, 6, 8);

                // Fill in sources
                int[] sizes = new int[3]{3, 3, 2};
                for(int i = 0; i < indexBuffer.Count; i++)
                {
                    int index = indexBuffer[i];
                    for (int j = index * sizes[i % 3]; j < (index+1) * sizes[i % 3]; j++)
                    {
                        vertexBuffer.AddValue(i % 3, sourceArrays[i % 3][j]);
                    }
                }
            }
        }
        public float[] Generate()
        {
            return (float[])vertexBuffer.ToArray();
        }
    }
}
