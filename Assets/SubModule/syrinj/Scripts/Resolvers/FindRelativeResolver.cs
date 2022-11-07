using Syrinj.Injection;
using UnityEngine;

namespace Syrinj.Resolvers
{
    public class FindRelativeResolver : IResolver
    {
        public object Resolve(Injectable injectable)
        {
            var name = ((FindRelativeAttribute)injectable.Attribute).PathName;

            var parent = ((MonoBehaviour)injectable.Object).transform;
            var transform = parent.FindRelativePath(name);

            if (transform == null)
            {
                return null;
            }
            else if (injectable.Type.IsAssignableFrom(transform.GetType()))
            {
                return transform;
            }
            else
            {
                return transform.GetComponent(injectable.Type);
            }
        }
    }
}
