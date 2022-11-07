using Syrinj.Attributes;

namespace Syrinj
{
    public class AnimatorStringHashAttribute : UnityConvenienceAttribute
    {
        public readonly string HashName;

        public AnimatorStringHashAttribute(string hashName)
        {
            HashName = hashName;
        }
    }
}
