using Syrinj.Attributes;

namespace Syrinj
{
    public class FindRelativeAttribute : UnityConvenienceAttribute
    {
        public readonly string PathName;

        public FindRelativeAttribute(string pathName)
        {
            PathName = pathName;
        }
    }
}
