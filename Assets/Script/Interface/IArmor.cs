using System.Collections;
using UnityEngine.EventSystems;

public interface IArmor : IEventSystemHandler
{
    IEnumerable ApplyDamage(int damage);

    int? GetHealth();
}
