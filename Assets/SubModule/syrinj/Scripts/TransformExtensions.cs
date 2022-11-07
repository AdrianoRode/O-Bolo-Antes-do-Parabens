using System;
using UnityEngine;

namespace Syrinj
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Find a Transform within the current one, supporting deep searching using the same format as GameObject.Find.
        /// </summary>
        /// <param name="parent">The parent Transform</param>
        /// <param name="path">Specify the path to the child Transform e.g. "drawable/turret" </param>
        /// <returns>The resulting Transform, or null</returns>
        public static Transform FindRelativePath(this Transform parent, string path)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            if (String.IsNullOrEmpty(path)) throw new ArgumentException("must be specified", "path");

            var pathSegs = path.Trim('/', ' ').Split('/');

            var current = parent;
            foreach (var seg in pathSegs)
            {
                current = current.Find(seg);
                if (current == null) return null;
            }

            return current;
        }
    }
}