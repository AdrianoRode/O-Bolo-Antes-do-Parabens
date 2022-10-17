using System;
using System.Collections;
using Ez;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    [Header("Weapon")]
    [SerializeField]private WeaponSO weapon;
    private bool canShoot = true;
    
    [Header("Bullet")]
    [SerializeField]private TrailRenderer bulletTrail;
    [SerializeField]private Transform[] localFire;
    private int damage;

    void OnEnable()
    {
        canShoot = true;
    }
    
    void Update()
    {
        damage = weapon.damage;
    }
    
    public IEnumerable Fire()
    {
        RaycastHit hit;
        int layerMask = 1 << 2;
        layerMask = ~layerMask;
        
        if (canShoot && weapon.ammo > 0)
        {
            for (int i = 0; i < localFire.Length; i++)
            {
                if (Physics.Raycast(localFire[i].position, localFire[i].TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.transform.gameObject.CompareTag("Hitable"))
                    {
                        hit.collider.gameObject.Send<IArmor>(_=>_.ApplyDamage(damage));
                    }
                    
                    weapon.ammo--;
                    Debug.Log(weapon.ammo);
                    TrailRenderer trail = Instantiate(bulletTrail, localFire[i].position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, hit));
                }  
            }
            
            canShoot = false;
            StartCoroutine(Delay().GetEnumerator());
        }
        yield return null;
    }

    public IEnumerable Delay()
    {
        yield return new WaitForSeconds(weapon.cadence * Time.deltaTime);
        canShoot = true;
    }

    public int? Ammo()
    {
        return weapon.ammo;
    }

    IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;
        
        while(time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;
            
            yield return null;
        }
        trail.transform.position = hit.point;
        
        Destroy(trail.gameObject, trail.time);
    }

}
