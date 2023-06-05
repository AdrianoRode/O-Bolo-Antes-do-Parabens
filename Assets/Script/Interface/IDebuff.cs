using System.Collections;
using UnityEngine.EventSystems;

public interface IDebuff : IEventSystemHandler
{
    IEnumerable ApplyStun(bool active);
}
