using UnityEngine;
using Unity.AI.Navigation;

public class NavMeshScript : MonoBehaviour
{
    public NavMeshSurface _navMeshSurface;

    public void NewBake()
    {
        _navMeshSurface.BuildNavMesh();
        
    }
}
