using System;
using System.Collections.Generic;
using System.Linq;

namespace Eltitnu.Common
{
    internal class BufferAttribute
    {
        public string name;
        public int size;
        public int offset;
        public int stride;
        public List<float> values = new();
    }
    public class BufferArray
    {
        private Dictionary<int, BufferAttribute> BufferAttributes = new();
        public int ElementCount = 0;
        private bool isFromMode = false;
        private bool isSetElementCount = false;
        public BufferArray(string mode)
        {
            if(mode == "FROM_ARRAY")
            {
                isFromMode = true;
            }
            else if(mode == "To_ARRAY")
            {

            }
        }
        public void setElementCount(int count)
        {
            ElementCount = count;
            isSetElementCount = true;
        }
        public void AddAttribute(int index, string name, int size, int offset, int stride)
        {
            if (BufferAttributes.ContainsKey(index))
            {
                throw new Exception("Attribute Index exists");
            }
            var newBufferAttribute = new BufferAttribute()
            {
                name = name,
                size = size,
                offset = offset,
                stride = stride
            };
            BufferAttributes.Add(index, newBufferAttribute);
        }
        public void AddValue(int index, float value)
        {
            if (BufferAttributes.ContainsKey(index))
            {
                BufferAttributes[index].values.Add(value);
            }
            else
            {
                throw new Exception("Index Not Found");
            }
        }
        public Array ToArray()
        {
            if(isSetElementCount == false)
            {
                throw new Exception("No ElementCount Set");
            }
            int arrayLength = BufferAttributes.First().Value.stride * ElementCount;
            float[] array = new float[arrayLength];
            foreach (var item in BufferAttributes)
            {
                var attribute = item.Value;
                int attPtr = attribute.offset;
                for(int i = 0; i < attribute.values.Count;)
                {
                    for(int j = 0; j < attribute.size; j++)
                    {
                        array[attPtr + j] = attribute.values[i];
                        i++;
                    }
                    attPtr += attribute.stride;
                }
            }
            return array;
        }

        // TODO: Read mode is useless so far
        public void ReadArray(float[] array)
        {
            if(isFromMode == false)
            {
                throw new Exception("Not in FROM_ARRAY Mode");
            }
            else if(BufferAttributes.Count == 0)
            {
                throw new Exception("No Attributes Set");
            }
            foreach(var item in BufferAttributes)
            {
                var attribute = item.Value;
                int attPtr = attribute.offset;
                for (int i = 0; i < ElementCount; i++)
                {
                    for (int j = 0; j < attribute.size; j++)
                    {
                        attribute.values.Add(array[attPtr + j]);
                    }
                    attPtr += attribute.stride;
                }
            }
        }
    }
}
