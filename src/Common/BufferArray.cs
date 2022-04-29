using System;
using System.Collections.Generic;

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
        public BufferArray() { }
        public void setElementCount(int count)
        {
            ElementCount = count;
        }
        public void AddAttribute(int index, string name, int size, int offset, int stride)
        {
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
            if(ElementCount == 0)
            {
                throw new Exception("No Elements Added");
            }
            int arrayLength = 0;
            foreach (var item in BufferAttributes)
            {
                arrayLength += item.Value.stride;
            }
            arrayLength *= ElementCount;
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
    }
}
