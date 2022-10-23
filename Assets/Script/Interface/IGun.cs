using System.Collections;
using UnityEngine.EventSystems;

public interface IGun : IEventSystemHandler
{
    IEnumerable Fire();

    IEnumerable Reload();
    IEnumerable Cadence();
    
}
