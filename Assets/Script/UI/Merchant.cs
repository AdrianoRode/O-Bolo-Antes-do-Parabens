using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Merchant : MonoBehaviour, IInventory
{
    [SerializeField]private WeaponSO[] arrayInventory;

    public List<WeaponSO> inventory { get; private set; }

    void Awake()
    {
        inventory = new List<WeaponSO>();
        inventory = arrayInventory.ToList();
    }
    public IEnumerable PickingUpItem(GameObject i)
    {
        yield return null;
    }

    public IEnumerable PickingUpWeapon(WeaponSO weaponSo)
    {
        if (weaponSo != null)
        {
            inventory.Add(weaponSo);
        }
        yield return null;
    }

    public GameObject GetWeapon()
    {
        return null;
    }
    
}
