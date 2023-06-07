using System.Collections;
using System.Collections.Generic;
using Script.Weapon;
using UnityEngine;
using DG.Tweening;

public class Ammo : MonoBehaviour
{
    private GameObject player;
    private Gun gun;
    public delegate void AmmoCollect();
    public event AmmoCollect AmmoCollected;
    void OnEnable()
    {
        player = GameObject.Find("Player");

        AmmoCollected += gun.OnAmmoCollected;
    }

    void Start()
    {
        gun = GetComponent<Gun>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            transform.DOMove(player.transform.position,25f * Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AmmoCollected();
            gameObject.SetActive(false);    
        }
    }

}
