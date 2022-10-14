using System.Collections;
using UnityEngine.EventSystems;

public interface IUI : IEventSystemHandler
{
    IEnumerable OpenShop(bool s);
    IEnumerable InputUI(bool b);
    IEnumerable TxtNewValue(int i);
    bool? OnShop();

}
