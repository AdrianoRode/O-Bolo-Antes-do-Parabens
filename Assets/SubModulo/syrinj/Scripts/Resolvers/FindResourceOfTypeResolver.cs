using Syrinj.Attributes;
using Syrinj.Injection;
using UnityEngine;

namespace Syrinj.Resolvers
{
    public class FindResourceOfTypeResolver : IResolver
    {
        public object Resolve(Injectable injectable)
        {
            var attribute = (FindResourceOfTypeAttribute)injectable.Attribute;
            if (attribute.ComponentType == null)
            {
                //var resources = Resources.FindObjectsOfTypeAll(injectable.Type);
                var resources =  Resources.LoadAll("", injectable.Type);
                return resources?[0];
            }
            else
            {
                //var resources = Resources.FindObjectsOfTypeAll(attribute.ComponentType);
                var resources =  Resources.LoadAll("", attribute.ComponentType); //.ToArray();
                return resources?[0];
            }
        }
    }
    // var attribute = (FindResourceOfTypeAttribute)injectable.Attribute;
    // string[] guids;
    //
    // string path = "";
    // guids = AssetDatabase.FindAssets("t:"+nameof(attribute.ComponentType));
    // foreach (string guid in guids)
    // {
    //     path = AssetDatabase.GUIDToAssetPath(guid);
    //     Debug.Log("path: "+path);
    // }
    //
    // return AssetDatabase.LoadAssetAtPath(path, attribute.ComponentType);
}