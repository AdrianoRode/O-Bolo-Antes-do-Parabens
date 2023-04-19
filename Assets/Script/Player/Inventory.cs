using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ez;
public class Inventory : MonoBehaviour, IInventory
{
    [Header("Weapons Inventory")]
    [SerializeField]private List<GameObject> weapons;
    [SerializeField]private List<GameObject> pickUp;

    [Header("Weapon Position")]
    [SerializeField]private Transform localWeapon;

    private int _currentIndex = 0;
    private int _currentWeapon = 0;
    public GameObject actualWeapon;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll > 0f)
        {
            _currentIndex++;
        }
        else if (scroll < 0f)
        {
            _currentIndex--;
        }
        
        if(_currentIndex > weapons.Count - 1)
        {
            _currentIndex = 0;
        }
        else if(_currentIndex < 0)
        {
            _currentIndex = weapons.Count - 1;
        }
        actualWeapon = weapons[_currentIndex];

        for(int i = 0; i < weapons.Count; i++)
        {
            if(weapons[i] != actualWeapon)
            {
                weapons[i].SetActive(false);
            }
            else
            {
                weapons[i].SetActive(true);
            }
        }

    }
    public IEnumerable PickingUpItem(GameObject i)
    {
        pickUp.Add(i);
        
        int index = pickUp.IndexOf(i);
        _currentWeapon = index;
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

    public IEnumerable Teste(GameObject f)
    {
        yield return null;
    }
    
    public GameObject GetWeapon()
    {
        //Destroy(actualWeapon);
        weapons.Remove(actualWeapon);
        actualWeapon.SetActive(false);
        return actualWeapon;
    }

}
