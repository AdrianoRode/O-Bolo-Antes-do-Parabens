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
    [SerializeField]private Transform localFire;
    private int damage;
    private int speed = 50;
    
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
        int layerMask = 1 << 6;
        layerMask = ~layerMask;
        
        if (canShoot)
        {
    
            if (Physics.Raycast(localFire.position, localFire.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.gameObject.CompareTag("Hitable"))
                {
                    hit.collider.gameObject.Send<IArmor>(_=>_.ApplyDamage(damage));
                }
                
                TrailRenderer trail = Instantiate(bulletTrail, localFire.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
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
