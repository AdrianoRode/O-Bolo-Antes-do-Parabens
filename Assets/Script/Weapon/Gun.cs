using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    [Header("Bullet")]
    [SerializeField]private GameObject bullet;
    [SerializeField]private Transform localFire;
    
    [Header("Weapon")]
    [SerializeField]private WeaponSO weapon;
    private bool canShoot = true;
    
    void OnEnable()
    {
        canShoot = true;
    }
    public IEnumerable Fire()
    {
        if (canShoot == true)
        {
            Instantiate(bullet, localFire.position, localFire.rotation);
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

}
