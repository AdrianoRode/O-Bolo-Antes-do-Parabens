using System.Collections.Generic;
using Script.Item;
using UnityEngine;
public class CreateMenu : MonoBehaviour
{
    public ItemReference element;
    private List<WeaponSO> inventory;
    void Start()
    {
        inventory = new List<WeaponSO>();
        inventory = FindObjectOfType<Merchant>().inventory;
        InstantiateElements();
    }
    void OnEnable()
    {
        InstantiateElements();
    }

    void InstantiateElements()
    {
        int i;
        
        for (i = 0; i < inventory.Count; i++)
        {
            (Instantiate(element, transform) as ItemReference).SetValues(inventory[i]);
        }

        inventory.RemoveRange(0, i);

    }

}
