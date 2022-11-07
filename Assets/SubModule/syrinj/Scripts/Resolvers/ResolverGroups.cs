using System;
using System.Collections.Generic;

namespace Syrinj.Resolvers
{
    public static class ResolverGroups
    {
        public static readonly Dictionary<Type, IResolver> Default = new Dictionary<Type, IResolver>()
        {
            {typeof (AnimatorStringHashAttribute),      new AnimatorStringHashResolver()},
            {typeof (FindAttribute),                    new FindResolver()},
            {typeof (FindObjectOfTypeAttribute),        new FindObjectOfTypeResolver()},
            {typeof (FindResourceOfTypeAttribute),      new FindResourceOfTypeResolver()},            
            {typeof (FindRelativeAttribute),            new FindRelativeResolver()},
            {typeof (FindWithTagAttribute),             new FindWithTagResolver()},
            {typeof (GetComponentAttribute),            new GetComponentResolver()},
            {typeof (GetComponentInChildrenAttribute),  new GetComponentInChildrenResolver()},
        };

        public static readonly Dictionary<Type, IResolver> GetComponentResolvers = new Dictionary<Type, IResolver>()
        {
            {typeof (GetComponentAttribute),            new GetComponentResolver()},
            {typeof (GetComponentInChildrenAttribute),  new GetComponentInChildrenResolver()},
        };

        public static readonly Dictionary<Type, IResolver> Empty = new Dictionary<Type, IResolver>();
    }
}
