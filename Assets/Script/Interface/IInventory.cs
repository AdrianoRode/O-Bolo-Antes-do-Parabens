using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInventory : IEventSystemHandler
{
    IEnumerable PickingUpItem(GameObject i);

    IEnumerable PickingUpWeapon(WeaponSO weaponSo);
}
