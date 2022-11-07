using System;
using Syrinj.Attributes;

namespace Syrinj
{
    public class FindResourceOfTypeAttribute : UnityConvenienceAttribute
    {
        public readonly Type ComponentType;

        public FindResourceOfTypeAttribute()
        { }

        public FindResourceOfTypeAttribute(Type componentType)
        {
            ComponentType = componentType;
        }
    }
}
