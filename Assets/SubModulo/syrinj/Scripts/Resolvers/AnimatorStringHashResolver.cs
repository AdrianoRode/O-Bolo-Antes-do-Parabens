using Syrinj.Injection;
using UnityEngine;

namespace Syrinj.Resolvers
{
    public class AnimatorStringHashResolver : IResolver
    {
        public object Resolve(Injectable injectable)
        {
            var name = ((AnimatorStringHashAttribute)injectable.Attribute).HashName;
            return Animator.StringToHash(name);
        }
    }
}
