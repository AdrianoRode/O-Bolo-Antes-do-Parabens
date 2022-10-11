using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IInventory
{
    [Header("Weapons Inventory")]
    [SerializeField]private List<GameObject> weapons;
    [SerializeField]private List<GameObject> pickUp;

    [Header("Weapon Position")]
    [SerializeField]private Transform localWeapon;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(true);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(true);
        }
    }
    public IEnumerable PickingUpItem(GameObject i)
    {
        pickUp.Add(i);
        
        var playerpos = new Vector3(localWeapon.position.x, localWeapon.position.y, localWeapon.position.z);
        
        GameObject go = Instantiate(i, playerpos, transform.localRotation) as GameObject;
        go.transform.parent = GameObject.Find("Player").transform;
        weapons.Add(go);
        
        yield return null;
    }
    public IEnumerable PickingUpWeapon(WeaponSO weaponSo)
    {
        yield return null;
    }

    public IEnumerable PickingCoin(int c)
    {
        yield return null;
    }
     
}
